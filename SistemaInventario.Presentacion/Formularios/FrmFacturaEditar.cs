using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Formularios;

/// <summary>
/// Ventana de facturación. Al crear una factura se elige el cliente, se agregan productos con su cantidad
/// (si el producto ya está en la factura se suman las cantidades, no se duplica la línea) y se ven los totales
/// con ITBIS (18 %). Al abrir una factura existente se muestra en modo de solo lectura.
/// El aspecto se edita con el diseñador visual (FrmFacturaEditar.Designer.cs).
/// </summary>
internal sealed partial class FrmFacturaEditar : Form
{
    private readonly IFacturaServicio servicio;
    private readonly Factura factura;
    private readonly bool soloLectura;
    private readonly Dictionary<string, Control> controlesPorCampo = new Dictionary<string, Control>();

    /// <summary>Constructor solo para el diseñador visual de Visual Studio; no se usa en tiempo de ejecución.</summary>
    [Obsolete("Solo para el diseñador de Visual Studio. Use el constructor con parámetros.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public FrmFacturaEditar()
    {
        servicio = null!;
        factura = new Factura();
        InitializeComponent();
    }

    /// <summary>Crea la ventana.</summary>
    /// <param name="servicio">Servicio de facturas.</param>
    /// <param name="productos">Productos activos que se pueden facturar.</param>
    /// <param name="existente">Factura a mostrar (solo lectura), o <c>null</c> para crear una nueva.</param>
    public FrmFacturaEditar(IFacturaServicio servicio, IReadOnlyList<Producto> productos, Factura? existente)
    {
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        factura = existente ?? new Factura();
        soloLectura = existente != null;

        // El aspecto vive en FrmFacturaEditar.Designer.cs (se edita con el diseñador visual).
        InitializeComponent();

        txtCliente.MaxLength = Factura.LongitudMaximaCliente;
        txtDocumento.MaxLength = Factura.LongitudMaximaDocumento;
        numCantidad.Maximum = FacturaDetalle.CantidadMaxima;
        UiHelper.AlinearColumnasNumericas(gridLineas);
        UiHelper.GrisarAlDeshabilitar(btnQuitar);

        controlesPorCampo[nameof(Factura.ClienteNombre)] = txtCliente;
        controlesPorCampo[nameof(Factura.ClienteDocumento)] = txtDocumento;
        controlesPorCampo[nameof(Factura.Detalles)] = gridLineas;
        UiHelper.LimpiarErrorAlEditar(errores, txtCliente, txtDocumento);

        foreach (Producto producto in productos)
        {
            cboProducto.Items.Add(new ItemProducto(producto));
        }

        if (cboProducto.Items.Count > 0)
        {
            cboProducto.SelectedIndex = 0;
        }

        gridLineas.SelectionChanged += (remitente, argumentos) => ActualizarBotones();

        if (soloLectura)
        {
            ConfigurarSoloLectura();
        }

        MostrarLineas();
    }

    /// <summary>Identificador de la factura guardada (válido cuando la ventana se cierra con Aceptar).</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int IdGuardado { get; private set; }

    private void btnAgregar_Click(object? sender, EventArgs e)
    {
        ItemProducto? item = cboProducto.SelectedItem as ItemProducto;

        if (item == null)
        {
            UiHelper.Advertir(this, "Seleccione el producto que desea agregar.", "Revise los datos");
            return;
        }

        int cantidad = (int)numCantidad.Value;
        FacturaDetalle? existente = factura.Detalles.FirstOrDefault(d => d.ProductoId == item.Producto.Id);

        if (existente != null)
        {
            // Evita líneas duplicadas: se suman las cantidades en la línea que ya existe.
            long nueva = (long)existente.Cantidad + cantidad;

            if (nueva > FacturaDetalle.CantidadMaxima)
            {
                UiHelper.Advertir(
                    this,
                    "La cantidad total de \"" + item.Producto.Nombre + "\" no puede superar " +
                    FacturaDetalle.CantidadMaxima.ToString("N0", CultureInfo.CurrentCulture) + ".",
                    "Cantidad excedida");
                return;
            }

            existente.Cantidad = (int)nueva;
            factura.RecalcularTotales();
        }
        else
        {
            if (factura.Detalles.Count >= Factura.MaximoLineas)
            {
                UiHelper.Advertir(this, "Una factura admite como máximo " + Factura.MaximoLineas + " líneas.", "Límite alcanzado");
                return;
            }

            factura.AgregarDetalle(new FacturaDetalle
            {
                ProductoId = item.Producto.Id,
                ProductoNombre = item.Producto.Nombre,
                Cantidad = cantidad,
                PrecioUnitario = item.Producto.PrecioUnitario
            });
        }

        errores.SetError(gridLineas, string.Empty);
        numCantidad.Value = 1;
        MostrarLineas();
    }

    private void btnQuitar_Click(object? sender, EventArgs e)
    {
        FacturaDetalle? seleccionada = ObtenerLineaSeleccionada();

        if (seleccionada != null)
        {
            factura.QuitarDetalle(seleccionada);
            MostrarLineas();
        }
    }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        errores.Clear();

        factura.ClienteNombre = txtCliente.Text;
        factura.ClienteDocumento = txtDocumento.Text;

        bool correcto = UiHelper.Ejecutar(
            this,
            () => { IdGuardado = servicio.Crear(factura); },
            erroresValidacion => UiHelper.MostrarErrores(errores, controlesPorCampo, erroresValidacion, this));

        if (correcto)
        {
            DialogResult = DialogResult.OK;
        }
    }

    private FacturaDetalle? ObtenerLineaSeleccionada()
    {
        DataGridViewRow? fila = gridLineas.CurrentRow;
        return fila == null ? null : fila.DataBoundItem as FacturaDetalle;
    }

    private void MostrarLineas()
    {
        gridLineas.DataSource = null;
        gridLineas.DataSource = new BindingList<FacturaDetalle>(factura.Detalles.ToList());

        lblSubtotalValor.Text = Dinero(factura.Subtotal);
        lblItbisValor.Text = Dinero(factura.Itbis);
        lblTotalValor.Text = Dinero(factura.Total);

        ActualizarBotones();
    }

    private void ActualizarBotones()
    {
        btnQuitar.Enabled = !soloLectura && ObtenerLineaSeleccionada() != null;
    }

    private void ConfigurarSoloLectura()
    {
        Text = "Factura " + factura.Numero;
        txtCliente.Text = factura.ClienteNombre;
        txtDocumento.Text = factura.ClienteDocumento ?? string.Empty;
        txtCliente.ReadOnly = true;
        txtDocumento.ReadOnly = true;

        lblProducto.Visible = false;
        cboProducto.Visible = false;
        lblCantidad.Visible = false;
        numCantidad.Visible = false;
        btnAgregar.Visible = false;
        btnQuitar.Visible = false;
        btnGuardar.Visible = false;
        btnCancelar.Text = "Cerrar";

        lblEstadoFactura.Text = factura.Activo
            ? "Emitida el " + factura.FechaCreacion.ToString(UiHelper.FormatoFechaHora, CultureInfo.CurrentCulture)
            : "FACTURA ANULADA";
        lblEstadoFactura.Visible = true;
    }

    private static string Dinero(decimal valor)
    {
        return "RD$ " + valor.ToString("N2", CultureInfo.CurrentCulture);
    }
}
