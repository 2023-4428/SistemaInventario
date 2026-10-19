using System;
using System.Collections.Generic;

namespace SistemaInventario.Entidades;

/// <summary>Categoría que agrupa productos. Puede tener hasta <see cref="MaximoFotos"/> fotos.</summary>
public class Categoria : EntidadBase
{
    /// <summary>Longitud mínima permitida para el nombre.</summary>
    public const int LongitudMinimaNombre = 3;

    /// <summary>Longitud máxima permitida para el nombre.</summary>
    public const int LongitudMaximaNombre = 100;

    /// <summary>Longitud máxima permitida para la descripción.</summary>
    public const int LongitudMaximaDescripcion = 500;

    /// <summary>Cantidad máxima de fotos asociadas a una categoría.</summary>
    public const int MaximoFotos = 5;

    private readonly List<FotoCategoria> fotos = new List<FotoCategoria>();

    /// <summary>Nombre de la categoría (obligatorio, único, 3 a 100 caracteres).</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Descripción de la categoría (obligatoria, máximo 500 caracteres).</summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Cantidad de fotos activas. Es un dato de resumen que se llena al listar categorías,
    /// para no cargar los bytes de las imágenes en los listados.
    /// </summary>
    public int CantidadFotos { get; set; }

    /// <summary>Fotos de la categoría (solo se cargan al consultar una categoría por su identificador).</summary>
    public IReadOnlyList<FotoCategoria> Fotos => fotos;

    /// <summary>Agrega una foto a la categoría.</summary>
    /// <param name="foto">Foto a agregar.</param>
    public void AgregarFoto(FotoCategoria foto)
    {
        if (foto == null)
        {
            throw new ArgumentNullException(nameof(foto));
        }

        fotos.Add(foto);
    }

    /// <summary>Quita una foto de la categoría.</summary>
    /// <param name="foto">Foto a quitar.</param>
    /// <returns><c>true</c> si la foto estaba en la categoría.</returns>
    public bool QuitarFoto(FotoCategoria foto) => fotos.Remove(foto);

    /// <summary>Reemplaza todas las fotos por las indicadas.</summary>
    /// <param name="nuevasFotos">Conjunto final de fotos de la categoría.</param>
    public void EstablecerFotos(IEnumerable<FotoCategoria> nuevasFotos)
    {
        if (nuevasFotos == null)
        {
            throw new ArgumentNullException(nameof(nuevasFotos));
        }

        List<FotoCategoria> copia = new List<FotoCategoria>(nuevasFotos);
        fotos.Clear();
        fotos.AddRange(copia);
    }

    /// <inheritdoc />
    public override string ToString() => Nombre;
}
