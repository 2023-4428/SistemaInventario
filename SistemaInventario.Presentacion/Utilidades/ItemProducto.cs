using System.Globalization;
using SistemaInventario.Entidades;

namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>Elemento de la lista de productos de la ventana de facturación (muestra nombre y precio).</summary>
internal sealed class ItemProducto
{
    /// <summary>Crea el elemento.</summary>
    /// <param name="producto">Producto que representa.</param>
    public ItemProducto(Producto producto)
    {
        Producto = producto;
    }

    /// <summary>Producto que representa el elemento.</summary>
    public Producto Producto { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Producto.Nombre + "  —  RD$ " + Producto.PrecioUnitario.ToString("N2", CultureInfo.CurrentCulture);
    }
}
