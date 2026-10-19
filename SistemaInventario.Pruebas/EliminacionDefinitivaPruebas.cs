using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class EliminacionDefinitivaPruebas
{
    private readonly Escenario escenario = new Escenario();

    [Fact]
    public void Categoria_Activa_NoSePuedeEliminarDefinitivamente()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");

        Assert.Throws<ReglaNegocioException>(() => escenario.ServicioCategorias.EliminarDefinitivo(categoria.Id));
        Assert.NotNull(escenario.Categorias.ObtenerPorId(categoria.Id));
    }

    [Fact]
    public void Categoria_EnPapeleraSinProductos_SeBorraDeVerdad()
    {
        Categoria categoria = escenario.NuevaCategoria("Deportes", activa: false);

        escenario.ServicioCategorias.EliminarDefinitivo(categoria.Id);

        Assert.Null(escenario.Categorias.ObtenerPorId(categoria.Id));
    }

    [Fact]
    public void Categoria_ConProductoEnPapelera_NoSePuedeEliminarDefinitivamente()
    {
        Categoria categoria = escenario.NuevaCategoria("Deportes", activa: false);
        Suplidor suplidor = escenario.NuevoSuplidor("Alfa Comercial");
        escenario.NuevoProducto("Balón", 725m, categoria, suplidor, activo: false);

        ReglaNegocioException ex = Assert.Throws<ReglaNegocioException>(
            () => escenario.ServicioCategorias.EliminarDefinitivo(categoria.Id));

        Assert.Contains("Elimine definitivamente esos productos", ex.Message);
        Assert.NotNull(escenario.Categorias.ObtenerPorId(categoria.Id));
    }

    [Fact]
    public void Suplidor_EnPapeleraSinProductos_SeBorraDeVerdad()
    {
        Suplidor suplidor = escenario.NuevoSuplidor("Proveedor Viejo", activo: false);

        escenario.ServicioSuplidores.EliminarDefinitivo(suplidor.Id);

        Assert.Null(escenario.Suplidores.ObtenerPorId(suplidor.Id));
    }

    [Fact]
    public void Suplidor_ConProductoEnPapelera_NoSePuedeEliminarDefinitivamente()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");
        Suplidor suplidor = escenario.NuevoSuplidor("Proveedor Viejo", activo: false);
        escenario.NuevoProducto("Radio", 540m, categoria, suplidor, activo: false);

        Assert.Throws<ReglaNegocioException>(() => escenario.ServicioSuplidores.EliminarDefinitivo(suplidor.Id));
    }

    [Fact]
    public void Producto_Activo_NoSePuedeEliminarDefinitivamente()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");
        Suplidor suplidor = escenario.NuevoSuplidor("Alfa Comercial");
        Producto producto = escenario.NuevoProducto("Cable", 100m, categoria, suplidor);

        Assert.Throws<ReglaNegocioException>(() => escenario.ServicioProductos.EliminarDefinitivo(producto.Id));
    }

    [Fact]
    public void Producto_EnPapelera_SeBorraDeVerdad()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");
        Suplidor suplidor = escenario.NuevoSuplidor("Alfa Comercial");
        Producto producto = escenario.NuevoProducto("Cable", 100m, categoria, suplidor, activo: false);

        escenario.ServicioProductos.EliminarDefinitivo(producto.Id);

        Assert.Null(escenario.Productos.ObtenerPorId(producto.Id));
    }

    [Fact]
    public void Producto_FacturadoEnPapelera_NoSePuedeEliminarDefinitivamente()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");
        Suplidor suplidor = escenario.NuevoSuplidor("Alfa Comercial");
        Producto producto = escenario.NuevoProducto("Cable", 100m, categoria, suplidor, activo: false);
        escenario.Productos.ProductosFacturados.Add(producto.Id);

        ReglaNegocioException ex = Assert.Throws<ReglaNegocioException>(
            () => escenario.ServicioProductos.EliminarDefinitivo(producto.Id));

        Assert.Contains("factura", ex.Message);
        Assert.NotNull(escenario.Productos.ObtenerPorId(producto.Id));
    }

    [Fact]
    public void RegistroInexistente_LanzaEntidadNoEncontrada()
    {
        Assert.Throws<EntidadNoEncontradaException>(() => escenario.ServicioCategorias.EliminarDefinitivo(999));
    }
}
