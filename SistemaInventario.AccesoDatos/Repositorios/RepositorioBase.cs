using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using SistemaInventario.AccesoDatos.Extensiones;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Repositorios;

/// <summary>
/// Implementación genérica de <see cref="IRepositorio{T}"/> (patrón <i>Template Method</i>).
/// Implementa el CRUD y el borrado lógico una sola vez; cada repositorio concreto solo aporta
/// el SQL propio de su tabla y la forma de mapear filas y parámetros.
/// <para>
/// Toda llamada a la base de datos pasa por <see cref="Ejecutar{TResultado}"/>, que captura
/// <see cref="SqlException"/>, la registra en el log y la convierte en <see cref="ErrorBaseDatosException"/>.
/// </para>
/// </summary>
/// <typeparam name="T">Tipo de entidad administrada.</typeparam>
public abstract class RepositorioBase<T> : IRepositorio<T> where T : EntidadBase
{
    private readonly ConexionBD conexionBD;
    private readonly ILogger registro;

    /// <summary>Inicializa el repositorio con sus dependencias (inyección por constructor).</summary>
    /// <param name="conexionBD">Singleton que fabrica las conexiones.</param>
    /// <param name="registro">Logger donde se escriben los errores.</param>
    protected RepositorioBase(ConexionBD conexionBD, ILogger registro)
    {
        if (conexionBD == null)
        {
            throw new ArgumentNullException(nameof(conexionBD));
        }

        if (registro == null)
        {
            throw new ArgumentNullException(nameof(registro));
        }

        this.conexionBD = conexionBD;
        this.registro = registro;
    }

    /// <summary>Nombre de la tabla (sin esquema; se usa <c>dbo</c>).</summary>
    protected abstract string NombreTabla { get; }

    /// <summary>Nombre legible de la entidad, en minúsculas, para los mensajes ("categoría").</summary>
    protected abstract string NombreEntidad { get; }

    /// <summary>Sentencia SELECT completa sin WHERE; la tabla principal debe llevar el alias <c>t</c>.</summary>
    protected abstract string SqlSeleccion { get; }

    /// <summary>Expresión ORDER BY (sin la palabra clave) para los listados.</summary>
    protected abstract string ClausulaOrden { get; }

    /// <summary>Sentencia INSERT que devuelve el identificador con <c>OUTPUT INSERTED.Id</c>.</summary>
    protected abstract string SqlInsertar { get; }

    /// <summary>Sentencia UPDATE con la condición <c>WHERE Id = @Id</c>.</summary>
    protected abstract string SqlActualizar { get; }

    /// <summary>Convierte la fila actual del lector en una entidad.</summary>
    /// <param name="lector">Lector posicionado en una fila.</param>
    /// <returns>La entidad construida.</returns>
    protected abstract T Mapear(SqlDataReader lector);

    /// <summary>Agrega al comando los parámetros de las columnas de negocio (sin <c>@Id</c>).</summary>
    /// <param name="comando">Comando de INSERT o UPDATE.</param>
    /// <param name="entidad">Entidad con los valores.</param>
    protected abstract void AsignarParametros(SqlCommand comando, T entidad);

    /// <summary>Logger del repositorio.</summary>
    protected ILogger Registro => registro;

    /// <summary>
    /// Borra los datos dependientes del registro (tablas hijas) antes de borrar el registro principal.
    /// Se ejecuta dentro de la misma transacción. Por defecto no hay dependientes.
    /// </summary>
    /// <param name="conexion">Conexión abierta.</param>
    /// <param name="transaccion">Transacción en curso.</param>
    /// <param name="id">Identificador del registro principal.</param>
    protected virtual void EliminarDependientesEn(SqlConnection conexion, SqlTransaction transaccion, int id)
    {
    }

    /// <inheritdoc />
    public IReadOnlyList<T> ObtenerActivos() => ListarPorEstado(true);

