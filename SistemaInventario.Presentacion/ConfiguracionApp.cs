using System;
using System.IO;
using System.Text.Json;

namespace SistemaInventario.Presentacion;

/// <summary>Lee la configuración de la aplicación desde <c>appsettings.json</c> (junto al ejecutable).</summary>
internal static class ConfiguracionApp
{
    private const string NombreArchivo = "appsettings.json";
    private const string NombreCadena = "SistemaInventario";

    /// <summary>Obtiene la cadena de conexión <c>ConnectionStrings:SistemaInventario</c>.</summary>
    /// <returns>La cadena de conexión de SQL Server.</returns>
    /// <exception cref="FileNotFoundException">Si no existe appsettings.json.</exception>
    /// <exception cref="InvalidOperationException">Si el archivo es inválido o no contiene la cadena.</exception>
    public static string ObtenerCadenaConexion()
    {
        string ruta = Path.Combine(AppContext.BaseDirectory, NombreArchivo);

        if (!File.Exists(ruta))
        {
            throw new FileNotFoundException(
                "No se encontró el archivo " + NombreArchivo + " junto al ejecutable.", ruta);
        }

        try
        {
            using FileStream flujo = File.OpenRead(ruta);
            using JsonDocument documento = JsonDocument.Parse(flujo);

            if (documento.RootElement.TryGetProperty("ConnectionStrings", out JsonElement cadenas)
                && cadenas.TryGetProperty(NombreCadena, out JsonElement valor)
                && valor.ValueKind == JsonValueKind.String)
            {
                string? cadena = valor.GetString();

                if (!string.IsNullOrWhiteSpace(cadena))
                {
                    return cadena;
                }
            }
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                NombreArchivo + " no tiene un formato JSON válido: " + ex.Message, ex);
        }

        throw new InvalidOperationException(
            "Falta la cadena de conexión ConnectionStrings:" + NombreCadena + " en " + NombreArchivo + ".");
    }
}
