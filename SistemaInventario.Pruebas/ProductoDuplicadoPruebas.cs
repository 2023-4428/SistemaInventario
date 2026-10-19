using System.Linq;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ProductoDuplicadoPruebas
{
    private readonly Escenario escenario = new Escenario();
    private readonly Categoria electronica;
    private readonly Categoria hogar;
    private readonly Suplidor alfa;
    private readonly Suplidor beta;

    public ProductoDuplicadoPruebas()
    {
        electronica = escenario.NuevaCategoria("Electrónica");
        hogar = escenario.NuevaCategoria("Hogar");
        alfa = escenario.NuevoSuplidor("Alfa Comercial");
        beta = escenario.NuevoSuplidor("Beta Importaciones");
        escenario.NuevoProducto("Cable HDMI", 275m, electronica, alfa);
    }

    [Fact]
    public void Crear_ConMismoNombreCategoriaYSuplidor_LanzaValidacionEnNombre()
    {
        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
            escenario.ServicioProductos.Crear(ConstructorDatos.ProductoValido(electronica.Id, alfa.Id, "Cable HDMI", 300m)));

        Assert.Equal(nameof(Producto.Nombre), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Crear_IgnoraMayusculasYEspaciosSobrantes()
    {
        Assert.Throws<ValidacionException>(() =>
            escenario.ServicioProductos.Crear(ConstructorDatos.ProductoValido(electronica.Id, alfa.Id, "  cable hdmi  ", 300m)));
    }

    [Fact]
    public void Crear_ConMismoNombrePeroOtraCategoria_SePermite()
    {
        int id = escenario.ServicioProductos.Crear(ConstructorDatos.ProductoValido(hogar.Id, alfa.Id, "Cable HDMI", 275m));

        Assert.True(id > 0);
    }

    [Fact]
    public void Crear_ConMismoNombrePeroOtroSuplidor_SePermite()
    {
        int id = escenario.ServicioProductos.Crear(ConstructorDatos.ProductoValido(electronica.Id, beta.Id, "Cable HDMI", 275m));

        Assert.True(id > 0);
    }

    [Fact]
    public void Crear_ConDuplicadoEnLaPapelera_TambienSeRechaza()
    {
        escenario.NuevoProducto("Mouse", 795m, electronica, alfa, activo: false);

        Assert.Throws<ValidacionException>(() =>
            escenario.ServicioProductos.Crear(ConstructorDatos.ProductoValido(electronica.Id, alfa.Id, "Mouse", 795m)));
    }

    [Fact]
    public void Actualizar_SinCambiarNombre_NoSeCuentaAsiMismoComoDuplicado()
    {
        Producto existente = escenario.Productos.ObtenerActivos().Single();
        Producto cambios = ConstructorDatos.ProductoValido(electronica.Id, alfa.Id, "Cable HDMI", 310m);
        cambios.Id = existente.Id;

        escenario.ServicioProductos.Actualizar(cambios);

        Assert.Equal(310m, escenario.ServicioProductos.ObtenerPorId(existente.Id).PrecioUnitario);
    }

    [Fact]
    public void Actualizar_AUnNombreQueYaExiste_LanzaValidacion()
    {
        Producto otro = escenario.NuevoProducto("Cable VGA", 150m, electronica, alfa);
        Producto cambios = ConstructorDatos.ProductoValido(electronica.Id, alfa.Id, "Cable HDMI", 150m);
        cambios.Id = otro.Id;

        Assert.Throws<ValidacionException>(() => escenario.ServicioProductos.Actualizar(cambios));
    }
}
