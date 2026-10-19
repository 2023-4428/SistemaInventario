using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Formularios;

/// <summary>Diálogo para crear o editar un producto (la categoría y el suplidor deben estar activos).</summary>
internal sealed partial class FrmProductoEditar : Form
{
    private readonly IProductoServicio servicio;
    private readonly Producto? productoOriginal;
    private readonly Dictionary<string, Control> controlesPorCampo = new Dictionary<string, Control>();

    /// <summary>Constructor solo para el diseñador visual de Visual Studio; no se usa en tiempo de ejecución.</summary>
    [Obsolete("Solo para el diseñador de Visual Studio. Use el constructor con parámetros.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public FrmProductoEditar()
    {
        servicio = null!;
        InitializeComponent();
    }

    /// <summary>Crea el diálogo.</summary>
    /// <param name="servicio">Servicio de productos.</param>
    /// <param name="categorias">Categorías activas entre las que se puede elegir.</param>
    /// <param name="suplidores">Suplidores activos entre los que se puede elegir.</param>
    /// <param name="producto">Producto a editar, o <c>null</c> para crear uno nuevo.</param>
    public FrmProductoEditar(
        IProductoServicio servicio,
        IReadOnlyList<Categoria> categorias,
        IReadOnlyList<Suplidor> suplidores,
        Producto? producto)
    {
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        productoOriginal = producto;

        // El aspecto del formulario vive en FrmProductoEditar.Designer.cs (se edita con el diseñador visual).
        InitializeComponent();

        Text = producto == null ? "Nuevo producto" : "Editar producto";
        txtNombre.MaxLength = Producto.LongitudMaximaNombre;

        // Se llenan con Items (sin DataSource): así la lista inicia sin selección de forma confiable.
        cboCategoria.DisplayMember = nameof(Categoria.Nombre);
        foreach (Categoria categoria in categorias)
        {
            cboCategoria.Items.Add(categoria);
        }

        cboSuplidor.DisplayMember = nameof(Suplidor.NombreEmpresa);
        foreach (Suplidor suplidor in suplidores)
        {
            cboSuplidor.Items.Add(suplidor);
        }

        controlesPorCampo[nameof(Producto.Nombre)] = txtNombre;
        controlesPorCampo[nameof(Producto.PrecioUnitario)] = txtPrecio;
        controlesPorCampo[nameof(Producto.CategoriaId)] = cboCategoria;
        controlesPorCampo[nameof(Producto.SuplidorId)] = cboSuplidor;
        UiHelper.LimpiarErrorAlEditar(errores, txtNombre, txtPrecio, cboCategoria, cboSuplidor);

        if (producto != null)
        {
            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.PrecioUnitario.ToString("0.00", CultureInfo.CurrentCulture);
            cboCategoria.SelectedItem = BuscarPorId(categorias, producto.CategoriaId);
            cboSuplidor.SelectedItem = BuscarPorId(suplidores, producto.SuplidorId);
        }
    }

    /// <summary>Identificador del producto guardado (válido cuando el diálogo se cierra con Aceptar).</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int IdGuardado { get; private set; }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        Guardar();
    }

    private void Guardar()
    {
        errores.Clear();

        decimal precio;
        string textoPrecio = txtPrecio.Text.Trim();

        if (textoPrecio.Length == 0)
        {
            errores.SetError(txtPrecio, "El precio es obligatorio.");
            txtPrecio.Focus();
            return;
        }

        if (!decimal.TryParse(textoPrecio, NumberStyles.Number, CultureInfo.CurrentCulture, out precio))
        {
            errores.SetError(txtPrecio, "Escriba un número válido (por ejemplo, 1500 o 1500.50).");
            txtPrecio.Focus();
            return;
        }

        Producto producto = new Producto();
        producto.Id = productoOriginal == null ? 0 : productoOriginal.Id;
        producto.Nombre = txtNombre.Text;
        producto.PrecioUnitario = precio;
        Categoria? categoriaElegida = cboCategoria.SelectedItem as Categoria;
        Suplidor? suplidorElegido = cboSuplidor.SelectedItem as Suplidor;
        producto.CategoriaId = categoriaElegida == null ? 0 : categoriaElegida.Id;
        producto.SuplidorId = suplidorElegido == null ? 0 : suplidorElegido.Id;

        bool correcto = UiHelper.Ejecutar(
            this,
            () =>
            {
                if (producto.EsNueva)
                {
                    IdGuardado = servicio.Crear(producto);
                }
                else
                {
                    servicio.Actualizar(producto);
                    IdGuardado = producto.Id;
                }
            },
            erroresValidacion => UiHelper.MostrarErrores(errores, controlesPorCampo, erroresValidacion, this));

        if (correcto)
        {
            DialogResult = DialogResult.OK;
        }
    }

    private static T? BuscarPorId<T>(IReadOnlyList<T> elementos, int id) where T : EntidadBase
    {
        foreach (T elemento in elementos)
        {
            if (elemento.Id == id)
            {
                return elemento;
            }
        }

        return null;
    }
}
