using System;
using System.Collections.Generic;
using System.Linq;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Entidades;
using SistemaInventario.Entidades.Reportes;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>
/// Implementa los reportes con consultas LINQ no triviales
/// (join/group join, agrupaciones, agregados, ordenamientos compuestos y paginación con <c>Take</c>).
/// </summary>
public sealed class ReporteServicio : IReporteServicio
{
    private readonly IProductoRepositorio productos;
    private readonly ICategoriaRepositorio categorias;
    private readonly ISuplidorRepositorio suplidores;

    /// <summary>Crea el servicio con sus dependencias (inyección por constructor).</summary>
    /// <param name="productos">Repositorio de productos.</param>
    /// <param name="categorias">Repositorio de categorías.</param>
    /// <param name="suplidores">Repositorio de suplidores.</param>
    public ReporteServicio(
        IProductoRepositorio productos,
        ICategoriaRepositorio categorias,
        ISuplidorRepositorio suplidores)
    {
        this.productos = productos ?? throw new ArgumentNullException(nameof(productos));
        this.categorias = categorias ?? throw new ArgumentNullException(nameof(categorias));
        this.suplidores = suplidores ?? throw new ArgumentNullException(nameof(suplidores));
    }

    /// <summary>
    /// LINQ #1 — Productos agrupados por categoría con conteo.
    /// Usa <c>join ... into</c> (group join) para incluir también las categorías sin productos.
    /// </summary>
    /// <returns>Una fila por categoría, de mayor a menor cantidad.</returns>
    public IReadOnlyList<ConteoCategoria> ProductosAgrupadosPorCategoria()
    {
        IReadOnlyList<Categoria> listaCategorias = categorias.ObtenerActivos();
        IReadOnlyList<Producto> listaProductos = productos.ObtenerActivos();

        return (from categoria in listaCategorias
                join producto in listaProductos on categoria.Id equals producto.CategoriaId into grupo
                let cantidad = grupo.Count()
                orderby cantidad descending, categoria.Nombre
                select new ConteoCategoria(categoria.Nombre, cantidad)).ToList();
    }

    /// <summary>
    /// LINQ #2 — Top N suplidores con más productos.
    /// Usa <c>GroupJoin</c>, ordenamiento compuesto (cantidad descendente y nombre), <c>Take</c> y un
    /// <c>Select</c> con índice para numerar la posición.
    /// </summary>
    /// <param name="cantidad">Cantidad de posiciones a devolver.</param>
    /// <returns>Ranking de suplidores.</returns>
    public IReadOnlyList<RankingSuplidor> TopSuplidoresPorProductos(int cantidad = 5)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad debe ser mayor que cero.");
        }

        IReadOnlyList<Suplidor> listaSuplidores = suplidores.ObtenerActivos();
        IReadOnlyList<Producto> listaProductos = productos.ObtenerActivos();

        return listaSuplidores
            .GroupJoin(
                listaProductos,
                suplidor => suplidor.Id,
                producto => producto.SuplidorId,
                (suplidor, grupo) => new { suplidor.NombreEmpresa, Cantidad = grupo.Count() })
            .OrderByDescending(fila => fila.Cantidad)
            .ThenBy(fila => fila.NombreEmpresa)
            .Take(cantidad)
            .Select((fila, indice) => new RankingSuplidor(indice + 1, fila.NombreEmpresa, fila.Cantidad))
            .ToList();
    }

    /// <summary>
    /// LINQ #3 — Productos en un rango de precios filtrados por categoría.
    /// La consulta se compone de forma dinámica según los criterios recibidos.
    /// </summary>
    /// <param name="precioMinimo">Precio mínimo inclusive; <c>null</c> para no limitar.</param>
    /// <param name="precioMaximo">Precio máximo inclusive; <c>null</c> para no limitar.</param>
    /// <param name="categoriaId">Categoría a filtrar; <c>null</c> para todas.</param>
    /// <returns>Productos ordenados por precio.</returns>
    public IReadOnlyList<Producto> ProductosPorRangoDePrecio(decimal? precioMinimo, decimal? precioMaximo, int? categoriaId)
    {
        IEnumerable<Producto> consulta = productos.ObtenerActivos();

        if (categoriaId.HasValue)
        {
            consulta = consulta.Where(producto => producto.CategoriaId == categoriaId.Value);
        }

        if (precioMinimo.HasValue)
        {
            consulta = consulta.Where(producto => producto.PrecioUnitario >= precioMinimo.Value);
        }

        if (precioMaximo.HasValue)
        {
            consulta = consulta.Where(producto => producto.PrecioUnitario <= precioMaximo.Value);
        }

        return consulta
            .OrderBy(producto => producto.PrecioUnitario)
            .ThenBy(producto => producto.Nombre)
            .ToList();
    }

    /// <summary>
    /// LINQ #5 — Promedio de precios por categoría.
    /// Agrupa por una clave compuesta (anónima) y calcula Count, Average, Min y Max.
    /// </summary>
    /// <returns>Una fila por categoría con productos.</returns>
    public IReadOnlyList<PromedioCategoria> PromedioPreciosPorCategoria()
    {
        return productos.ObtenerActivos()
            .GroupBy(producto => new { producto.CategoriaId, producto.CategoriaNombre })
            .Select(grupo => new PromedioCategoria(
                grupo.Key.CategoriaNombre,
                grupo.Count(),
                Math.Round(grupo.Average(producto => producto.PrecioUnitario), 2),
                grupo.Min(producto => producto.PrecioUnitario),
                grupo.Max(producto => producto.PrecioUnitario)))
            .OrderByDescending(fila => fila.PrecioPromedio)
            .ThenBy(fila => fila.Categoria)
            .ToList();
    }

    /// <summary>
    /// LINQ #6 — El producto más caro de cada categoría.
    /// Agrupa por categoría y toma el primer elemento de cada grupo ordenado por precio descendente.
    /// </summary>
    /// <returns>Una fila por categoría con productos.</returns>
    public IReadOnlyList<ProductoDestacado> ProductoMasCaroPorCategoria()
    {
        return productos.ObtenerActivos()
            .GroupBy(producto => producto.CategoriaId)
            .Select(grupo => grupo
                .OrderByDescending(producto => producto.PrecioUnitario)
                .ThenBy(producto => producto.Nombre)
                .First())
            .OrderBy(producto => producto.CategoriaNombre)
            .Select(producto => new ProductoDestacado(
                producto.CategoriaNombre,
                producto.Nombre,
                producto.PrecioUnitario,
                producto.SuplidorNombre))
            .ToList();
    }

    /// <summary>
    /// LINQ #7 — Categorías sin productos activos.
    /// Usa <c>GroupJoin</c> y filtra los grupos vacíos (equivalente a un LEFT JOIN con IS NULL).
    /// </summary>
    /// <returns>Categorías vacías ordenadas por nombre.</returns>
    public IReadOnlyList<CategoriaVacia> CategoriasSinProductos()
    {
        IReadOnlyList<Producto> listaProductos = productos.ObtenerActivos();

        return categorias.ObtenerActivos()
            .GroupJoin(
                listaProductos,
                categoria => categoria.Id,
                producto => producto.CategoriaId,
                (categoria, grupo) => new { Categoria = categoria, Cantidad = grupo.Count() })
            .Where(fila => fila.Cantidad == 0)
            .OrderBy(fila => fila.Categoria.Nombre)
            .Select(fila => new CategoriaVacia(fila.Categoria.Nombre, fila.Categoria.Descripcion))
            .ToList();
    }
}
