using System;

namespace SistemaInventario.Comun;

/// <summary>
/// Contrato de registro (log) compartido por todas las capas de la solución.
/// Las capas reciben una implementación por constructor (inyección de dependencias),
/// de modo que ninguna depende de una clase concreta de registro.
/// </summary>
public interface ILogger
{
    /// <summary>Ruta completa del archivo donde se escriben los registros.</summary>
    string RutaArchivo { get; }

    /// <summary>Registra un error, opcionalmente con la excepción que lo originó.</summary>
    /// <param name="mensaje">Descripción del error.</param>
    /// <param name="excepcion">Excepción asociada (puede ser <c>null</c>).</param>
    void Error(string mensaje, Exception? excepcion = null);

    /// <summary>Registra una advertencia.</summary>
    /// <param name="mensaje">Descripción de la advertencia.</param>
    void Advertencia(string mensaje);

    /// <summary>Registra un mensaje informativo.</summary>
    /// <param name="mensaje">Texto informativo.</param>
    void Informacion(string mensaje);
}
