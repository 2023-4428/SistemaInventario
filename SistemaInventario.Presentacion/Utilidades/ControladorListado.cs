using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SistemaInventario.Entidades;

namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>
/// Lógica común de las pantallas de listado (cuadro de búsqueda con filtrado en tiempo real, tabla ordenable,
/// conservación de la selección y botones que dependen de la selección).
/// <para>
/// Es una clase sin interfaz: recibe los controles que la pantalla dibujó en su diseñador. Así cada pantalla puede
/// editarse libremente en el diseñador visual de Visual Studio sin romper la lógica.
/// </para>
/// </summary>
/// <typeparam name="T">Entidad que se lista.</typeparam>
internal sealed class ControladorListado<T> where T : EntidadBase
{
    private readonly Control propietario;
    private readonly DataGridView grid;
    private readonly TextBox txtBuscar;
    private readonly Label lblEstado;
    private readonly string nombrePlural;
    private readonly Func<IReadOnlyList<T>> cargarDatos;
    private readonly Func<IReadOnlyList<T>, string, IReadOnlyList<T>> filtrar;
    private readonly List<Button> botonesDeSeleccion = new List<Button>();

    private List<T> origen = new List<T>();
    private bool inicializado;

    /// <summary>Crea el controlador y conecta los eventos de búsqueda y de la tabla.</summary>
    /// <param name="propietario">Pantalla que contiene los controles (se usa para mostrar los mensajes).</param>
    /// <param name="grid">Tabla con las columnas ya definidas en el diseñador.</param>
    /// <param name="txtBuscar">Cuadro de búsqueda.</param>
    /// <param name="lblEstado">Etiqueta donde se muestra el contador de registros.</param>
    /// <param name="nombrePlural">Nombre plural de lo que se lista, para el contador ("categorías").</param>
    /// <param name="cargarDatos">Obtiene los registros desde la capa de negocio.</param>
    /// <param name="filtrar">Aplica la búsqueda (y otros filtros) a los datos ya cargados, sin volver a consultar.</param>
    public ControladorListado(
        Control propietario,
        DataGridView grid,
        TextBox txtBuscar,
        Label lblEstado,
        string nombrePlural,
        Func<IReadOnlyList<T>> cargarDatos,
        Func<IReadOnlyList<T>, string, IReadOnlyList<T>> filtrar)
    {
        this.propietario = propietario ?? throw new ArgumentNullException(nameof(propietario));
        this.grid = grid ?? throw new ArgumentNullException(nameof(grid));
        this.txtBuscar = txtBuscar ?? throw new ArgumentNullException(nameof(txtBuscar));
        this.lblEstado = lblEstado ?? throw new ArgumentNullException(nameof(lblEstado));
        this.nombrePlural = nombrePlural;
        this.cargarDatos = cargarDatos ?? throw new ArgumentNullException(nameof(cargarDatos));
        this.filtrar = filtrar ?? throw new ArgumentNullException(nameof(filtrar));

        txtBuscar.TextChanged += (remitente, argumentos) => AplicarFiltro();
        grid.SelectionChanged += (remitente, argumentos) => ActualizarBotones();
        grid.CellDoubleClick += (remitente, argumentos) =>
        {
            if (argumentos.RowIndex >= 0)
            {
                EditarSeleccionado();
            }
        };
        grid.KeyDown += (remitente, argumentos) =>
        {
            if (argumentos.KeyCode == Keys.Delete)
            {
                EliminarSeleccionado();
                argumentos.Handled = true;
            }
            else if (argumentos.KeyCode == Keys.Enter)
            {
                EditarSeleccionado();
                argumentos.Handled = true;
                argumentos.SuppressKeyPress = true;
            }
        };
    }

    /// <summary>Acción de editar (o ver) el registro seleccionado. Si es <c>null</c> no se hace nada.</summary>
    public Action<T>? AlEditar { get; set; }

    /// <summary>Acción de eliminar el registro seleccionado. Si es <c>null</c> no se hace nada.</summary>
    public Action<T>? AlEliminar { get; set; }

