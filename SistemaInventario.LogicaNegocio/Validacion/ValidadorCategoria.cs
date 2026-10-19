using System;
using SistemaInventario.Entidades;

namespace SistemaInventario.LogicaNegocio.Validacion;

/// <summary>Valida los campos de una <see cref="Categoria"/> y de sus fotos.</summary>
public sealed class ValidadorCategoria : IValidador<Categoria>
{
    private readonly IValidador<FotoCategoria> validadorFoto;

    /// <summary>Crea el validador con el validador de fotos (inyección por constructor).</summary>
    /// <param name="validadorFoto">Validador aplicado a cada foto.</param>
    public ValidadorCategoria(IValidador<FotoCategoria> validadorFoto)
    {
        this.validadorFoto = validadorFoto ?? throw new ArgumentNullException(nameof(validadorFoto));
    }

    /// <inheritdoc />
    public ResultadoValidacion Validar(Categoria entidad)
    {
        ResultadoValidacion resultado = new ResultadoValidacion();

        string nombre = (entidad.Nombre ?? string.Empty).Trim();
        if (nombre.Length == 0)
        {
            resultado.Agregar(nameof(Categoria.Nombre), "El nombre es obligatorio.");
        }
        else if (nombre.Length < Categoria.LongitudMinimaNombre || nombre.Length > Categoria.LongitudMaximaNombre)
        {
            resultado.Agregar(
                nameof(Categoria.Nombre),
                $"El nombre debe tener entre {Categoria.LongitudMinimaNombre} y {Categoria.LongitudMaximaNombre} caracteres.");
        }

        string descripcion = (entidad.Descripcion ?? string.Empty).Trim();
        if (descripcion.Length == 0)
        {
            resultado.Agregar(nameof(Categoria.Descripcion), "La descripción es obligatoria.");
        }
        else if (descripcion.Length > Categoria.LongitudMaximaDescripcion)
        {
            resultado.Agregar(
                nameof(Categoria.Descripcion),
                $"La descripción no puede exceder {Categoria.LongitudMaximaDescripcion} caracteres.");
        }

        if (entidad.Fotos.Count > Categoria.MaximoFotos)
        {
            resultado.Agregar(
                nameof(Categoria.Fotos),
                $"Una categoría admite como máximo {Categoria.MaximoFotos} fotos.");
        }

        foreach (FotoCategoria foto in entidad.Fotos)
        {
            ResultadoValidacion resultadoFoto = validadorFoto.Validar(foto);

            foreach (var error in resultadoFoto.Errores)
            {
                resultado.Agregar(nameof(Categoria.Fotos), $"{foto.NombreArchivo}: {error.Mensaje}");
            }
        }

        return resultado;
    }
}
