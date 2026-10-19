using System;
using System.Windows.Forms;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Controles;

/// <summary>
/// Papelera: una pestaña por tipo de registro. Desde cada una se puede restaurar un registro o eliminarlo
/// definitivamente. El aspecto se edita con el diseñador visual (UcPapelera.Designer.cs).
/// </summary>
internal sealed partial class UcPapelera : UserControl, IRecargable
{
    private readonly IListaPapelera[] listas;

    /// <summary>Crea la pantalla con los servicios de todas las entidades.</summary>
    /// <param name="categorias">Servicio de categorías.</param>
    /// <param name="suplidores">Servicio de suplidores.</param>
    /// <param name="productos">Servicio de productos.</param>
    /// <param name="facturas">Servicio de facturas.</param>
    public UcPapelera(
        ICategoriaServicio categorias,
        ISuplidorServicio suplidores,
        IProductoServicio productos,
        IFacturaServicio facturas)
    {
        if (categorias == null)
        {
            throw new ArgumentNullException(nameof(categorias));
        }

        if (suplidores == null)
        {
            throw new ArgumentNullException(nameof(suplidores));
        }

        if (productos == null)
        {
            throw new ArgumentNullException(nameof(productos));
        }

        if (facturas == null)
        {
            throw new ArgumentNullException(nameof(facturas));
        }

        InitializeComponent();

        listaCategorias.Inicializar(categorias);
        listaSuplidores.Inicializar(suplidores);
        listaProductos.Inicializar(productos);
        listaFacturas.Inicializar(facturas);

        listas = new IListaPapelera[] { listaCategorias, listaSuplidores, listaProductos, listaFacturas };

        foreach (IListaPapelera lista in listas)
        {
            lista.CantidadCambio += (remitente, argumentos) => ActualizarTitulos();

            // Al restaurar o borrar algo cambian las reglas de las demás pestañas: se recargan todas.
            lista.RegistroModificado += (remitente, argumentos) => Recargar();
        }

        ActualizarTitulos();
    }

    /// <inheritdoc />
    public void Recargar()
    {
        foreach (IListaPapelera lista in listas)
        {
            lista.Recargar();
        }
    }

    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!DesignMode)
        {
            Recargar();
        }
    }

    private void ActualizarTitulos()
    {
        paginaCategorias.Text = "Categorías (" + listaCategorias.Cantidad + ")";
        paginaSuplidores.Text = "Suplidores (" + listaSuplidores.Cantidad + ")";
        paginaProductos.Text = "Productos (" + listaProductos.Cantidad + ")";
        paginaFacturas.Text = "Facturas (" + listaFacturas.Cantidad + ")";
    }
}
