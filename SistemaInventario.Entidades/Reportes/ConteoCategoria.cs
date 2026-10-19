namespace SistemaInventario.Entidades.Reportes;

/// <summary>Fila del reporte "productos agrupados por categoría con conteo".</summary>
/// <param name="Categoria">Nombre de la categoría.</param>
/// <param name="CantidadProductos">Cantidad de productos activos de la categoría.</param>
public sealed record ConteoCategoria(string Categoria, int CantidadProductos);
