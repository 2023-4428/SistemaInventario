using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using SistemaInventario.AccesoDatos.Extensiones;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun;
using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Repositorios;

/// <summary>
/// Repositorio de categorías. Persiste la categoría y sus fotos como una unidad:
/// crear o actualizar guarda ambas dentro de una misma transacción.
/// </summary>
public sealed class CategoriaRepositorio : RepositorioBase<Categoria>, ICategoriaRepositorio
{
    /// <summary>Crea el repositorio con sus dependencias.</summary>
    /// <param name="conexionBD">Singleton de conexión.</param>
    /// <param name="registro">Logger de errores.</param>
    public CategoriaRepositorio(ConexionBD conexionBD, ILogger registro)
        : base(conexionBD, registro)
    {
    }

    /// <inheritdoc />
    protected override string NombreTabla => "Categoria";

    /// <inheritdoc />
    protected override string NombreEntidad => "categoría";

    /// <inheritdoc />
    protected override string ClausulaOrden => "t.Nombre";

    /// <inheritdoc />
    protected override string SqlSeleccion =>
        "SELECT t.Id, t.Nombre, t.Descripcion, t.Activo, t.FechaCreacion, t.FechaModificacion, " +
        "(SELECT COUNT(*) FROM dbo.FotoCategoria f WHERE f.CategoriaId = t.Id AND f.Activo = 1) AS CantidadFotos " +
        "FROM dbo.Categoria t";

    /// <inheritdoc />
    protected override string SqlInsertar =>
        "INSERT INTO dbo.Categoria (Nombre, Descripcion) OUTPUT INSERTED.Id VALUES (@Nombre, @Descripcion);";

    /// <inheritdoc />
    protected override string SqlActualizar =>
        "UPDATE dbo.Categoria SET Nombre = @Nombre, Descripcion = @Descripcion, " +
        "FechaModificacion = SYSDATETIME() WHERE Id = @Id;";

    /// <inheritdoc />
    protected override Categoria Mapear(SqlDataReader lector)
    {
        Categoria categoria = new Categoria
        {
            Nombre = lector.LeerTexto("Nombre"),
            Descripcion = lector.LeerTexto("Descripcion"),
            CantidadFotos = lector.LeerEntero("CantidadFotos")
        };

        MapearCamposBase(lector, categoria);
        return categoria;
    }

    /// <inheritdoc />
    protected override void AsignarParametros(SqlCommand comando, Categoria entidad)
    {
        comando.AgregarParametro("@Nombre", SqlDbType.NVarChar, entidad.Nombre, Categoria.LongitudMaximaNombre);
        comando.AgregarParametro("@Descripcion", SqlDbType.NVarChar, entidad.Descripcion, Categoria.LongitudMaximaDescripcion);
    }

    /// <summary>Obtiene la categoría por identificador, incluyendo sus fotos activas.</summary>
    /// <param name="id">Identificador de la categoría.</param>
    /// <returns>La categoría con sus fotos, o <c>null</c> si no existe.</returns>
    public override Categoria? ObtenerPorId(int id)
    {
        Categoria? categoria = base.ObtenerPorId(id);

        if (categoria != null)
        {
            List<FotoCategoria> fotos = CargarFotos(categoria.Id);
            categoria.EstablecerFotos(fotos);
            categoria.CantidadFotos = fotos.Count;
        }

        return categoria;
    }

    /// <summary>Inserta la categoría y sus fotos en una sola transacción.</summary>
    /// <param name="entidad">Categoría a insertar.</param>
    /// <returns>Identificador generado.</returns>
    public override int Insertar(Categoria entidad)
    {
        if (entidad == null)
        {
            throw new System.ArgumentNullException(nameof(entidad));
        }

        return EjecutarEnTransaccion("insertar categoría", (conexion, transaccion) =>
        {
            int id = InsertarEn(conexion, transaccion, entidad);
            SincronizarFotos(conexion, transaccion, entidad);
            return id;
        });
    }

    /// <summary>Actualiza la categoría y sincroniza sus fotos en una sola transacción.</summary>
    /// <param name="entidad">Categoría con los valores y el conjunto final de fotos.</param>
    public override void Actualizar(Categoria entidad)
    {
        if (entidad == null)
        {
            throw new System.ArgumentNullException(nameof(entidad));
        }

        EjecutarEnTransaccion("actualizar categoría", (conexion, transaccion) =>
        {
            ActualizarEn(conexion, transaccion, entidad);
            SincronizarFotos(conexion, transaccion, entidad);
            return true;
        });
    }

