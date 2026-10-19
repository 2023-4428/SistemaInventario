using System;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace SistemaInventario.AccesoDatos;

/// <summary>
/// Patrón <b>Singleton</b> para el acceso a la base de datos.
/// <para>
/// Existe una única instancia en toda la aplicación (<see cref="Instancia"/>) que centraliza la
/// cadena de conexión y fabrica las conexiones. Cada llamada a <see cref="CrearConexion"/> devuelve una
/// <see cref="SqlConnection"/> nueva y de vida corta: ADO.NET las reutiliza internamente mediante su
/// pool de conexiones, lo que evita problemas de concurrencia y de lectores abiertos que aparecen al
/// compartir una sola conexión física entre todas las operaciones.
/// </para>
/// </summary>
public sealed class ConexionBD
{
    private static readonly Lazy<ConexionBD> instancia =
        new Lazy<ConexionBD>(() => new ConexionBD(), LazyThreadSafetyMode.ExecutionAndPublication);

    private readonly object candado = new object();
    private string? cadenaConexion;

    private ConexionBD()
    {
    }

    /// <summary>Única instancia de la clase (creación perezosa y segura entre hilos).</summary>
    public static ConexionBD Instancia => instancia.Value;

    /// <summary>Indica si ya se estableció la cadena de conexión.</summary>
    public bool EstaConfigurada
    {
        get
        {
            lock (candado)
            {
                return cadenaConexion != null && cadenaConexion.Trim().Length > 0;
            }
        }
    }

    /// <summary>Establece la cadena de conexión que usarán todos los repositorios.</summary>
    /// <param name="cadena">Cadena de conexión de SQL Server.</param>
    public void Configurar(string cadena)
    {
        if (cadena == null || cadena.Trim().Length == 0)
        {
            throw new ArgumentException("La cadena de conexión es obligatoria.", nameof(cadena));
        }

        lock (candado)
        {
            cadenaConexion = cadena;
        }
    }

    /// <summary>Crea una conexión nueva (cerrada) con la cadena configurada. Quien la reciba debe liberarla.</summary>
    /// <returns>Una <see cref="SqlConnection"/> lista para abrirse.</returns>
    /// <exception cref="InvalidOperationException">Si todavía no se llamó a <see cref="Configurar"/>.</exception>
    public SqlConnection CrearConexion()
    {
        string cadena;

        lock (candado)
        {
            if (cadenaConexion == null || cadenaConexion.Trim().Length == 0)
            {
                throw new InvalidOperationException(
                    "La conexión a la base de datos no ha sido configurada. Llame a Configurar() antes de usarla.");
            }

            cadena = cadenaConexion;
        }

        return new SqlConnection(cadena);
    }

    /// <summary>Intenta abrir una conexión para comprobar que la base de datos está disponible.</summary>
    /// <param name="mensajeError">Mensaje del error si la prueba falla; <c>null</c> si tuvo éxito.</param>
    /// <returns><c>true</c> si la conexión se abrió correctamente.</returns>
    public bool ProbarConexion(out string? mensajeError)
    {
        try
        {
            using SqlConnection conexion = CrearConexion();
            conexion.Open();
            mensajeError = null;
            return true;
        }
        catch (SqlException ex)
        {
            mensajeError = ex.Message;
            return false;
        }
        catch (InvalidOperationException ex)
        {
            mensajeError = ex.Message;
            return false;
        }
    }
}
