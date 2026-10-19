using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Formularios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Controles;

/// <summary>
/// Pantalla de facturación: lista las facturas emitidas, abre la ventana para crear una nueva, permite ver
/// el detalle de una factura y anularla (va a la Papelera). El aspecto se edita con el diseñador visual.
/// </summary>
internal sealed partial class UcFacturas : UserControl, IRecargable
{
    private readonly IFacturaServicio servicio;
    private readonly IProductoServicio productos;
    private readonly ControladorListado<Factura> listado;

    /// <summary>Crea la pantalla.</summary>
    /// <param name="servicio">Servicio de facturas.</param>
    /// <param name="productos">Servicio de productos (para elegir qué facturar).</param>
    public UcFacturas(IFacturaServicio servicio, IProductoServicio productos)
    {
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        this.productos = productos ?? throw new ArgumentNullException(nameof(productos));

        InitializeComponent();
        UiHelper.AlinearColumnasNumericas(grid);
        UiHelper.GrisarAlDeshabilitar(btnVer, btnAnular);

        listado = new ControladorListado<Factura>(
            this,
            grid,
            txtBuscar,
            lblEstado,
            "facturas",
            () => this.servicio.ObtenerActivos(),
            (datos, texto) => this.servicio.Buscar(datos, texto));
        listado.AlEditar = VerFactura;
        listado.AlEliminar = AnularFactura;
        listado.AsociarBotonesDeSeleccion(btnVer, btnAnular);
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

    private void btnNueva_Click(object? sender, EventArgs e)
    {
        IReadOnlyList<Producto>? disponibles = null;
        bool correcto = UiHelper.Ejecutar(FindForm(), () => { disponibles = productos.ObtenerActivos(); });

        if (!correcto || disponibles == null)
        {
            return;
        }

        if (disponibles.Count == 0)
        {
            UiHelper.Advertir(
                FindForm(),
                "Para facturar debe existir al menos un producto activo. Registre productos en la sección Productos.",
                "No hay productos");
            return;
        }

        using (FrmFacturaEditar ventana = new FrmFacturaEditar(servicio, disponibles, null))
        {
            if (ventana.ShowDialog(FindForm()) == DialogResult.OK)
            {
                listado.Recargar(ventana.IdGuardado);
            }
        }
    }

    private void btnVer_Click(object? sender, EventArgs e)
    {
        listado.EditarSeleccionado();
    }

    private void btnAnular_Click(object? sender, EventArgs e)
    {
        listado.EliminarSeleccionado();
    }

    private void btnActualizar_Click(object? sender, EventArgs e)
    {
        listado.Recargar();
    }

    private void VerFactura(Factura seleccionada)
    {
        Factura? completa = null;
        bool correcto = UiHelper.Ejecutar(FindForm(), () => { completa = servicio.ObtenerPorId(seleccionada.Id); });

        if (!correcto || completa == null)
        {
            return;
        }

        using (FrmFacturaEditar ventana = new FrmFacturaEditar(servicio, new List<Producto>(), completa))
        {
            ventana.ShowDialog(FindForm());
        }
    }

    private void AnularFactura(Factura seleccionada)
    {
        bool confirmado = UiHelper.Confirmar(
            FindForm(),
            "¿Anular la factura " + seleccionada.Numero + " de \"" + seleccionada.ClienteNombre + "\"?\n\n" +
            "Se enviará a la Papelera y podrá restaurarla después.",
            "Anular factura");

        if (confirmado && UiHelper.Ejecutar(FindForm(), () => servicio.Eliminar(seleccionada.Id)))
        {
            listado.Recargar();
        }
    }
}
