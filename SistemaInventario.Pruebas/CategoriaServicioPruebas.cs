using System;
using System.Linq;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class CategoriaServicioPruebas
{
    private readonly Escenario escenario = new Escenario();

    private CategoriaServicio Servicio => escenario.ServicioCategorias;

    [Fact]
    public void Crear_ConDatosValidos_AsignaIdYLaGuarda()
    {
        int id = Servicio.Crear(ConstructorDatos.CategoriaValida("Herramientas"));

        Assert.True(id > 0);
        Assert.Equal("Herramientas", Servicio.ObtenerPorId(id).Nombre);
        Assert.Single(Servicio.ObtenerActivos());
    }

    [Fact]
    public void Crear_RecortaLosEspaciosDelNombreYDeLaDescripcion()
    {
        Categoria categoria = new Categoria { Nombre = "  Hogar  ", Descripcion = "   Cosas para la casa   " };

        int id = Servicio.Crear(categoria);

        Categoria guardada = Servicio.ObtenerPorId(id);
        Assert.Equal("Hogar", guardada.Nombre);
        Assert.Equal("Cosas para la casa", guardada.Descripcion);
    }

    [Fact]
    public void Crear_ConNombreDuplicado_SinImportarMayusculas_LanzaValidacionEnNombre()
    {
        escenario.NuevaCategoria("Electrónica");

        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
        {
            Servicio.Crear(ConstructorDatos.CategoriaValida("ELECTRÓNICA"));
        });

        Assert.Contains(ex.Errores, e => e.Campo == nameof(Categoria.Nombre));
        Assert.Single(escenario.Categorias.ObtenerActivos());
    }

    [Fact]
    public void Crear_ConNombreDeUnaCategoriaEnLaPapelera_TambienEsDuplicado()
    {
        escenario.NuevaCategoria("Deportes", activa: false);

        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
        {
            Servicio.Crear(ConstructorDatos.CategoriaValida("Deportes"));
        });

        Assert.Contains("Papelera", ex.Errores.First().Mensaje);
    }

    [Fact]
    public void Crear_ConVariosErrores_LosReportaTodosJuntos()
    {
        Categoria categoria = new Categoria { Nombre = "ab", Descripcion = string.Empty };

        ValidacionException ex = Assert.Throws<ValidacionException>(() => { Servicio.Crear(categoria); });

        Assert.Contains(ex.Errores, e => e.Campo == nameof(Categoria.Nombre));
        Assert.Contains(ex.Errores, e => e.Campo == nameof(Categoria.Descripcion));
        Assert.Empty(escenario.Categorias.ObtenerActivos());
    }

    [Fact]
    public void Crear_SiLaBaseDeDatosReportaDuplicado_LoTraduceAUnErrorDeValidacion()
    {
        escenario.Categorias.ErrorAlInsertar = new ErrorBaseDatosException(
            "duplicado", null, ErrorBaseDatosException.ViolacionUnica);

        ValidacionException ex = Assert.Throws<ValidacionException>(() =>
        {
            Servicio.Crear(ConstructorDatos.CategoriaValida("Nueva"));
        });

        Assert.Equal(nameof(Categoria.Nombre), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Crear_SiLaBaseDeDatosFallaPorOtraCausa_PropagaLaExcepcion()
    {
        escenario.Categorias.ErrorAlInsertar = new ErrorBaseDatosException("sin conexión", null);

        Assert.Throws<ErrorBaseDatosException>(() => { Servicio.Crear(ConstructorDatos.CategoriaValida("Nueva")); });
    }

    [Fact]
    public void Actualizar_ConservandoSuPropioNombre_EsValido()
    {
        Categoria existente = escenario.NuevaCategoria("Electrónica");
        Categoria cambios = new Categoria { Id = existente.Id, Nombre = "Electrónica", Descripcion = "Nueva descripción" };

        Servicio.Actualizar(cambios);

        Assert.Equal("Nueva descripción", Servicio.ObtenerPorId(existente.Id).Descripcion);
    }

    [Fact]
    public void Actualizar_ConElNombreDeOtraCategoria_LanzaValidacion()
    {
        escenario.NuevaCategoria("Electrónica");
        Categoria otra = escenario.NuevaCategoria("Hogar");
        Categoria cambios = new Categoria { Id = otra.Id, Nombre = "electrónica", Descripcion = "x" };

        ValidacionException ex = Assert.Throws<ValidacionException>(() => { Servicio.Actualizar(cambios); });

        Assert.Equal(nameof(Categoria.Nombre), ex.Errores.Single().Campo);
    }

    [Fact]
    public void Actualizar_UnaCategoriaEliminada_LanzaReglaDeNegocio()
    {
        Categoria eliminada = escenario.NuevaCategoria("Deportes", activa: false);
        Categoria cambios = new Categoria { Id = eliminada.Id, Nombre = "Deportes 2", Descripcion = "x" };

        Assert.Throws<ReglaNegocioException>(() => { Servicio.Actualizar(cambios); });
    }

    [Fact]
    public void Actualizar_UnaCategoriaInexistente_LanzaEntidadNoEncontrada()
    {
        Categoria cambios = new Categoria { Id = 999, Nombre = "Fantasma", Descripcion = "x" };

        Assert.Throws<EntidadNoEncontradaException>(() => { Servicio.Actualizar(cambios); });
    }

    [Fact]
    public void Eliminar_ConProductosActivos_LanzaReglaDeNegocioYNoElimina()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");
        Suplidor suplidor = escenario.NuevoSuplidor("Proveedor 1");
        escenario.NuevoProducto("Cable", 100m, categoria, suplidor);
        escenario.NuevoProducto("Mouse", 200m, categoria, suplidor);

        ReglaNegocioException ex = Assert.Throws<ReglaNegocioException>(() => { Servicio.Eliminar(categoria.Id); });

        Assert.Contains("2 producto(s) activo(s)", ex.Message);
        Assert.True(escenario.Categorias.ObtenerPorId(categoria.Id)!.Activo);
    }

    [Fact]
    public void Eliminar_SinProductos_LaEnviaALaPapelera()
    {
        Categoria categoria = escenario.NuevaCategoria("Juguetes");

        Servicio.Eliminar(categoria.Id);

        Assert.Empty(Servicio.ObtenerActivos());
        Assert.Single(Servicio.ObtenerEliminados());
    }

    [Fact]
    public void Eliminar_SoloConProductosEliminados_EstaPermitido()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");
        Suplidor suplidor = escenario.NuevoSuplidor("Proveedor 1");
        escenario.NuevoProducto("Cable", 100m, categoria, suplidor, activo: false);

        Servicio.Eliminar(categoria.Id);

        Assert.Single(Servicio.ObtenerEliminados());
    }

    [Fact]
    public void Eliminar_UnaCategoriaYaEliminada_LanzaReglaDeNegocio()
    {
        Categoria categoria = escenario.NuevaCategoria("Deportes", activa: false);

        Assert.Throws<ReglaNegocioException>(() => { Servicio.Eliminar(categoria.Id); });
    }

    [Fact]
    public void Restaurar_UnaCategoriaEliminada_LaVuelveAActivar()
    {
        Categoria categoria = escenario.NuevaCategoria("Deportes", activa: false);

        Servicio.Restaurar(categoria.Id);

        Assert.True(Servicio.ObtenerPorId(categoria.Id).Activo);
        Assert.Empty(Servicio.ObtenerEliminados());
    }

    [Fact]
    public void Restaurar_UnaCategoriaActiva_LanzaReglaDeNegocio()
    {
        Categoria categoria = escenario.NuevaCategoria("Electrónica");

        Assert.Throws<ReglaNegocioException>(() => { Servicio.Restaurar(categoria.Id); });
    }

    [Fact]
    public void Buscar_FiltraPorNombreODescripcion()
    {
        Categoria a = escenario.Categorias.Agregar(new Categoria { Nombre = "Electrónica", Descripcion = "Equipos y cables" });
        escenario.Categorias.Agregar(new Categoria { Nombre = "Hogar", Descripcion = "Cocina y limpieza" });

        Assert.Equal(a.Id, Assert.Single(Servicio.Buscar(Servicio.ObtenerActivos(), "ELECTRONICA")).Id);
        Assert.Single(Servicio.Buscar(Servicio.ObtenerActivos(), "limpieza"));
        Assert.Equal(2, Servicio.Buscar(Servicio.ObtenerActivos(), "").Count);
    }

    [Fact]
    public void ValidarFoto_RechazaExtensionNoPermitida_YAceptaUnaValida()
    {
        Assert.False(Servicio.ValidarFoto(new FotoCategoria { NombreArchivo = "a.gif", Contenido = ConstructorDatos.BytesJpeg() }).EsValido);
        Assert.True(Servicio.ValidarFoto(ConstructorDatos.FotoPng()).EsValido);
    }

    [Fact]
    public void Crear_ConNull_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => { Servicio.Crear(null!); });
    }
}
