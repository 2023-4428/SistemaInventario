using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;

namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>
/// Lógica común de una pestaña de la Papelera: buscar, restaurar y eliminar definitivamente registros ya eliminados.
/// Igual que <see cref="ControladorListado{T}"/>, recibe los controles que la pantalla dibujó en su diseñador.
/// Las reglas (por ejemplo, no borrar una categoría que aún tiene productos) las aplica el servicio.
/// </summary>
/// <typeparam name="T">Entidad cuyos registros eliminados se muestran.</typeparam>
internal sealed class ControladorPapelera<T> where T : EntidadBase
{
    private readonly Control propietario;
    private readonly DataGridView grid;
    private readonly TextBox txtBuscar;
    private readonly Label lblEstado;
    private readonly Button btnRestaurar;
    private readonly Button btnEliminarDefinitivo;
    private readonly IServicio<T> servicio;
    private readonly string nombreEntidad;
    private readonly Func<T, string> nombreMostrado;

    private List<T> origen = new List<T>();

    /// <summary>Crea el controlador y conecta los eventos de la pestaña.</summary>
    /// <param name="propietario">Pantalla que contiene los controles (se usa para mostrar los mensajes).</param>
    /// <param name="grid">Tabla con las columnas ya definidas en el diseñador.</param>
    /// <param name="txtBuscar">Cuadro de búsqueda.</param>
    /// <param name="lblEstado">Etiqueta de estado.</param>
    /// <param name="btnRestaurar">Botón Restaurar.</param>
    /// <param name="btnEliminarDefinitivo">Botón Eliminar definitivamente.</param>
    /// <param name="servicio">Servicio de la entidad.</param>
    /// <param name="nombreEntidad">Nombre legible con artículo, en minúsculas ("la categoría").</param>
    /// <param name="nombreMostrado">Obtiene el texto que identifica a un registro en los mensajes.</param>
    public ControladorPapelera(
        Control propietario,
        DataGridView grid,
        TextBox txtBuscar,
        Label lblEstado,
        Button btnRestaurar,
        Button btnEliminarDefinitivo,
        IServicio<T> servicio,
        string nombreEntidad,
        Func<T, string> nombreMostrado)
    {
        this.propietario = propietario ?? throw new ArgumentNullException(nameof(propietario));
        this.grid = grid ?? throw new ArgumentNullException(nameof(grid));
        this.txtBuscar = txtBuscar ?? throw new ArgumentNullException(nameof(txtBuscar));
        this.lblEstado = lblEstado ?? throw new ArgumentNullException(nameof(lblEstado));
        this.btnRestaurar = btnRestaurar ?? throw new ArgumentNullException(nameof(btnRestaurar));
        this.btnEliminarDefinitivo = btnEliminarDefinitivo ?? throw new ArgumentNullException(nameof(btnEliminarDefinitivo));
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        this.nombreEntidad = nombreEntidad;
        this.nombreMostrado = nombreMostrado ?? throw new ArgumentNullException(nameof(nombreMostrado));

        txtBuscar.TextChanged += (remitente, argumentos) => AplicarFiltro();
        grid.SelectionChanged += (remitente, argumentos) => ActualizarBotones();
        grid.CellDoubleClick += (remitente, argumentos) =>
        {
            if (argumentos.RowIndex >= 0)
            {
                RestaurarSeleccionado();
            }
        };

        ActualizarBotones();
    }

    /// <summary>Se produce cuando cambia la cantidad de registros eliminados.</summary>
    public event EventHandler? CantidadCambio;

    /// <summary>Se produce después de restaurar o borrar definitivamente un registro.</summary>
    public event EventHandler? RegistroModificado;

    /// <summary>Cantidad de registros eliminados de este tipo.</summary>
    public int Cantidad => origen.Count;

    /// <summary>Vuelve a consultar los registros eliminados.</summary>
    public void Recargar()
    {
        List<T>? nuevos = null;
        bool correcto = UiHelper.Ejecutar(propietario.FindForm(), () => { nuevos = servicio.ObtenerEliminados().ToList(); });

        if (!correcto || nuevos == null)
        {
            return;
        }

        origen = nuevos;
        AplicarFiltro();

        EventHandler? manejador = CantidadCambio;
        if (manejador != null)
        {
            manejador(this, EventArgs.Empty);
        }
    }

    /// <summary>Restaura el registro seleccionado.</summary>
    public void RestaurarSeleccionado()
    {
        T? seleccionado = ObtenerSeleccionado();

        if (seleccionado == null)
        {
            return;
        }

        string nombre = nombreMostrado(seleccionado);

        if (!UiHelper.Ejecutar(propietario.FindForm(), () => servicio.Restaurar(seleccionado.Id)))
        {
            return;
        }

        Recargar();
        MostrarExito("✔ Se restauró " + nombreEntidad + " \"" + nombre + "\".");
        AvisarCambio();
    }

    /// <summary>Pide confirmación y borra físicamente el registro seleccionado. No se puede deshacer.</summary>
    public void EliminarDefinitivamenteSeleccionado()
    {
        T? seleccionado = ObtenerSeleccionado();

        if (seleccionado == null)
        {
            return;
        }

        string nombre = nombreMostrado(seleccionado);

        bool confirmado = UiHelper.Confirmar(
            propietario.FindForm(),
            "¿Eliminar DEFINITIVAMENTE " + nombreEntidad + " \"" + nombre + "\"?\n\n" +
            "Esta acción NO se puede deshacer: el registro se borrará de la base de datos y ya no podrá restaurarse.",
            "Eliminar definitivamente");

        if (!confirmado)
        {
            return;
        }

        if (!UiHelper.Ejecutar(propietario.FindForm(), () => servicio.EliminarDefinitivo(seleccionado.Id)))
        {
            return;
        }

        Recargar();
        MostrarExito("✔ Se eliminó definitivamente " + nombreEntidad + " \"" + nombre + "\".");
        AvisarCambio();
    }

    private void AplicarFiltro()
    {
        IReadOnlyList<T> filtrados = servicio.Buscar(origen, txtBuscar.Text);
        grid.DataSource = new ListaOrdenable<T>(filtrados);

        lblEstado.ForeColor = Tema.TextoSuave;
        lblEstado.Text = origen.Count == 0
            ? "No hay registros eliminados."
            : (filtrados.Count == origen.Count
                ? origen.Count + " registro(s) eliminado(s)"
                : "Mostrando " + filtrados.Count + " de " + origen.Count + " registro(s) eliminado(s)");

        ActualizarBotones();
    }

    private T? ObtenerSeleccionado()
    {
        DataGridViewRow? fila = grid.CurrentRow;
        return fila == null ? null : fila.DataBoundItem as T;
    }

    private void ActualizarBotones()
    {
        bool haySeleccion = ObtenerSeleccionado() != null;
        btnRestaurar.Enabled = haySeleccion;
        btnEliminarDefinitivo.Enabled = haySeleccion;
    }

    private void MostrarExito(string mensaje)
    {
        lblEstado.Text = mensaje;
        lblEstado.ForeColor = Tema.Exito;
    }

    private void AvisarCambio()
    {
        EventHandler? manejador = RegistroModificado;
        if (manejador != null)
        {
            manejador(this, EventArgs.Empty);
        }
    }
}
