using System;
using System.Linq;
using SistemaInventario.Entidades;
using SistemaInventario.Entidades.Reportes;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ReporteServicioPruebas
{
    private readonly Escenario escenario = new Escenario();

    public ReporteServicioPruebas()
    {
        Categoria electronica = escenario.NuevaCategoria("Electrónica");
        Categoria hogar = escenario.NuevaCategoria("Hogar");
        Categoria oficina = escenario.NuevaCategoria("Oficina");
        escenario.NuevaCategoria("Vacía");
        Categoria eliminada = escenario.NuevaCategoria("Deportes", activa: false);

        Suplidor s1 = escenario.NuevoSuplidor("Suplidor 1");
        Suplidor s2 = escenario.NuevoSuplidor("Suplidor 2");
        Suplidor s3 = escenario.NuevoSuplidor("Suplidor 3");
        Suplidor s4 = escenario.NuevoSuplidor("Suplidor 4");
        Suplidor s5 = escenario.NuevoSuplidor("Suplidor 5");
        Suplidor s6 = escenario.NuevoSuplidor("Suplidor 6");
        escenario.NuevoSuplidor("Suplidor eliminado", activo: false);

        escenario.NuevoProducto("P1", 1000m, electronica, s1);
        escenario.NuevoProducto("P2", 2000m, electronica, s1);
        escenario.NuevoProducto("P3", 3000m, electronica, s2);
        escenario.NuevoProducto("P4", 500m, hogar, s1);
        escenario.NuevoProducto("P5", 1500m, hogar, s3);
        escenario.NuevoProducto("P6", 100m, oficina, s4);
        escenario.NuevoProducto("P7", 200m, oficina, s5);
        escenario.NuevoProducto("P8", 300m, oficina, s6);

        // Un producto eliminado no debe contarse en ningún reporte
        escenario.NuevoProducto("Eliminado", 99999m, electronica, s1, activo: false);
        escenario.NuevoProducto("Deportivo", 50m, eliminada, s2, activo: false);
    }

    [Fact]
    public void ProductosAgrupadosPorCategoria_IncluyeCategoriasVaciasYOrdenaPorCantidad()
    {
        var filas = escenario.ServicioReportes.ProductosAgrupadosPorCategoria();

        Assert.Equal(
            new[] { "Electrónica:3", "Oficina:3", "Hogar:2", "Vacía:0" },
            filas.Select(f => f.Categoria + ":" + f.CantidadProductos).ToArray());
    }

    [Fact]
    public void TopSuplidores_DevuelveCincoOrdenadosPorCantidadYNombre()
    {
        var ranking = escenario.ServicioReportes.TopSuplidoresPorProductos();

        Assert.Equal(5, ranking.Count);
        Assert.Equal(1, ranking[0].Posicion);
        Assert.Equal("Suplidor 1", ranking[0].Suplidor);
        Assert.Equal(3, ranking[0].CantidadProductos);
        Assert.Equal(
            new[] { "Suplidor 1", "Suplidor 2", "Suplidor 3", "Suplidor 4", "Suplidor 5" },
            ranking.Select(r => r.Suplidor).ToArray());
        Assert.Equal(new[] { 1, 2, 3, 4, 5 }, ranking.Select(r => r.Posicion).ToArray());
    }

    [Fact]
    public void TopSuplidores_RespetaLaCantidadSolicitada()
    {
        Assert.Equal(2, escenario.ServicioReportes.TopSuplidoresPorProductos(2).Count);
        Assert.Equal(6, escenario.ServicioReportes.TopSuplidoresPorProductos(50).Count);
    }

    [Fact]
    public void TopSuplidores_ConCantidadNoPositiva_LanzaArgumentOutOfRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => { escenario.ServicioReportes.TopSuplidoresPorProductos(0); });
    }

    [Fact]
    public void PromedioPreciosPorCategoria_CalculaPromedioMinimoYMaximo()
    {
        var filas = escenario.ServicioReportes.PromedioPreciosPorCategoria();

        Assert.Equal(new[] { "Electrónica", "Hogar", "Oficina" }, filas.Select(f => f.Categoria).ToArray());

        PromedioCategoria electronica = filas[0];
        Assert.Equal(3, electronica.CantidadProductos);
        Assert.Equal(2000m, electronica.PrecioPromedio);
        Assert.Equal(1000m, electronica.PrecioMinimo);
        Assert.Equal(3000m, electronica.PrecioMaximo);

        Assert.Equal(1000m, filas[1].PrecioPromedio);
        Assert.Equal(200m, filas[2].PrecioPromedio);
    }

    [Fact]
    public void ProductoMasCaroPorCategoria_DevuelveUnoPorCategoriaOrdenadoPorNombre()
    {
        var filas = escenario.ServicioReportes.ProductoMasCaroPorCategoria();

        Assert.Equal(
            new[] { "Electrónica:P3", "Hogar:P5", "Oficina:P8" },
            filas.Select(f => f.Categoria + ":" + f.Producto).ToArray());
        Assert.Equal(3000m, filas[0].Precio);
    }

    [Fact]
    public void CategoriasSinProductos_SoloDevuelveLasActivasSinProductosActivos()
    {
        var filas = escenario.ServicioReportes.CategoriasSinProductos();

        Assert.Equal("Vacía", Assert.Single(filas).Categoria);
    }

    [Fact]
    public void ProductosPorRangoDePrecio_FiltraYOrdenaPorPrecio()
    {
        var filas = escenario.ServicioReportes.ProductosPorRangoDePrecio(500m, 2000m, null);

        Assert.Equal(new[] { "P4", "P1", "P5", "P2" }, filas.Select(p => p.Nombre).ToArray());
    }

    [Fact]
    public void ProductosPorRangoDePrecio_ConCategoria_FiltraPorEsaCategoria()
    {
        int idElectronica = escenario.Categorias.ObtenerActivos().First(c => c.Nombre == "Electrónica").Id;

        var filas = escenario.ServicioReportes.ProductosPorRangoDePrecio(null, null, idElectronica);

        Assert.Equal(new[] { "P1", "P2", "P3" }, filas.Select(p => p.Nombre).ToArray());
    }

    [Fact]
    public void ProductosPorRangoDePrecio_SoloConMinimo_NoLimitaElMaximo()
    {
        var filas = escenario.ServicioReportes.ProductosPorRangoDePrecio(2000m, null, null);

        Assert.Equal(new[] { "P2", "P3" }, filas.Select(p => p.Nombre).ToArray());
    }

    [Fact]
    public void ProductosPorRangoDePrecio_SinCriterios_DevuelveSoloLosActivos()
    {
        Assert.Equal(8, escenario.ServicioReportes.ProductosPorRangoDePrecio(null, null, null).Count);
    }
}
