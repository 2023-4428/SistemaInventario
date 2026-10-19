using System;
using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using SistemaInventario.AccesoDatos.Extensiones;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun;
using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Repositorios;

/// <summary>
/// Repositorio de productos. Sus consultas incluyen (con JOIN) el nombre de la categoría
/// y del suplidor, aunque estén eliminados, para poder mostrar la Papelera completa.
/// </summary>
public sealed class ProductoRepositorio : RepositorioBase<Producto>, IProductoRepositorio
{
    /// <summary>Crea el repositorio con sus dependencias.</summary>
    /// <param name="conexionBD">Singleton de conexión.</param>
    /// <param name="registro">Logger de errores.</param>
    public ProductoRepositorio(ConexionBD conexionBD, ILogger registro)
        : base(conexionBD, registro)
    {
    }

    /// <inheritdoc />
    protected override string NombreTabla => "Producto";

    /// <inheritdoc />
    protected override string NombreEntidad => "producto";

    /// <inheritdoc />
    protected override string ClausulaOrden => "t.Nombre";

    /// <inheritdoc />
    protected override string SqlSeleccion =>
        "SELECT t.Id, t.Nombre, t.PrecioUnitario, t.CategoriaId, t.SuplidorId, " +
        "c.Nombre AS CategoriaNombre, s.NombreEmpresa AS SuplidorNombre, " +
        "t.Activo, t.FechaCreacion, t.FechaModificacion " +
        "FROM dbo.Producto t " +
        "INNER JOIN dbo.Categoria c ON c.Id = t.CategoriaId " +
        "INNER JOIN dbo.Suplidor s ON s.Id = t.SuplidorId";

    /// <inheritdoc />
    protected override string SqlInsertar =>
        "INSERT INTO dbo.Producto (Nombre, PrecioUnitario, CategoriaId, SuplidorId) " +
        "OUTPUT INSERTED.Id VALUES (@Nombre, @PrecioUnitario, @CategoriaId, @SuplidorId);";

    /// <inheritdoc />
    protected override string SqlActualizar =>
        "UPDATE dbo.Producto SET Nombre = @Nombre, PrecioUnitario = @PrecioUnitario, " +
        "CategoriaId = @CategoriaId, SuplidorId = @SuplidorId, FechaModificacion = SYSDATETIME() " +
        "WHERE Id = @Id;";

    /// <inheritdoc />
    protected override Producto Mapear(SqlDataReader lector)
    {
        Producto producto = new Producto
        {
            Nombre = lector.LeerTexto("Nombre"),
            PrecioUnitario = lector.LeerDecimal("PrecioUnitario"),
            CategoriaId = lector.LeerEntero("CategoriaId"),
            SuplidorId = lector.LeerEntero("SuplidorId"),
            CategoriaNombre = lector.LeerTexto("CategoriaNombre"),
            SuplidorNombre = lector.LeerTexto("SuplidorNombre")
        };

        MapearCamposBase(lector, producto);
        return producto;
    }

    /// <inheritdoc />
    protected override void AsignarParametros(SqlCommand comando, Producto entidad)
    {
        comando.AgregarParametro("@Nombre", SqlDbType.NVarChar, entidad.Nombre, Producto.LongitudMaximaNombre);
        comando.AgregarParametroDecimal("@PrecioUnitario", entidad.PrecioUnitario);
        comando.AgregarParametro("@CategoriaId", SqlDbType.Int, entidad.CategoriaId);
        comando.AgregarParametro("@SuplidorId", SqlDbType.Int, entidad.SuplidorId);
    }

    /// <inheritdoc />
    public int ContarActivosPorCategoria(int categoriaId)
    {
        return ContarActivos("CategoriaId", categoriaId);
    }

    /// <inheritdoc />
    public int ContarActivosPorSuplidor(int suplidorId)
    {
        return ContarActivos("SuplidorId", suplidorId);
    }

    /// <inheritdoc />
    public int ContarPorCategoria(int categoriaId)
    {
        return Contar("contar productos de la categoría", "SELECT COUNT(*) FROM dbo.Producto WHERE CategoriaId = @Valor", categoriaId);
    }

    /// <inheritdoc />
    public int ContarPorSuplidor(int suplidorId)
    {
        return Contar("contar productos del suplidor", "SELECT COUNT(*) FROM dbo.Producto WHERE SuplidorId = @Valor", suplidorId);
    }

    /// <inheritdoc />
    public int ContarEnFacturas(int productoId)
    {
        return Contar(
            "contar las facturas del producto",
            "SELECT COUNT(*) FROM dbo.FacturaDetalle WHERE ProductoId = @Valor",
            productoId);
    }

    /// <inheritdoc />
    public bool ExisteDuplicado(string nombre, int categoriaId, int suplidorId, int excluirId)
    {
        return Ejecutar("verificar productos duplicados", conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText =
                "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Producto WHERE Nombre = @Nombre " +
                "AND CategoriaId = @CategoriaId AND SuplidorId = @SuplidorId AND Id <> @Id) THEN 1 ELSE 0 END";
            comando.AgregarParametro("@Nombre", SqlDbType.NVarChar, nombre, Producto.LongitudMaximaNombre);
            comando.AgregarParametro("@CategoriaId", SqlDbType.Int, categoriaId);
            comando.AgregarParametro("@SuplidorId", SqlDbType.Int, suplidorId);
            comando.AgregarParametro("@Id", SqlDbType.Int, excluirId);
            return Convert.ToInt32(comando.ExecuteScalar(), CultureInfo.InvariantCulture) == 1;
        });
    }

    private int ContarActivos(string columna, int valor)
    {
        return Contar(
            "contar productos activos",
            $"SELECT COUNT(*) FROM dbo.Producto WHERE {columna} = @Valor AND Activo = 1",
            valor);
    }

    private int Contar(string operacion, string sql, int valor)
    {
        return Ejecutar(operacion, conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText = sql;
            comando.AgregarParametro("@Valor", SqlDbType.Int, valor);
            return Convert.ToInt32(comando.ExecuteScalar(), CultureInfo.InvariantCulture);
        });
    }
}
