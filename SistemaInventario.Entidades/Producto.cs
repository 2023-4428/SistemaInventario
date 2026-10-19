namespace SistemaInventario.Entidades;

/// <summary>Producto del inventario, asociado a una categoría y a un suplidor.</summary>
public class Producto : EntidadBase
{
    /// <summary>Longitud mínima del nombre.</summary>
    public const int LongitudMinimaNombre = 2;

    /// <summary>Longitud máxima del nombre.</summary>
    public const int LongitudMaximaNombre = 150;

    /// <summary>Precio máximo permitido (evita desbordar el tipo DECIMAL(18,2) y errores de digitación).</summary>
    public const decimal PrecioMaximo = 99999999.99m;

    /// <summary>Nombre del producto (obligatorio).</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Precio unitario (mayor a 0 y con máximo 2 decimales).</summary>
    public decimal PrecioUnitario { get; set; }

    /// <summary>Identificador de la categoría asociada (obligatoria y activa).</summary>
    public int CategoriaId { get; set; }

    /// <summary>Identificador del suplidor asociado (obligatorio y activo).</summary>
    public int SuplidorId { get; set; }

    /// <summary>Nombre de la categoría. Dato de solo lectura que se obtiene con un JOIN al consultar.</summary>
    public string CategoriaNombre { get; set; } = string.Empty;

    /// <summary>Nombre de la empresa suplidora. Dato de solo lectura que se obtiene con un JOIN al consultar.</summary>
    public string SuplidorNombre { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString() => Nombre;
}