    /// <inheritdoc />
    protected override void EliminarDependientesEn(SqlConnection conexion, SqlTransaction transaccion, int id)
    {
        using SqlCommand comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = "DELETE FROM dbo.FotoCategoria WHERE CategoriaId = @CategoriaId";
        comando.AgregarParametro("@CategoriaId", SqlDbType.Int, id);
        comando.ExecuteNonQuery();
    }

    /// <inheritdoc />
    public bool ExisteNombre(string nombre, int excluirId)
    {
        return ExisteValor("Nombre", nombre, Categoria.LongitudMaximaNombre, excluirId);
    }

    private List<FotoCategoria> CargarFotos(int categoriaId)
    {
        return Ejecutar("cargar las fotos de la categoría", conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText =
                "SELECT Id, CategoriaId, NombreArchivo, Contenido, Activo, FechaCreacion, FechaModificacion " +
                "FROM dbo.FotoCategoria WHERE CategoriaId = @CategoriaId AND Activo = 1 ORDER BY Id";
            comando.AgregarParametro("@CategoriaId", SqlDbType.Int, categoriaId);

            List<FotoCategoria> fotos = new List<FotoCategoria>();

            using SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                FotoCategoria foto = new FotoCategoria
                {
                    CategoriaId = lector.LeerEntero("CategoriaId"),
                    NombreArchivo = lector.LeerTexto("NombreArchivo"),
                    Contenido = lector.LeerBytes("Contenido")
                };

                MapearCamposBase(lector, foto);
                fotos.Add(foto);
            }

            return fotos;
        });
    }

    /// <summary>
    /// Deja las fotos en la base de datos iguales al conjunto indicado: las fotos que ya no están se
    /// desactivan (borrado lógico) y las nuevas (Id = 0) se insertan.
    /// </summary>
    private void SincronizarFotos(SqlConnection conexion, SqlTransaction transaccion, Categoria categoria)
    {
        List<int> idsActuales = new List<int>();

        using (SqlCommand consulta = conexion.CreateCommand())
        {
            consulta.Transaction = transaccion;
            consulta.CommandText = "SELECT Id FROM dbo.FotoCategoria WHERE CategoriaId = @CategoriaId AND Activo = 1";
            consulta.AgregarParametro("@CategoriaId", SqlDbType.Int, categoria.Id);

            using SqlDataReader lector = consulta.ExecuteReader();
            while (lector.Read())
            {
                idsActuales.Add(lector.GetInt32(0));
            }
        }

        HashSet<int> idsConservar = categoria.Fotos.Where(f => f.Id != 0).Select(f => f.Id).ToHashSet();

        foreach (int idQuitar in idsActuales.Where(id => !idsConservar.Contains(id)))
        {
            using SqlCommand baja = conexion.CreateCommand();
            baja.Transaction = transaccion;
            baja.CommandText =
                "UPDATE dbo.FotoCategoria SET Activo = 0, FechaModificacion = SYSDATETIME() " +
                "WHERE Id = @Id AND CategoriaId = @CategoriaId";
            baja.AgregarParametro("@Id", SqlDbType.Int, idQuitar);
            baja.AgregarParametro("@CategoriaId", SqlDbType.Int, categoria.Id);
            baja.ExecuteNonQuery();
        }

        foreach (FotoCategoria foto in categoria.Fotos.Where(f => f.Id == 0))
        {
            using SqlCommand alta = conexion.CreateCommand();
            alta.Transaction = transaccion;
            alta.CommandText =
                "INSERT INTO dbo.FotoCategoria (CategoriaId, NombreArchivo, Contenido) " +
                "OUTPUT INSERTED.Id VALUES (@CategoriaId, @NombreArchivo, @Contenido);";
            alta.AgregarParametro("@CategoriaId", SqlDbType.Int, categoria.Id);
            alta.AgregarParametro("@NombreArchivo", SqlDbType.NVarChar, foto.NombreArchivo, 260);
            alta.AgregarParametro("@Contenido", SqlDbType.VarBinary, foto.Contenido, -1);

            foto.Id = System.Convert.ToInt32(alta.ExecuteScalar(), System.Globalization.CultureInfo.InvariantCulture);
            foto.CategoriaId = categoria.Id;
        }
    }
}
