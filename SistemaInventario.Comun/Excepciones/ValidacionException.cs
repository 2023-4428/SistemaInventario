using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaInventario.Comun.Excepciones;

/// <summary>Se lanza cuando los datos de una entidad no cumplen las reglas de validación.</summary>
public sealed class ValidacionException : Exception
{
    /// <summary>Crea la excepción a partir de una lista de errores.</summary>
    /// <param name="errores">Errores detectados (al menos uno).</param>
    public ValidacionException(IReadOnlyList<ErrorValidacion> errores)
        : base(ResumirErrores(errores))
    {
        Errores = errores;
    }

    /// <summary>Crea la excepción para un único error.</summary>
    /// <param name="campo">Campo con el error.</param>
    /// <param name="mensaje">Mensaje del error.</param>
    public ValidacionException(string campo, string mensaje)
        : this(new List<ErrorValidacion> { new ErrorValidacion(campo, mensaje) })
    {
    }

    /// <summary>Errores de validación detectados.</summary>
    public IReadOnlyList<ErrorValidacion> Errores { get; }

    private static string ResumirErrores(IReadOnlyList<ErrorValidacion> errores)
    {
        return string.Join(Environment.NewLine, errores.Select(e => "• " + e.Mensaje));
    }
}
