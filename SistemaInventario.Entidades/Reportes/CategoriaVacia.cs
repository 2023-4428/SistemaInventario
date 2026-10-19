namespace SistemaInventario.Entidades.Reportes;

/// <summary>Fila del reporte "categorías sin productos activos".</summary>
/// <param name="Categoria">Nombre de la categoría.</param>
/// <param name="Descripcion">Descripción de la categoría.</param>
public sealed record CategoriaVacia(string Categoria, string Descripcion);
