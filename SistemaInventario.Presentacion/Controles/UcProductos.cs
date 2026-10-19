using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Formularios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Controles;

/// <summary>
/// Listado y administración de productos, con filtros combinables por categoría, suplidor y
/// rango de precios además de la búsqueda de texto. Todo se filtra en tiempo real.
/// El aspecto se edita con el diseñador visual (UcProductos.Designer.cs); aquí solo está la lógica.
/// </summary>
internal sealed partial class UcProductos : UserControl, IRecargable
{
    private readonly IProductoServicio servicio;
    private readonly ICategoriaServicio categorias;
    private readonly ISuplidorServicio suplidores;
    private readonly ControladorListado<Producto> listado;
    private bool cargandoCombos;

    /// <summary>Crea la pantalla con sus servicios (inyección por constructor).</summary>
    /// <param name="servicio">Servicio de productos.</param>
    /// <param name="categorias">Servicio de categorías (para el filtro y el editor).</param>
    /// <param name="suplidores">Servicio de suplidores (para el filtro y el editor).</param>
    public UcProductos(IProductoServicio servicio, ICategoriaServicio categorias, ISuplidorServicio suplidores)
    {
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        this.categorias = categorias ?? throw new ArgumentNullException(nameof(categorias));
        this.suplidores = suplidores ?? throw new ArgumentNullException(nameof(suplidores));

        InitializeComponent();
        UiHelper.AlinearColumnasNumericas(grid);
        UiHelper.GrisarAlDeshabilitar(btnEditar, btnEliminar);
        UiHelper.LimpiarErrorAlEditar(errores, txtPrecioMinimo, txtPrecioMaximo);

        listado = new ControladorListado<Producto>(
            this,
            grid,
            txtBuscar,
            lblEstado,
            "productos",
            CargarDatos,
            Filtrar);
        listado.AlEditar = EditarProducto;
        listado.AlEliminar = EliminarProducto;
        listado.AsociarBotonesDeSeleccion(btnEditar, btnEliminar);
    }

    /// <inheritdoc />
    public void Recargar()
    {
        listado.Recargar();
    }

    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!DesignMode)
        {
            listado.Iniciar();
        }
    }

    private void btnNuevo_Click(object? sender, EventArgs e)
    {
        AbrirEditor(null);
    }

    private void btnEditar_Click(object? sender, EventArgs e)
    {
        listado.EditarSeleccionado();
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        listado.EliminarSeleccionado();
    }

    private void btnActualizar_Click(object? sender, EventArgs e)
    {
        listado.Recargar();
    }

    private void btnLimpiar_Click(object? sender, EventArgs e)
    {
        cargandoCombos = true;
        try
        {
            cboCategoria.SelectedIndex = 0;
            cboSuplidor.SelectedIndex = 0;
            txtPrecioMinimo.Clear();
            txtPrecioMaximo.Clear();
        }
        finally
        {
            cargandoCombos = false;
        }

        listado.AplicarFiltro();
    }

    private void cboCategoria_SelectedIndexChanged(object? sender, EventArgs e)
    {
        AlCambiarFiltro();
    }

    private void cboSuplidor_SelectedIndexChanged(object? sender, EventArgs e)
    {
        AlCambiarFiltro();
    }

    private void txtPrecio_TextChanged(object? sender, EventArgs e)
    {
        AlCambiarFiltro();
    }

    private void AlCambiarFiltro()
    {
        if (!cargandoCombos)
        {
            listado.AplicarFiltro();
        }
    }

    private IReadOnlyList<Producto> CargarDatos()
    {
        IReadOnlyList<Producto> productos = servicio.ObtenerActivos();
        CargarOpcionesDeFiltro();
        return productos;
    }

    private IReadOnlyList<Producto> Filtrar(IReadOnlyList<Producto> datos, string texto)
    {
        FiltroProducto filtro = new FiltroProducto();
        filtro.Texto = texto;
        filtro.CategoriaId = UiHelper.SeleccionDe(cboCategoria);
        filtro.SuplidorId = UiHelper.SeleccionDe(cboSuplidor);
        filtro.PrecioMinimo = UiHelper.LeerMontoOpcional(txtPrecioMinimo, errores);
        filtro.PrecioMaximo = UiHelper.LeerMontoOpcional(txtPrecioMaximo, errores);

        if (filtro.PrecioMinimo.HasValue && filtro.PrecioMaximo.HasValue && filtro.PrecioMinimo > filtro.PrecioMaximo)
        {
            errores.SetError(txtPrecioMaximo, "El precio máximo no puede ser menor que el mínimo.");
        }

        return servicio.Filtrar(datos, filtro);
    }

    private void CargarOpcionesDeFiltro()
    {
        IReadOnlyList<Categoria> listaCategorias = categorias.ObtenerActivos();
        IReadOnlyList<Suplidor> listaSuplidores = suplidores.ObtenerActivos();

        cargandoCombos = true;
        try
        {
            UiHelper.LlenarComboConTodos(
                cboCategoria,
                "Todas las categorías",
                listaCategorias.Select(c => new ItemCombo(c.Id, c.Nombre)));
            UiHelper.LlenarComboConTodos(
                cboSuplidor,
                "Todos los suplidores",
                listaSuplidores.Select(s => new ItemCombo(s.Id, s.NombreEmpresa)));
        }
        finally
        {
            cargandoCombos = false;
        }
    }

    private void EditarProducto(Producto seleccionado)
    {
        Producto? completo = null;
        bool correcto = UiHelper.Ejecutar(FindForm(), () => { completo = servicio.ObtenerPorId(seleccionado.Id); });

        if (correcto && completo != null)
        {
            AbrirEditor(completo);
        }
    }

    private void EliminarProducto(Producto seleccionado)
    {
        bool confirmado = UiHelper.Confirmar(
            FindForm(),
            "¿Enviar el producto \"" + seleccionado.Nombre + "\" a la Papelera?\n\n" +
            "Podrá restaurarlo más adelante desde la sección Papelera.",
            "Eliminar producto");

        if (confirmado && UiHelper.Ejecutar(FindForm(), () => servicio.Eliminar(seleccionado.Id)))
        {
            listado.Recargar();
        }
    }

    private void AbrirEditor(Producto? producto)
    {
        IReadOnlyList<Categoria>? listaCategorias = null;
        IReadOnlyList<Suplidor>? listaSuplidores = null;

        bool correcto = UiHelper.Ejecutar(FindForm(), () =>
        {
            listaCategorias = categorias.ObtenerActivos();
            listaSuplidores = suplidores.ObtenerActivos();
        });

        if (!correcto || listaCategorias == null || listaSuplidores == null)
        {
            return;
        }

        if (listaCategorias.Count == 0 || listaSuplidores.Count == 0)
        {
            UiHelper.Advertir(
                FindForm(),
                "Para registrar productos debe existir al menos una categoría y un suplidor activos.",
                "Faltan datos previos");
            return;
        }

        using (FrmProductoEditar dialogo = new FrmProductoEditar(servicio, listaCategorias, listaSuplidores, producto))
        {
            if (dialogo.ShowDialog(FindForm()) == DialogResult.OK)
            {
                listado.Recargar(dialogo.IdGuardado);
            }
        }
    }
}
