using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace SistemaInventario.Comun;

/// <summary>
/// Implementación de <see cref="ILogger"/> que escribe los errores en el archivo <c>errores.log</c>.
/// Es segura para uso concurrente y nunca lanza excepciones: si no puede escribir, lo ignora
/// para no interrumpir la aplicación por un problema de registro.
/// </summary>
public sealed class Logger : ILogger
{
    /// <summary>Nombre del archivo de registro por defecto.</summary>
    public const string NombreArchivoPredeterminado = "errores.log";

    private readonly object candado = new object();
    private readonly string rutaAlterna;

    /// <summary>Crea un logger que escribe junto al ejecutable, en <c>errores.log</c>.</summary>
    public Logger() : this(Path.Combine(AppContext.BaseDirectory, NombreArchivoPredeterminado))
    {
    }

    /// <summary>Crea un logger que escribe en la ruta indicada.</summary>
    /// <param name="rutaArchivo">Ruta completa del archivo de registro.</param>
    public Logger(string rutaArchivo)
    {
        if (string.IsNullOrWhiteSpace(rutaArchivo))
        {
            throw new ArgumentException("La ruta del archivo de registro es obligatoria.", nameof(rutaArchivo));
        }

        RutaArchivo = rutaArchivo;
        rutaAlterna = Path.Combine(Path.GetTempPath(), "SistemaInventario", NombreArchivoPredeterminado);
    }

    /// <inheritdoc />
    public string RutaArchivo { get; }

    /// <inheritdoc />
    public void Error(string mensaje, Exception? excepcion = null) => Escribir("ERROR", mensaje, excepcion);

    /// <inheritdoc />
    public void Advertencia(string mensaje) => Escribir("ADVERTENCIA", mensaje, null);

    /// <inheritdoc />
    public void Informacion(string mensaje) => Escribir("INFO", mensaje, null);

    private void Escribir(string nivel, string mensaje, Exception? excepcion)
    {
        StringBuilder linea = new StringBuilder();
        linea.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
        linea.Append(" [").Append(nivel).Append("] ").AppendLine(mensaje);

        if (excepcion != null)
        {
            linea.Append("    Excepción: ").AppendLine(excepcion.GetType().FullName);
            linea.AppendLine("    " + excepcion.ToString().Replace(Environment.NewLine, Environment.NewLine + "    "));
        }

        string texto = linea.ToString();

        lock (candado)
        {
            if (!IntentarEscribir(RutaArchivo, texto))
            {
                IntentarEscribir(rutaAlterna, texto);
            }
        }
    }

    private static bool IntentarEscribir(string ruta, string texto)
    {
        try
        {
            string? directorio = Path.GetDirectoryName(ruta);
            if (!string.IsNullOrEmpty(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            File.AppendAllText(ruta, texto, new UTF8Encoding(false));
            return true;
        }
        catch (IOException)
        {
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }
}
