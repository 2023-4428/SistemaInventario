namespace SistemaInventario.Entidades.Reportes;

/// <summary>Fila del reporte "producto más caro de cada categoría".</summary>
/// <param name="Categoria">Nombre de la categoría.</param>
/// <param name="Producto">Nombre del producto más caro.</param>
/// <param name="Precio">Precio unitario del producto.</param>
/// <param name="Suplidor">Nombre de la empresa suplidora.</param>
public sealed record ProductoDestacado(string Categoria, string Producto, decimal Precio, string Suplidor);
