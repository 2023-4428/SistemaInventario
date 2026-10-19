using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Formularios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Controles;

/// <summary>
/// Pantalla de categorías: listado con búsqueda en tiempo real, creación, edición y eliminación (lógica).
/// El aspecto se edita con el diseñador visual (UcCategorias.Designer.cs); aquí solo está la lógica.
/// </summary>
internal sealed partial class UcCategorias : UserControl, IRecargable
{
    private readonly ICategoriaServicio servicio;
    private readonly ControladorListado<Categoria> listado;

    /// <summary>Crea la pantalla.</summary>
    /// <param name="servicio">Servicio de categorías.</param>
    public UcCategorias(ICategoriaServicio servicio)
    {
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));

        InitializeComponent();
        UiHelper.AlinearColumnasNumericas(grid);
        UiHelper.GrisarAlDeshabilitar(btnEditar, btnEliminar);

        listado = new ControladorListado<Categoria>(
            this,
            grid,
            txtBuscar,
            lblEstado,
            "categorías",
            () => this.servicio.ObtenerActivos(),
            (datos, texto) => this.servicio.Buscar(datos, texto));
        listado.AlEditar = categoria => AbrirEditor(categoria);
        listado.AlEliminar = EliminarCategoria;
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

    private void AbrirEditor(Categoria? categoria)
    {
        Categoria? paraEditar = categoria;

        if (categoria != null)
        {
            // El listado no trae las fotos: se cargan completas solo al editar.
            Categoria? completa = null;
            bool correcto = UiHelper.Ejecutar(FindForm(), () => { completa = servicio.ObtenerPorId(categoria.Id); });

            if (!correcto || completa == null)
            {
                return;
            }

            paraEditar = completa;
        }

        using (FrmCategoriaEditar dialogo = new FrmCategoriaEditar(servicio, paraEditar))
        {
            if (dialogo.ShowDialog(FindForm()) == DialogResult.OK)
            {
                listado.Recargar(dialogo.IdGuardado);
            }
        }
    }

    private void EliminarCategoria(Categoria categoria)
    {
        bool confirmado = UiHelper.Confirmar(
            FindForm(),
            "¿Enviar la categoría \"" + categoria.Nombre + "\" a la papelera?\n\nPodrá restaurarla después desde la Papelera.",
            "Eliminar categoría");

        if (confirmado && UiHelper.Ejecutar(FindForm(), () => servicio.Eliminar(categoria.Id)))
        {
            listado.Recargar();
        }
    }
}
