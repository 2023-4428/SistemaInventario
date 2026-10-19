using System.Collections.Generic;
using System.Linq;
using SistemaInventario.Comun.Excepciones;

namespace SistemaInventario.LogicaNegocio.Validacion;

/// <summary>Acumula los errores de validación encontrados en una entidad.</summary>
public sealed class ResultadoValidacion
{
    private readonly List<ErrorValidacion> errores = new List<ErrorValidacion>();

    /// <summary>Indica si no se encontró ningún error.</summary>
    public bool EsValido => errores.Count == 0;

    /// <summary>Errores encontrados.</summary>
    public IReadOnlyList<ErrorValidacion> Errores => errores;

    /// <summary>Agrega un error asociado a un campo.</summary>
    /// <param name="campo">Nombre del campo (propiedad) con el error.</param>
    /// <param name="mensaje">Mensaje para el usuario.</param>
    /// <returns>El mismo resultado, para encadenar llamadas.</returns>
    public ResultadoValidacion Agregar(string campo, string mensaje)
    {
        errores.Add(new ErrorValidacion(campo, mensaje));
        return this;
    }

    /// <summary>Indica si ya existe algún error para el campo dado.</summary>
    /// <param name="campo">Nombre del campo.</param>
    /// <returns><c>true</c> si el campo tiene al menos un error.</returns>
    public bool TieneErroresEn(string campo) => errores.Any(e => e.Campo == campo);

    /// <summary>Lanza <see cref="ValidacionException"/> si hay errores.</summary>
    /// <exception cref="ValidacionException">Cuando existe al menos un error.</exception>
    public void LanzarSiHayErrores()
    {
        if (!EsValido)
        {
            throw new ValidacionException(errores.ToList());
        }
    }
}
