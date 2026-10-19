using System.Linq;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class FacturaServicioPruebas
{
    private readonly Escenario escenario = new Escenario();
    private readonly Producto cable;
    private readonly Producto mouse;

    public FacturaServicioPruebas()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");
        Suplidor suplidor = escenario.NuevoSuplidor("Alfa Comercial");
        cable = escenario.NuevoProducto("Cable HDMI", 100m, categoria, suplidor);
        mouse = escenario.NuevoProducto("Mouse", 50.50m, categoria, suplidor);
    }

    [Fact]
    public void Totales_SumanLineasYCalculanItbisDel18Porciento()
    {
        Factura factura = ConstructorDatos.FacturaValida("Juan Pérez", (cable, 2), (mouse, 1));

        Assert.Equal(250.50m, factura.Subtotal);
        Assert.Equal(45.09m, factura.Itbis);
        Assert.Equal(295.59m, factura.Total);
        Assert.Equal(2, factura.CantidadLineas);
    }

    [Fact]
    public void Itbis_SeRedondeaAlejandoseDeCero()
    {
        Factura factura = new Factura();
        factura.AgregarDetalle(new FacturaDetalle { ProductoId = 1, Cantidad = 1, PrecioUnitario = 0.25m });

        Assert.Equal(0.05m, factura.Itbis);
        Assert.Equal(0.30m, factura.Total);
    }

    [Fact]
    public void QuitarDetalle_RecalculaLosTotales()
    {
        Factura factura = ConstructorDatos.FacturaValida("Juan Pérez", (cable, 2), (mouse, 1));

        factura.QuitarDetalle(factura.Detalles.Last());

        Assert.Equal(200m, factura.Subtotal);
        Assert.Equal(36m, factura.Itbis);
        Assert.Equal(236m, factura.Total);
    }

    [Fact]
    public void Crear_GuardaLaFacturaConTotalesRecalculados()
    {
        Factura factura = ConstructorDatos.FacturaValida("  Juan Pérez  ", (cable, 3));
        factura.Total = 1m;
        factura.Subtotal = 1m;

        int id = escenario.ServicioFacturas.Crear(factura);

        Factura guardada = escenario.ServicioFacturas.ObtenerPorId(id);
        Assert.Equal("Juan Pérez", guardada.ClienteNombre);
        Assert.Equal(300m, guardada.Subtotal);
        Assert.Equal(354m, guardada.Total);
    }

    [Fact]
    public void Crear_TomaElPrecioVigenteDelProducto_NoElQueEnviaLaPantalla()
    {
        Factura factura = ConstructorDatos.FacturaValida("Juan Pérez", (cable, 1));
        factura.Detalles[0].PrecioUnitario = 1m;

        int id = escenario.ServicioFacturas.Crear(factura);

        Assert.Equal(100m, escenario.ServicioFacturas.ObtenerPorId(id).Detalles.Single().PrecioUnitario);
    }

    [Fact]
    public void Crear_SinLineas_LanzaValidacionEnDetalles()
    {
        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
            escenario.ServicioFacturas.Crear(ConstructorDatos.FacturaValida("Juan Pérez")));

        Assert.Equal(nameof(Factura.Detalles), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Crear_SinCliente_LanzaValidacionEnCliente()
    {
        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
            escenario.ServicioFacturas.Crear(ConstructorDatos.FacturaValida("   ", (cable, 1))));

        Assert.Equal(nameof(Factura.ClienteNombre), ex.Errores.Single().Campo);
    }

    [Theory]
    [InlineData("001-1234567-8", true)]
    [InlineData("00112345678", true)]
    [InlineData("131-12345-6", true)]
    [InlineData("131123456", true)]
    [InlineData("12345", false)]
    [InlineData("ABC-1234567-8", false)]
    public void Crear_ValidaCedulaORnc(string documento, bool valido)
    {
        Factura factura = ConstructorDatos.FacturaValida("Juan Pérez", (cable, 1));
        factura.ClienteDocumento = documento;

        if (valido)
        {
            Assert.True(escenario.ServicioFacturas.Crear(factura) > 0);
        }
        else
        {
            ValidacionException ex = Assert.Throws<ValidacionException>(() => escenario.ServicioFacturas.Crear(factura));
            Assert.Equal(nameof(Factura.ClienteDocumento), ex.Errores.Single().Campo);
        }
    }

    [Fact]
    public void Crear_ConProductoRepetido_LanzaValidacion()
    {
        Factura factura = ConstructorDatos.FacturaValida("Juan Pérez", (cable, 1), (cable, 2));

        Assert.Throws<ValidacionException>(() => escenario.ServicioFacturas.Crear(factura));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    [InlineData(10000)]
    public void Crear_ConCantidadFueraDeRango_LanzaValidacion(int cantidad)
    {
        Factura factura = ConstructorDatos.FacturaValida("Juan Pérez", (cable, cantidad));

        Assert.Throws<ValidacionException>(() => escenario.ServicioFacturas.Crear(factura));
    }

    [Fact]
    public void Crear_ConProductoEliminado_LanzaValidacion()
    {
        Producto eliminado = escenario.NuevoProducto("Radio", 540m, escenario.NuevaCategoria("Audio"), escenario.NuevoSuplidor("Gamma"), activo: false);
        Factura factura = ConstructorDatos.FacturaValida("Juan Pérez", (eliminado, 1));

        ValidacionException ex = Assert.Throws<ValidacionException>(() => escenario.ServicioFacturas.Crear(factura));

        Assert.Equal(nameof(Factura.Detalles), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Eliminar_AnulaLaFacturaYRestaurarLaReactiva()
    {
        int id = escenario.ServicioFacturas.Crear(ConstructorDatos.FacturaValida("Juan Pérez", (cable, 1)));

        escenario.ServicioFacturas.Eliminar(id);
        Assert.Single(escenario.ServicioFacturas.ObtenerEliminados());
        Assert.Empty(escenario.ServicioFacturas.ObtenerActivos());

        escenario.ServicioFacturas.Restaurar(id);
        Assert.Single(escenario.ServicioFacturas.ObtenerActivos());
    }

    [Fact]
    public void EliminarDefinitivo_SoloSiLaFacturaEstaAnulada()
    {
        int id = escenario.ServicioFacturas.Crear(ConstructorDatos.FacturaValida("Juan Pérez", (cable, 1)));

        Assert.Throws<ReglaNegocioException>(() => escenario.ServicioFacturas.EliminarDefinitivo(id));

        escenario.ServicioFacturas.Eliminar(id);
        escenario.ServicioFacturas.EliminarDefinitivo(id);

        Assert.Throws<EntidadNoEncontradaException>(() => escenario.ServicioFacturas.ObtenerPorId(id));
    }

    [Fact]
    public void Buscar_EncuentraPorNombreDeCliente()
    {
        escenario.ServicioFacturas.Crear(ConstructorDatos.FacturaValida("Juan Pérez", (cable, 1)));
        escenario.ServicioFacturas.Crear(ConstructorDatos.FacturaValida("María Rodríguez", (mouse, 1)));

        var resultado = escenario.ServicioFacturas.Buscar(escenario.ServicioFacturas.ObtenerActivos(), "maria");

        Assert.Equal("María Rodríguez", resultado.Single().ClienteNombre);
    }
}
