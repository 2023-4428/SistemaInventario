using System;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Formularios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Controles;

/// <summary>
/// Pantalla de suplidores: listado con búsqueda en tiempo real, creación, edición y eliminación (lógica).
/// El aspecto se edita con el diseñador visual (UcSuplidores.Designer.cs); aquí solo está la lógica.
/// </summary>
internal sealed partial class UcSuplidores : UserControl, IRecargable
{
    private readonly ISuplidorServicio servicio;
    private readonly ControladorListado<Suplidor> listado;

    /// <summary>Crea la pantalla.</summary>
    /// <param name="servicio">Servicio de suplidores.</param>
    public UcSuplidores(ISuplidorServicio servicio)
    {
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));

        InitializeComponent();
        UiHelper.AlinearColumnasNumericas(grid);
        UiHelper.GrisarAlDeshabilitar(btnEditar, btnEliminar);

        listado = new ControladorListado<Suplidor>(
            this,
            grid,
            txtBuscar,
            lblEstado,
            "suplidores",
            () => this.servicio.ObtenerActivos(),
            (datos, texto) => this.servicio.Buscar(datos, texto));
        listado.AlEditar = suplidor => AbrirEditor(suplidor);
        listado.AlEliminar = EliminarSuplidor;
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

    private void AbrirEditor(Suplidor? suplidor)
    {
        using (FrmSuplidorEditar dialogo = new FrmSuplidorEditar(servicio, suplidor))
        {
            if (dialogo.ShowDialog(FindForm()) == DialogResult.OK)
            {
                listado.Recargar(dialogo.IdGuardado);
            }
        }
    }

    private void EliminarSuplidor(Suplidor suplidor)
    {
        bool confirmado = UiHelper.Confirmar(
            FindForm(),
            "¿Enviar el suplidor \"" + suplidor.NombreEmpresa + "\" a la papelera?\n\nPodrá restaurarlo después desde la Papelera.",
            "Eliminar suplidor");

        if (confirmado && UiHelper.Ejecutar(FindForm(), () => servicio.Eliminar(suplidor.Id)))
        {
            listado.Recargar();
        }
    }
}
