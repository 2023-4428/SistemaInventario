using System;
using System.IO;

namespace SistemaInventario.Entidades;

/// <summary>Fotografía asociada a una <see cref="Categoria"/>. El contenido se guarda en la base de datos.</summary>
public class FotoCategoria : EntidadBase
{
    /// <summary>Identificador de la categoría a la que pertenece la foto.</summary>
    public int CategoriaId { get; set; }

    /// <summary>Nombre original del archivo (incluye la extensión).</summary>
    public string NombreArchivo { get; set; } = string.Empty;

    /// <summary>Bytes de la imagen.</summary>
    public byte[] Contenido { get; set; } = Array.Empty<byte>();

    /// <summary>Tamaño de la imagen en bytes.</summary>
    public long TamanoBytes => Contenido.LongLength;

    /// <summary>Extensión del archivo en minúsculas y con punto (por ejemplo, ".png").</summary>
    public string Extension => Path.GetExtension(NombreArchivo ?? string.Empty).ToLowerInvariant();

    /// <inheritdoc />
    public override string ToString() => NombreArchivo;
}
