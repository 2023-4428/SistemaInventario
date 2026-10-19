using System.Collections.Generic;
using SistemaInventario.Entidades;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>Servicio de negocio de productos.</summary>
public interface IProductoServicio : IServicio<Producto>
{
    /// <summary>
    /// Filtra en memoria una lista de productos por categoría, suplidor, rango de precios y texto.
    /// </summary>
    /// <param name="fuente">Productos a filtrar.</param>
    /// <param name="filtro">Criterios a aplicar.</param>
    /// <returns>Productos que cumplen todos los criterios.</returns>
    IReadOnlyList<Producto> Filtrar(IEnumerable<Producto> fuente, FiltroProducto filtro);
}
