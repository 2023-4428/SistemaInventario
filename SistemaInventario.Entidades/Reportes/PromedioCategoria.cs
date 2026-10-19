namespace SistemaInventario.Entidades.Reportes;

/// <summary>Fila del reporte "promedio de precios por categoría".</summary>
/// <param name="Categoria">Nombre de la categoría.</param>
/// <param name="CantidadProductos">Cantidad de productos considerados.</param>
/// <param name="PrecioPromedio">Precio promedio, redondeado a 2 decimales.</param>
/// <param name="PrecioMinimo">Precio más bajo de la categoría.</param>
/// <param name="PrecioMaximo">Precio más alto de la categoría.</param>
public sealed record PromedioCategoria(
    string Categoria,
    int CantidadProductos,
    decimal PrecioPromedio,
    decimal PrecioMinimo,
    decimal PrecioMaximo);
