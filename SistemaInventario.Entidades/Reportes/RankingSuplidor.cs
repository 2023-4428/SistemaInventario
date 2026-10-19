namespace SistemaInventario.Entidades.Reportes;

/// <summary>Fila del reporte "top de suplidores con más productos".</summary>
/// <param name="Posicion">Posición en el ranking (1 es el mejor).</param>
/// <param name="Suplidor">Nombre de la empresa suplidora.</param>
/// <param name="CantidadProductos">Cantidad de productos activos que suministra.</param>
public sealed record RankingSuplidor(int Posicion, string Suplidor, int CantidadProductos);
