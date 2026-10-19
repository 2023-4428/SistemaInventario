using System;

namespace SistemaInventario.Comun;

/// <summary>
/// Implementación de <see cref="ILogger"/> que no hace nada (patrón Null Object).
/// Útil en pruebas unitarias y en cualquier contexto donde no se desee registrar.
/// </summary>
public sealed class LoggerNulo : ILogger
{
    /// <inheritdoc />
    public string RutaArchivo => string.Empty;

    /// <inheritdoc />
    public void Error(string mensaje, Exception? excepcion = null)
    {
    }

    /// <inheritdoc />
    public void Advertencia(string mensaje)
    {
    }

    /// <inheritdoc />
    public void Informacion(string mensaje)
    {
    }
}
