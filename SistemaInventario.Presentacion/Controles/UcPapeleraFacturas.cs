using System;
using System.ComponentModel;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Controles;

/// <summary>
/// Pestaña de la Papelera con los registros eliminados (factura): permite buscarlos, restaurarlos o eliminarlos
/// definitivamente. El aspecto se edita con el diseñador visual; la lógica común está en <see cref="ControladorPapelera{T}"/>.
/// </summary>
internal sealed partial class UcPapeleraFacturas : UserControl, IListaPapelera
{
    private ControladorPapelera<Factura>? controlador;

    /// <summary>Crea la lista (queda sin datos hasta llamar a <see cref="Inicializar"/>).</summary>
    public UcPapeleraFacturas()
    {
        InitializeComponent();
        UiHelper.AlinearColumnasNumericas(grid);
        UiHelper.GrisarAlDeshabilitar(btnRestaurar, btnEliminarDefinitivo);
    }

    /// <inheritdoc />
    public event EventHandler? CantidadCambio;

    /// <inheritdoc />
    public event EventHandler? RegistroModificado;

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Cantidad => controlador == null ? 0 : controlador.Cantidad;

    /// <summary>Conecta la lista con el servicio de la capa de negocio.</summary>
    /// <param name="servicio">Servicio de factura.</param>
    public void Inicializar(IServicio<Factura> servicio)
    {
        controlador = new ControladorPapelera<Factura>(
            this,
            grid,
            txtBuscar,
            lblEstado,
            btnRestaurar,
            btnEliminarDefinitivo,
            servicio,
            "la factura",
            f => f.Numero + " (" + f.ClienteNombre + ")");
        controlador.CantidadCambio += (remitente, argumentos) => CantidadCambio?.Invoke(this, EventArgs.Empty);
        controlador.RegistroModificado += (remitente, argumentos) => RegistroModificado?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public void Recargar()
    {
        if (controlador != null)
        {
            controlador.Recargar();
        }
    }

    private void btnRestaurar_Click(object? sender, EventArgs e)
    {
        if (controlador != null)
        {
            controlador.RestaurarSeleccionado();
        }
    }

    private void btnEliminarDefinitivo_Click(object? sender, EventArgs e)
    {
        if (controlador != null)
        {
            controlador.EliminarDefinitivamenteSeleccionado();
        }
    }
}
