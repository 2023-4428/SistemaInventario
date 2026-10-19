using System;

namespace SistemaInventario.Entidades;

/// <summary>
/// Línea de una factura: un producto, la cantidad vendida y el precio con el que se facturó.
/// El precio y el nombre se copian del producto al facturar para que la factura no cambie si luego se modifica el producto.
/// </summary>
public class FacturaDetalle
{
    /// <summary>Cantidad máxima por línea.</summary>
    public const int CantidadMaxima = 9999;

    /// <summary>Identificador de la línea. Vale 0 mientras no se ha guardado.</summary>
    public int Id { get; set; }

    /// <summary>Identificador de la factura a la que pertenece.</summary>
    public int FacturaId { get; set; }

    /// <summary>Identificador del producto facturado.</summary>
    public int ProductoId { get; set; }

    /// <summary>Nombre del producto (dato de solo lectura que se obtiene con un JOIN al consultar).</summary>
    public string ProductoNombre { get; set; } = string.Empty;

    /// <summary>Cantidad vendida (mayor a 0).</summary>
    public int Cantidad { get; set; }

    /// <summary>Precio unitario con el que se facturó.</summary>
    public decimal PrecioUnitario { get; set; }

    /// <summary>Importe de la línea: cantidad por precio unitario, redondeado a 2 decimales.</summary>
    public decimal Subtotal => Math.Round(Cantidad * PrecioUnitario, 2, MidpointRounding.AwayFromZero);
}