    /// <inheritdoc />
    public IReadOnlyList<T> ObtenerEliminados() => ListarPorEstado(false);

    /// <inheritdoc />
    public virtual T? ObtenerPorId(int id)
    {
        return Ejecutar<T?>($"consultar {NombreEntidad} por identificador", conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText = SqlSeleccion + " WHERE t.Id = @Id";
            comando.AgregarParametro("@Id", SqlDbType.Int, id);

            using SqlDataReader lector = comando.ExecuteReader();
            return lector.Read() ? Mapear(lector) : null;
        });
    }

    /// <inheritdoc />
    public bool ExisteActivo(int id)
    {
        return Ejecutar($"verificar {NombreEntidad}", conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText =
                $"SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.{NombreTabla} WHERE Id = @Id AND Activo = 1) THEN 1 ELSE 0 END";
            comando.AgregarParametro("@Id", SqlDbType.Int, id);
            return Convert.ToInt32(comando.ExecuteScalar(), CultureInfo.InvariantCulture) == 1;
        });
    }

    /// <inheritdoc />
    public virtual int Insertar(T entidad)
    {
        if (entidad == null)
        {
            throw new ArgumentNullException(nameof(entidad));
        }

        return Ejecutar($"insertar {NombreEntidad}", conexion => InsertarEn(conexion, null, entidad));
    }

    /// <inheritdoc />
    public virtual void Actualizar(T entidad)
    {
        if (entidad == null)
        {
            throw new ArgumentNullException(nameof(entidad));
        }

        EjecutarSinResultado($"actualizar {NombreEntidad}", conexion => ActualizarEn(conexion, null, entidad));
    }

    /// <inheritdoc />
    public void EliminarLogico(int id) => CambiarEstado(id, false, $"eliminar {NombreEntidad}");

    /// <inheritdoc />
    public void Restaurar(int id) => CambiarEstado(id, true, $"restaurar {NombreEntidad}");

    /// <inheritdoc />
    public void EliminarDefinitivo(int id)
    {
        EjecutarEnTransaccion($"eliminar definitivamente {NombreEntidad}", (conexion, transaccion) =>
        {
            EliminarDependientesEn(conexion, transaccion, id);

            using SqlCommand comando = conexion.CreateCommand();
            comando.Transaction = transaccion;
            comando.CommandText = $"DELETE FROM dbo.{NombreTabla} WHERE Id = @Id AND Activo = 0";
            comando.AgregarParametro("@Id", SqlDbType.Int, id);

            if (comando.ExecuteNonQuery() == 0)
            {
                throw new EntidadNoEncontradaException(NombreEntidad, id);
            }

            return true;
        });
    }

    /// <summary>
    /// Ejecuta una acción con una conexión abierta, capturando <see cref="SqlException"/>.
    /// Es el único punto de entrada a la base de datos de los repositorios.
    /// </summary>
    /// <typeparam name="TResultado">Tipo del valor que devuelve la acción.</typeparam>
    /// <param name="operacion">Descripción de la operación, usada en mensajes y en el log.</param>
    /// <param name="accion">Acción a ejecutar con la conexión abierta.</param>
    /// <returns>El resultado de la acción.</returns>
    /// <exception cref="ErrorBaseDatosException">Si SQL Server devuelve un error.</exception>
    protected TResultado Ejecutar<TResultado>(string operacion, Func<SqlConnection, TResultado> accion)
    {
        try
        {
            using SqlConnection conexion = conexionBD.CrearConexion();
            conexion.Open();
            return accion(conexion);
        }
        catch (SqlException ex)
        {
            registro.Error($"Error de SQL Server (número {ex.Number}) al {operacion}.", ex);
            throw new ErrorBaseDatosException(ConstruirMensaje(ex, operacion), ex, ex.Number);
        }
        catch (InvalidOperationException ex)
        {
            registro.Error($"Operación inválida sobre la conexión al {operacion}.", ex);
            throw new ErrorBaseDatosException(
                $"No fue posible usar la conexión con la base de datos al {operacion}. {ex.Message}", ex);
        }
    }

    /// <summary>Variante de <see cref="Ejecutar{TResultado}"/> para acciones que no devuelven valor.</summary>
    /// <param name="operacion">Descripción de la operación.</param>
    /// <param name="accion">Acción a ejecutar con la conexión abierta.</param>
    protected void EjecutarSinResultado(string operacion, Action<SqlConnection> accion)
    {
        Ejecutar<bool>(operacion, conexion =>
        {
            accion(conexion);
            return true;
        });
    }

    /// <summary>
    /// Ejecuta una acción dentro de una transacción: confirma si termina bien y revierte si falla.
    /// </summary>
    /// <typeparam name="TResultado">Tipo del valor que devuelve la acción.</typeparam>
    /// <param name="operacion">Descripción de la operación.</param>
    /// <param name="accion">Acción que recibe la conexión y la transacción.</param>
    /// <returns>El resultado de la acción.</returns>
    protected TResultado EjecutarEnTransaccion<TResultado>(
        string operacion,
        Func<SqlConnection, SqlTransaction, TResultado> accion)
    {
        return Ejecutar(operacion, conexion =>
        {
            using SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                TResultado resultado = accion(conexion, transaccion);
                transaccion.Commit();
                return resultado;
            }
            catch (Exception)
            {
                RevertirSinFallar(transaccion);
                throw;
            }
        });
    }

    /// <summary>Inserta la entidad usando una conexión (y transacción) ya abiertas.</summary>
    /// <param name="conexion">Conexión abierta.</param>
    /// <param name="transaccion">Transacción en curso, o <c>null</c>.</param>
    /// <param name="entidad">Entidad a insertar.</param>
    /// <returns>Identificador generado.</returns>
    protected int InsertarEn(SqlConnection conexion, SqlTransaction? transaccion, T entidad)
    {
        using SqlCommand comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = SqlInsertar;
        AsignarParametros(comando, entidad);

        int id = Convert.ToInt32(comando.ExecuteScalar(), CultureInfo.InvariantCulture);
        entidad.Id = id;
        return id;
    }

    /// <summary>Actualiza la entidad usando una conexión (y transacción) ya abiertas.</summary>
    /// <param name="conexion">Conexión abierta.</param>
    /// <param name="transaccion">Transacción en curso, o <c>null</c>.</param>
    /// <param name="entidad">Entidad con los valores nuevos.</param>
    /// <exception cref="EntidadNoEncontradaException">Si el registro no existe.</exception>
    protected void ActualizarEn(SqlConnection conexion, SqlTransaction? transaccion, T entidad)
    {
        using SqlCommand comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = SqlActualizar;
        AsignarParametros(comando, entidad);
        comando.AgregarParametro("@Id", SqlDbType.Int, entidad.Id);

        if (comando.ExecuteNonQuery() == 0)
        {
            throw new EntidadNoEncontradaException(NombreEntidad, entidad.Id);
        }
    }

    /// <summary>Lee todas las filas del comando y las convierte en entidades.</summary>
    /// <param name="comando">Comando con la consulta.</param>
    /// <returns>Lista de entidades.</returns>
    protected List<T> LeerLista(SqlCommand comando)
    {
        List<T> lista = new List<T>();

        using SqlDataReader lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(Mapear(lector));
        }

        return lista;
    }

    /// <summary>Asigna los campos comunes (identificador, estado y fechas) a la entidad.</summary>
    /// <param name="lector">Lector posicionado en una fila con las columnas comunes.</param>
    /// <param name="entidad">Entidad a completar.</param>
    protected static void MapearCamposBase(SqlDataReader lector, EntidadBase entidad)
    {
        entidad.Id = lector.LeerEntero("Id");
        entidad.Activo = lector.LeerBooleano("Activo");
        entidad.FechaCreacion = lector.LeerFecha("FechaCreacion");
        entidad.FechaModificacion = lector.LeerFecha("FechaModificacion");
    }

    /// <summary>Indica si ya existe un registro con el mismo valor en una columna única.</summary>
    /// <param name="columna">Nombre de la columna (valor fijo definido en el código, nunca del usuario).</param>
    /// <param name="valor">Valor a buscar.</param>
    /// <param name="tamano">Tamaño de la columna NVARCHAR.</param>
    /// <param name="excluirId">Identificador a excluir (0 al crear).</param>
    /// <returns><c>true</c> si el valor ya existe en otro registro.</returns>
    protected bool ExisteValor(string columna, string valor, int tamano, int excluirId)
    {
        return Ejecutar($"verificar duplicados de {NombreEntidad}", conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText =
                $"SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.{NombreTabla} WHERE {columna} = @Valor AND Id <> @Id) THEN 1 ELSE 0 END";
            comando.AgregarParametro("@Valor", SqlDbType.NVarChar, valor, tamano);
            comando.AgregarParametro("@Id", SqlDbType.Int, excluirId);
            return Convert.ToInt32(comando.ExecuteScalar(), CultureInfo.InvariantCulture) == 1;
        });
    }

    private IReadOnlyList<T> ListarPorEstado(bool activo)
    {
        string operacion = activo ? $"listar {NombreEntidad} activos" : $"listar {NombreEntidad} eliminados";

        return Ejecutar<IReadOnlyList<T>>(operacion, conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText = $"{SqlSeleccion} WHERE t.Activo = @Activo ORDER BY {ClausulaOrden}";
            comando.AgregarParametro("@Activo", SqlDbType.Bit, activo);
            return LeerLista(comando);
        });
    }

    private void CambiarEstado(int id, bool activo, string operacion)
    {
        EjecutarSinResultado(operacion, conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText =
                $"UPDATE dbo.{NombreTabla} SET Activo = @Activo, FechaModificacion = SYSDATETIME() WHERE Id = @Id";
            comando.AgregarParametro("@Activo", SqlDbType.Bit, activo);
            comando.AgregarParametro("@Id", SqlDbType.Int, id);

            if (comando.ExecuteNonQuery() == 0)
            {
                throw new EntidadNoEncontradaException(NombreEntidad, id);
            }
        });
    }

    private void RevertirSinFallar(SqlTransaction transaccion)
    {
        try
        {
            transaccion.Rollback();
        }
        catch (SqlException ex)
        {
            registro.Error("No se pudo revertir la transacción.", ex);
        }
        catch (InvalidOperationException ex)
        {
            registro.Error("La transacción ya no estaba activa al intentar revertirla.", ex);
        }
    }

    private static string ConstruirMensaje(SqlException ex, string operacion)
    {
        switch (ex.Number)
        {
            case ErrorBaseDatosException.ViolacionUnica:
            case ErrorBaseDatosException.IndiceUnicoDuplicado:
                return $"No se pudo {operacion}: ya existe un registro con un valor duplicado.";
            case ErrorBaseDatosException.ViolacionClaveForanea:
                return $"No se pudo {operacion}: el registro está relacionado con otros datos (integridad referencial).";
            case 4060:
                return "No se encontró la base de datos SistemaInventario. Ejecute primero el script database/schema.sql.";
            case 18456:
                return "SQL Server rechazó las credenciales de acceso. Revise la cadena de conexión.";
            case -2:
                return $"La operación excedió el tiempo de espera al {operacion}.";
            case -1:
            case 2:
            case 53:
            case 10060:
            case 10061:
            case 11001:
                return "No se pudo conectar con SQL Server. Verifique que el servicio esté en ejecución y que el nombre de la instancia sea correcto.";
            default:
                return $"Ocurrió un error de base de datos al {operacion}.";
        }
    }
}