    /// <summary>Indica los botones que solo deben estar habilitados cuando hay una fila seleccionada.</summary>
    /// <param name="botones">Botones que dependen de la selección.</param>
    public void AsociarBotonesDeSeleccion(params Button[] botones)
    {
        botonesDeSeleccion.AddRange(botones);
        ActualizarBotones();
    }

    /// <summary>Carga los datos por primera vez (se llama cuando la pantalla se muestra).</summary>
    public void Iniciar()
    {
        if (inicializado)
        {
            return;
        }

        inicializado = true;
        Recargar(null);
    }

    /// <summary>Vuelve a consultar los datos conservando la selección actual.</summary>
    public void Recargar()
    {
        Recargar(null);
    }

    /// <summary>Vuelve a consultar los datos y selecciona el registro indicado.</summary>
    /// <param name="idASeleccionar">Identificador a seleccionar; <c>null</c> conserva la selección actual.</param>
    public void Recargar(int? idASeleccionar)
    {
        if (!inicializado)
        {
            return;
        }

        int? idActual = idASeleccionar;
        if (!idActual.HasValue)
        {
            T? actual = ObtenerSeleccionado();
            if (actual != null)
            {
                idActual = actual.Id;
            }
        }

        List<T>? nuevos = null;
        bool correcto = UiHelper.Ejecutar(propietario.FindForm(), () => { nuevos = cargarDatos().ToList(); });

        if (!correcto || nuevos == null)
        {
            return;
        }

        origen = nuevos;
        AplicarFiltro(idActual);
    }

    /// <summary>Vuelve a aplicar la búsqueda y los filtros a los datos cargados (filtrado en tiempo real).</summary>
    public void AplicarFiltro()
    {
        if (!inicializado)
        {
            return;
        }

        T? actual = ObtenerSeleccionado();
        AplicarFiltro(actual?.Id);
    }

    /// <summary>Obtiene el registro de la fila seleccionada.</summary>
    /// <returns>El registro seleccionado, o <c>null</c> si no hay selección.</returns>
    public T? ObtenerSeleccionado()
    {
        DataGridViewRow? fila = grid.CurrentRow;
        return fila == null ? null : fila.DataBoundItem as T;
    }

    /// <summary>Ejecuta <see cref="AlEditar"/> con el registro seleccionado.</summary>
    public void EditarSeleccionado()
    {
        T? seleccionado = ObtenerSeleccionado();

        if (seleccionado != null && AlEditar != null)
        {
            AlEditar(seleccionado);
        }
    }

    /// <summary>Ejecuta <see cref="AlEliminar"/> con el registro seleccionado.</summary>
    public void EliminarSeleccionado()
    {
        T? seleccionado = ObtenerSeleccionado();

        if (seleccionado != null && AlEliminar != null)
        {
            AlEliminar(seleccionado);
        }
    }

    private void AplicarFiltro(int? idASeleccionar)
    {
        IReadOnlyList<T> filtrados = filtrar(origen, txtBuscar.Text);

        DataGridViewColumn? columnaOrden = grid.SortedColumn;
        ListSortDirection direccion = grid.SortOrder == SortOrder.Descending
            ? ListSortDirection.Descending
            : ListSortDirection.Ascending;

        grid.DataSource = new ListaOrdenable<T>(filtrados);

        if (columnaOrden != null)
        {
            grid.Sort(columnaOrden, direccion);
        }

        if (idASeleccionar.HasValue)
        {
            SeleccionarPorId(idASeleccionar.Value);
        }

        lblEstado.Text = filtrados.Count == origen.Count
            ? origen.Count + " " + nombrePlural
            : "Mostrando " + filtrados.Count + " de " + origen.Count + " " + nombrePlural;

        ActualizarBotones();
    }

    private void SeleccionarPorId(int id)
    {
        foreach (DataGridViewRow fila in grid.Rows)
        {
            T? elemento = fila.DataBoundItem as T;

            if (elemento != null && elemento.Id == id)
            {
                grid.CurrentCell = fila.Cells[0];
                fila.Selected = true;
                return;
            }
        }
    }

    private void ActualizarBotones()
    {
        bool haySeleccion = ObtenerSeleccionado() != null;

        foreach (Button boton in botonesDeSeleccion)
        {
            boton.Enabled = haySeleccion;
        }
    }
}
