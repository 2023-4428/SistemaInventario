using System.Linq;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class SuplidorServicioPruebas
{
    private readonly Escenario escenario = new Escenario();

    private SuplidorServicio Servicio => escenario.ServicioSuplidores;

    [Fact]
    public void Crear_ConDatosValidos_LoGuarda()
    {
        int id = Servicio.Crear(ConstructorDatos.SuplidorValido("Importadora Atlántico SAS"));

        Assert.Equal("Importadora Atlántico SAS", Servicio.ObtenerPorId(id).NombreEmpresa);
    }

    [Fact]
    public void Crear_ConElMismoNombreDeEmpresa_LanzaValidacion()
    {
        escenario.NuevoSuplidor("Distribuidora Caribe SRL");

        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
        {
            Servicio.Crear(ConstructorDatos.SuplidorValido("distribuidora caribe srl"));
        });

        Assert.Equal(nameof(Suplidor.NombreEmpresa), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Crear_ConNombreDeEmpresaEnLaPapelera_TambienEsDuplicado()
    {
        escenario.NuevoSuplidor("Comercial Norte", activo: false);

        Assert.Throws<ValidacionException>(() => { Servicio.Crear(ConstructorDatos.SuplidorValido("Comercial Norte")); });
    }

    [Fact]
    public void Crear_ConTelefonoYCorreoInvalidos_ReportaAmbosCampos()
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.Telefono = "555-1234";
        suplidor.Correo = "no-es-un-correo";

        ValidacionException ex = Assert.Throws<ValidacionException>(() => { Servicio.Crear(suplidor); });

        Assert.Contains(ex.Errores, e => e.Campo == nameof(Suplidor.Telefono));
        Assert.Contains(ex.Errores, e => e.Campo == nameof(Suplidor.Correo));
    }

    [Fact]
    public void Crear_ConSitioWebEnBlanco_LoGuardaComoNulo()
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.SitioWeb = "   ";

        int id = Servicio.Crear(suplidor);

        Assert.Null(Servicio.ObtenerPorId(id).SitioWeb);
    }

    [Fact]
    public void Actualizar_ConservandoElMismoNombre_EsValido()
    {
        Suplidor existente = escenario.NuevoSuplidor("Proveedor 1");
        Suplidor cambios = ConstructorDatos.SuplidorValido("Proveedor 1");
        cambios.Id = existente.Id;
        cambios.NombreContacto = "Nuevo Contacto";

        Servicio.Actualizar(cambios);

        Assert.Equal("Nuevo Contacto", Servicio.ObtenerPorId(existente.Id).NombreContacto);
    }

    [Fact]
    public void Eliminar_ConProductosActivos_LanzaReglaDeNegocio()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");
        Suplidor suplidor = escenario.NuevoSuplidor("Proveedor 1");
        escenario.NuevoProducto("Cable", 100m, categoria, suplidor);

        ReglaNegocioException ex = Assert.Throws<ReglaNegocioException>(() => { Servicio.Eliminar(suplidor.Id); });

        Assert.Contains("Proveedor 1", ex.Message);
        Assert.True(escenario.Suplidores.ObtenerPorId(suplidor.Id)!.Activo);
    }

    [Fact]
    public void Eliminar_SinProductosActivos_LoEnviaALaPapeleraYSePuedeRestaurar()
    {
        Suplidor suplidor = escenario.NuevoSuplidor("Proveedor 1");

        Servicio.Eliminar(suplidor.Id);
        Assert.Single(Servicio.ObtenerEliminados());

        Servicio.Restaurar(suplidor.Id);
        Assert.Single(Servicio.ObtenerActivos());
    }

    [Fact]
    public void Buscar_EncuentraPorContactoTelefonoOCorreo()
    {
        Suplidor s1 = escenario.NuevoSuplidor("Alfa SRL");
        s1.NombreContacto = "Luisa Martínez";
        Suplidor s2 = escenario.NuevoSuplidor("Beta SRL");
        s2.Telefono = "849-555-0103";

        Assert.Equal(s1.Id, Assert.Single(Servicio.Buscar(Servicio.ObtenerActivos(), "luisa martinez")).Id);
        Assert.Equal(s2.Id, Assert.Single(Servicio.Buscar(Servicio.ObtenerActivos(), "849-555")).Id);
        Assert.Equal(2, Servicio.Buscar(Servicio.ObtenerActivos(), "empresa.com").Count);
    }
}
