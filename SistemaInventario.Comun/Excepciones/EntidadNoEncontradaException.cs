using System;

namespace SistemaInventario.Comun.Excepciones;

/// <summary>Se lanza cuando se busca un registro por identificador y no existe.</summary>
public sealed class EntidadNoEncontradaException : Exception
{
    /// <summary>Crea la excepción indicando el tipo de entidad y su identificador.</summary>
    /// <param name="entidad">Nombre legible de la entidad (por ejemplo, "categoría").</param>
    /// <param name="id">Identificador que no se encontró.</param>
    public EntidadNoEncontradaException(string entidad, int id)
        : base($"No se encontró el registro de {entidad} con identificador {id}.")
    {
        Entidad = entidad;
        Id = id;
    }

    /// <summary>Nombre legible de la entidad buscada.</summary>
    public string Entidad { get; }

    /// <summary>Identificador que no se encontró.</summary>
    public int Id { get; }
}
