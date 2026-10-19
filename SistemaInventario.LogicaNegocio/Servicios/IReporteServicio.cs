using System.Collections.Generic;
using SistemaInventario.Entidades;
using SistemaInventario.Entidades.Reportes;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>Reportes basados en consultas LINQ sobre los productos, categorías y suplidores activos.</summary>
public interface IReporteServicio
{
    /// <summary>Productos agrupados por categoría con su conteo (incluye categorías sin productos).</summary>
    /// <returns>Una fila por categoría, de mayor a menor cantidad.</returns>
    IReadOnlyList<ConteoCategoria> ProductosAgrupadosPorCategoria();

    /// <summary>Suplidores con más productos.</summary>
    /// <param name="cantidad">Cantidad de posiciones a devolver (por defecto, 5).</param>
    /// <returns>Ranking de suplidores.</returns>
    IReadOnlyList<RankingSuplidor> TopSuplidoresPorProductos(int cantidad = 5);

    /// <summary>Productos dentro de un rango de precios, opcionalmente filtrados por categoría.</summary>
    /// <param name="precioMinimo">Precio mínimo inclusive; <c>null</c> para no limitar.</param>
    /// <param name="precioMaximo">Precio máximo inclusive; <c>null</c> para no limitar.</param>
    /// <param name="categoriaId">Categoría a filtrar; <c>null</c> para todas.</param>
    /// <returns>Productos ordenados por precio.</returns>
    IReadOnlyList<Producto> ProductosPorRangoDePrecio(decimal? precioMinimo, decimal? precioMaximo, int? categoriaId);

    /// <summary>Promedio, mínimo y máximo de precios por categoría.</summary>
    /// <returns>Una fila por categoría con productos.</returns>
    IReadOnlyList<PromedioCategoria> PromedioPreciosPorCategoria();

    /// <summary>El producto más caro de cada categoría.</summary>
    /// <returns>Una fila por categoría con productos.</returns>
    IReadOnlyList<ProductoDestacado> ProductoMasCaroPorCategoria();

    /// <summary>Categorías activas que no tienen productos activos.</summary>
    /// <returns>Categorías vacías.</returns>
    IReadOnlyList<CategoriaVacia> CategoriasSinProductos();
}
