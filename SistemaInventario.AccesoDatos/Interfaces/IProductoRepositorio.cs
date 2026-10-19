using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Interfaces;

/// <summary>Repositorio de productos.</summary>
public interface IProductoRepositorio : IRepositorio<Producto>
{
    /// <summary>Cuenta los productos activos de una categoría.</summary>
    /// <param name="categoriaId">Identificador de la categoría.</param>
    /// <returns>Cantidad de productos activos.</returns>
    int ContarActivosPorCategoria(int categoriaId);

    /// <summary>Cuenta los productos activos de un suplidor.</summary>
    /// <param name="suplidorId">Identificador del suplidor.</param>
    /// <returns>Cantidad de productos activos.</returns>
    int ContarActivosPorSuplidor(int suplidorId);

    /// <summary>Cuenta todos los productos de una categoría, activos o en la Papelera.</summary>
    /// <param name="categoriaId">Identificador de la categoría.</param>
    /// <returns>Cantidad de productos.</returns>
    int ContarPorCategoria(int categoriaId);

    /// <summary>Cuenta todos los productos de un suplidor, activos o en la Papelera.</summary>
    /// <param name="suplidorId">Identificador del suplidor.</param>
    /// <returns>Cantidad de productos.</returns>
    int ContarPorSuplidor(int suplidorId);

    /// <summary>Cuenta las líneas de factura (de cualquier factura) en las que aparece el producto.</summary>
    /// <param name="productoId">Identificador del producto.</param>
    /// <returns>Cantidad de líneas de factura.</returns>
    int ContarEnFacturas(int productoId);

    /// <summary>
    /// Indica si existe otro producto (activo o eliminado) con el mismo nombre, categoría y suplidor.
    /// </summary>
    /// <param name="nombre">Nombre a buscar.</param>
    /// <param name="categoriaId">Categoría del producto.</param>
    /// <param name="suplidorId">Suplidor del producto.</param>
    /// <param name="excluirId">Identificador a excluir de la búsqueda (0 al crear).</param>
    /// <returns><c>true</c> si ya existe un producto igual.</returns>
    bool ExisteDuplicado(string nombre, int categoriaId, int suplidorId, int excluirId);
}
