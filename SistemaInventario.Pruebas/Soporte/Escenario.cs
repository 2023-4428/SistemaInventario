using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.LogicaNegocio.Validacion;
using SistemaInventario.Pruebas.Falsos;
using SistemaInventario.Entidades;

namespace SistemaInventario.Pruebas.Soporte;

/// <summary>Arma los servicios reales sobre repositorios en memoria (inyección por constructor).</summary>
public sealed class Escenario
{
    public Escenario()
    {
        ValidadorFoto validadorFoto = new ValidadorFoto();

        Categorias = new CategoriaRepositorioFalso();
        Suplidores = new SuplidorRepositorioFalso();
        Productos = new ProductoRepositorioFalso();
        Facturas = new FacturaRepositorioFalso();

        ServicioCategorias = new CategoriaServicio(Categorias, Productos, new ValidadorCategoria(validadorFoto), validadorFoto);
        ServicioSuplidores = new SuplidorServicio(Suplidores, Productos, new ValidadorSuplidor());
        ServicioProductos = new ProductoServicio(Productos, Categorias, Suplidores, new ValidadorProducto());
        ServicioReportes = new ReporteServicio(Productos, Categorias, Suplidores);
        ServicioFacturas = new FacturaServicio(Facturas, Productos, new ValidadorFactura());
    }

    public CategoriaRepositorioFalso Categorias { get; }
    public SuplidorRepositorioFalso Suplidores { get; }
    public ProductoRepositorioFalso Productos { get; }
    public FacturaRepositorioFalso Facturas { get; }
    public CategoriaServicio ServicioCategorias { get; }
    public SuplidorServicio ServicioSuplidores { get; }
    public ProductoServicio ServicioProductos { get; }
    public ReporteServicio ServicioReportes { get; }
    public FacturaServicio ServicioFacturas { get; }

    /// <summary>Agrega una categoría directamente al repositorio.</summary>
    public Categoria NuevaCategoria(string nombre, bool activa = true) =>
        Categorias.Agregar(ConstructorDatos.CategoriaValida(nombre), activa);

    /// <summary>Agrega un suplidor directamente al repositorio.</summary>
    public Suplidor NuevoSuplidor(string empresa, bool activo = true) =>
        Suplidores.Agregar(ConstructorDatos.SuplidorValido(empresa), activo);

    /// <summary>Agrega un producto (con los nombres de categoría y suplidor que el JOIN real devolvería).</summary>
    public Producto NuevoProducto(string nombre, decimal precio, Categoria categoria, Suplidor suplidor, bool activo = true)
    {
        Producto producto = ConstructorDatos.ProductoValido(categoria.Id, suplidor.Id, nombre, precio);
        producto.CategoriaNombre = categoria.Nombre;
        producto.SuplidorNombre = suplidor.NombreEmpresa;
        return Productos.Agregar(producto, activo);
    }
}
