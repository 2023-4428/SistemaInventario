using System;
using System.Collections.Generic;
using System.Linq;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Entidades;

namespace SistemaInventario.Pruebas.Falsos;

/// <summary>Repositorio de productos en memoria.</summary>
public sealed class ProductoRepositorioFalso : RepositorioFalso<Producto>, IProductoRepositorio
{
    /// <summary>Productos que "aparecen en facturas" (para probar el borrado definitivo).</summary>
    public HashSet<int> ProductosFacturados { get; } = new HashSet<int>();

    public int ContarActivosPorCategoria(int categoriaId) => Datos.Count(p => p.Activo && p.CategoriaId == categoriaId);

    public int ContarActivosPorSuplidor(int suplidorId) => Datos.Count(p => p.Activo && p.SuplidorId == suplidorId);

    public int ContarPorCategoria(int categoriaId) => Datos.Count(p => p.CategoriaId == categoriaId);

    public int ContarPorSuplidor(int suplidorId) => Datos.Count(p => p.SuplidorId == suplidorId);

    public int ContarEnFacturas(int productoId) => ProductosFacturados.Contains(productoId) ? 1 : 0;

    public bool ExisteDuplicado(string nombre, int categoriaId, int suplidorId, int excluirId)
    {
        return Datos.Any(p =>
            p.Id != excluirId &&
            p.CategoriaId == categoriaId &&
            p.SuplidorId == suplidorId &&
            string.Equals(p.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
    }
}
