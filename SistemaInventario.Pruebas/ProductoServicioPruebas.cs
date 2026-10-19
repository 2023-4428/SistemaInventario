using System.Linq;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ProductoServicioPruebas
{
    private readonly Escenario escenario = new Escenario();
    private readonly Categoria electronica;
    private readonly Categoria hogar;
    private readonly Suplidor proveedorA;
    private readonly Suplidor proveedorB;

    public ProductoServicioPruebas()
    {
        electronica = escenario.NuevaCategoria("Electrónica");
        hogar = escenario.NuevaCategoria("Hogar");
        proveedorA = escenario.NuevoSuplidor("Alfa Comercial");
        proveedorB = escenario.NuevoSuplidor("Beta Importaciones");
    }

    private ProductoServicio Servicio => escenario.ServicioProductos;

    [Fact]
    public void Crear_ConCategoriaYSuplidorActivos_LoGuarda()
    {
        int id = Servicio.Crear(ConstructorDatos.ProductoValido(electronica.Id, proveedorA.Id, "  Cable HDMI  ", 275.5m));

        Producto guardado = Servicio.ObtenerPorId(id);
        Assert.Equal("Cable HDMI", guardado.Nombre);
        Assert.Equal(275.5m, guardado.PrecioUnitario);
    }

    [Fact]
    public void Crear_ConCategoriaEliminada_LanzaValidacionEnCategoria()
    {
        Categoria eliminada = escenario.NuevaCategoria("Deportes", activa: false);

        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
        {
            Servicio.Crear(ConstructorDatos.ProductoValido(eliminada.Id, proveedorA.Id));
        });

        Assert.Equal(nameof(Producto.CategoriaId), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Crear_ConSuplidorEliminado_LanzaValidacionEnSuplidor()
    {
        Suplidor eliminado = escenario.NuevoSuplidor("Proveedor Viejo", activo: false);

        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
        {
            Servicio.Crear(ConstructorDatos.ProductoValido(electronica.Id, eliminado.Id));
        });

        Assert.Equal(nameof(Producto.SuplidorId), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Crear_ConCategoriaInexistente_LanzaValidacion()
    {
        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
        {
            Servicio.Crear(ConstructorDatos.ProductoValido(999, proveedorA.Id));
        });

        Assert.Equal(nameof(Producto.CategoriaId), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Crear_ConPrecioCeroYSinSeleccionarCategoria_ReportaLosDosErrores()
    {
        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
        {
            Servicio.Crear(ConstructorDatos.ProductoValido(0, proveedorA.Id, "Cable", 0m));
        });

        Assert.Contains(ex.Errores, e => e.Campo == nameof(Producto.PrecioUnitario));
        Assert.Contains(ex.Errores, e => e.Campo == nameof(Producto.CategoriaId));
        Assert.Empty(escenario.Productos.ObtenerActivos());
    }

    [Fact]
    public void Actualizar_CambiandoACategoriaEliminada_LanzaValidacion()
    {
        Producto existente = escenario.NuevoProducto("Cable", 100m, electronica, proveedorA);
        Categoria eliminada = escenario.NuevaCategoria("Deportes", activa: false);
        Producto cambios = ConstructorDatos.ProductoValido(eliminada.Id, proveedorA.Id, "Cable", 100m);
        cambios.Id = existente.Id;

        Assert.Throws<ValidacionException>(() => { Servicio.Actualizar(cambios); });
    }

    [Fact]
    public void Eliminar_YRestaurar_ConCategoriaYSuplidorActivos_Funciona()
    {
        Producto producto = escenario.NuevoProducto("Cable", 100m, electronica, proveedorA);

        Servicio.Eliminar(producto.Id);
        Assert.Single(Servicio.ObtenerEliminados());

        Servicio.Restaurar(producto.Id);
        Assert.Single(Servicio.ObtenerActivos());
    }

    [Fact]
    public void Restaurar_SiLaCategoriaEstaEliminada_LanzaReglaDeNegocioConElNombre()
    {
        Categoria deportes = escenario.NuevaCategoria("Deportes", activa: false);
        Producto balon = escenario.NuevoProducto("Balón", 725m, deportes, proveedorA, activo: false);

        ReglaNegocioException ex = Assert.Throws<ReglaNegocioException>(() => { Servicio.Restaurar(balon.Id); });

        Assert.Contains("Deportes", ex.Message);
        Assert.Empty(Servicio.ObtenerActivos());
    }

    [Fact]
    public void Restaurar_SiElSuplidorEstaEliminado_LanzaReglaDeNegocio()
    {
        Suplidor viejo = escenario.NuevoSuplidor("Proveedor Viejo", activo: false);
        Producto producto = escenario.NuevoProducto("Radio", 540m, electronica, viejo, activo: false);

        ReglaNegocioException ex = Assert.Throws<ReglaNegocioException>(() => { Servicio.Restaurar(producto.Id); });

        Assert.Contains("Proveedor Viejo", ex.Message);
    }

    private void CargarCatalogo()
    {
        escenario.NuevoProducto("Cable HDMI", 275m, electronica, proveedorA);
        escenario.NuevoProducto("Mouse Inalámbrico", 795m, electronica, proveedorA);
        escenario.NuevoProducto("Bocina Portátil", 2450m, electronica, proveedorB);
        escenario.NuevoProducto("Licuadora", 3200m, hogar, proveedorB);
        escenario.NuevoProducto("Organizador", 420m, hogar, proveedorA);
    }

    [Fact]
    public void Filtrar_SinCriterios_DevuelveTodos()
    {
        CargarCatalogo();

        Assert.Equal(5, Servicio.Filtrar(Servicio.ObtenerActivos(), new FiltroProducto()).Count);
    }

    [Fact]
    public void Filtrar_PorCategoria_DevuelveSoloLosDeEsaCategoria()
    {
        CargarCatalogo();

        var resultado = Servicio.Filtrar(Servicio.ObtenerActivos(), new FiltroProducto { CategoriaId = hogar.Id });

        Assert.Equal(2, resultado.Count);
        Assert.All(resultado, p => Assert.Equal(hogar.Id, p.CategoriaId));
    }

    [Fact]
    public void Filtrar_PorSuplidor_DevuelveSoloLosDeEseSuplidor()
    {
        CargarCatalogo();

        var resultado = Servicio.Filtrar(Servicio.ObtenerActivos(), new FiltroProducto { SuplidorId = proveedorB.Id });

        Assert.Equal(2, resultado.Count);
        Assert.All(resultado, p => Assert.Equal(proveedorB.Id, p.SuplidorId));
    }

    [Fact]
    public void Filtrar_PorRangoDePrecios_IncluyeLosExtremos()
    {
        CargarCatalogo();

        var resultado = Servicio.Filtrar(
            Servicio.ObtenerActivos(),
            new FiltroProducto { PrecioMinimo = 420m, PrecioMaximo = 2450m });

        Assert.Equal(new[] { "Bocina Portátil", "Mouse Inalámbrico", "Organizador" }, resultado.Select(p => p.Nombre).OrderBy(n => n).ToArray());
    }

    [Fact]
    public void Filtrar_SoloConPrecioMinimo_NoLimitaElMaximo()
    {
        CargarCatalogo();

        var resultado = Servicio.Filtrar(Servicio.ObtenerActivos(), new FiltroProducto { PrecioMinimo = 2450m });

        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public void Filtrar_ConRangoInvertido_NoDevuelveNada()
    {
        CargarCatalogo();

        var resultado = Servicio.Filtrar(
            Servicio.ObtenerActivos(),
            new FiltroProducto { PrecioMinimo = 1000m, PrecioMaximo = 500m });

        Assert.Empty(resultado);
    }

    [Fact]
    public void Filtrar_CombinandoCategoriaRangoYTexto_AplicaTodosLosCriterios()
    {
        CargarCatalogo();

        var resultado = Servicio.Filtrar(
            Servicio.ObtenerActivos(),
            new FiltroProducto { CategoriaId = electronica.Id, PrecioMaximo = 1000m, Texto = "INALAMBRICO" });

        Assert.Equal("Mouse Inalámbrico", Assert.Single(resultado).Nombre);
    }

    [Fact]
    public void Filtrar_ElTextoTambienBuscaPorCategoriaSuplidorYPrecio()
    {
        CargarCatalogo();

        Assert.Equal(2, Servicio.Filtrar(Servicio.ObtenerActivos(), new FiltroProducto { Texto = "hogar" }).Count);
        Assert.Equal(3, Servicio.Filtrar(Servicio.ObtenerActivos(), new FiltroProducto { Texto = "alfa" }).Count);
        Assert.Single(Servicio.Filtrar(Servicio.ObtenerActivos(), new FiltroProducto { Texto = "3200" }));
    }
}
