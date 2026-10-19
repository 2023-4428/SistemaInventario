namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>Criterios de filtrado del listado de productos. Un criterio en <c>null</c> no se aplica.</summary>
public sealed class FiltroProducto
{
    /// <summary>Texto de búsqueda parcial (nombre, categoría, suplidor o precio).</summary>
    public string? Texto { get; set; }

    /// <summary>Solo productos de esta categoría.</summary>
    public int? CategoriaId { get; set; }

    /// <summary>Solo productos de este suplidor.</summary>
    public int? SuplidorId { get; set; }

    /// <summary>Precio mínimo (inclusive).</summary>
    public decimal? PrecioMinimo { get; set; }

    /// <summary>Precio máximo (inclusive).</summary>
    public decimal? PrecioMaximo { get; set; }
}
