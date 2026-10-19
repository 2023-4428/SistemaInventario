using System;

namespace SistemaInventario.Comun.Excepciones;

/// <summary>
/// Se lanza cuando una operación incumple una regla de negocio
/// (por ejemplo, eliminar una categoría que tiene productos activos).
/// </summary>
public sealed class ReglaNegocioException : Exception
{
    /// <summary>Crea la excepción con un mensaje apto para mostrarse al usuario.</summary>
    /// <param name="mensaje">Descripción de la regla incumplida.</param>
    public ReglaNegocioException(string mensaje)
        : base(mensaje)
    {
    }
}
