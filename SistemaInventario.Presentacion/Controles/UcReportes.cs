using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.Entidades.Reportes;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Controles;

/// <summary>
/// Pantalla de reportes: ejecuta y muestra las consultas LINQ de la capa de negocio
/// (agrupaciones, conteos, promedios, rankings y filtros por rango de precios).
/// El aspecto se edita con el diseñador visual (UcReportes.Designer.cs); las columnas de cada reporte
/// cambian según el reporte elegido, por eso se definen aquí en código.
/// </summary>
internal sealed partial class UcReportes : UserControl, IRecargable
{
    private readonly IReporteServicio reportes;
    private readonly ICategoriaServicio categorias;
    private readonly List<DefinicionReporte> definiciones = new List<DefinicionReporte>();
    private bool cargandoCategorias;
    private bool inicializado;

    /// <summary>Crea la pantalla con sus servicios (inyección por constructor).</summary>
    /// <param name="reportes">Servicio de reportes LINQ.</param>
    /// <param name="categorias">Servicio de categorías (para el filtro del reporte por rango de precios).</param>
    public UcReportes(IReporteServicio reportes, ICategoriaServicio categorias)
    {
        this.reportes = reportes ?? throw new ArgumentNullException(nameof(reportes));
        this.categorias = categorias ?? throw new ArgumentNullException(nameof(categorias));

        InitializeComponent();
        UiHelper.GrisarAlDeshabilitar(btnConsultar);
        UiHelper.LimpiarErrorAlEditar(errores, txtMinimo, txtMaximo);

        CrearDefiniciones();

        foreach (DefinicionReporte definicion in definiciones)
        {
            lista.Items.Add(definicion);
        }
    }

    /// <summary>Vuelve a ejecutar el reporte seleccionado con los datos actuales.</summary>
    public void Recargar()
    {
        if (!inicializado)
        {
            return;
        }

        CargarCategorias();
        MostrarReporteSeleccionado();
    }

    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (inicializado || DesignMode)
        {
            return;
        }

        inicializado = true;
        CargarCategorias();
        lista.SelectedIndex = 0;
    }

    private void CrearDefiniciones()
    {
        definiciones.Add(new DefinicionReporte(
            "Productos por categoría",
            "Cantidad de productos activos de cada categoría. Incluye las categorías que todavía no tienen productos.",
            "join … into (group join) + Count + let + orderby descending/ascending",
            () => new ListaOrdenable<ConteoCategoria>(reportes.ProductosAgrupadosPorCategoria()),
            grid =>
            {
                grid.Columns.Add(Columna(nameof(ConteoCategoria.Categoria), "Categoría", 70F));
                grid.Columns.Add(Columna(nameof(ConteoCategoria.CantidadProductos), "Cantidad de productos", 30F, null, true));
            }));

        definiciones.Add(new DefinicionReporte(
            "Top 5 suplidores con más productos",
            "Los cinco suplidores que más productos activos suministran; los empates se resuelven por nombre.",
            "GroupJoin + OrderByDescending + ThenBy + Take(5) + Select con índice",
            () => new ListaOrdenable<RankingSuplidor>(reportes.TopSuplidoresPorProductos(5)),
            grid =>
            {
                grid.Columns.Add(Columna(nameof(RankingSuplidor.Posicion), "Puesto", 12F));
                grid.Columns.Add(Columna(nameof(RankingSuplidor.Suplidor), "Suplidor", 58F));
                grid.Columns.Add(Columna(nameof(RankingSuplidor.CantidadProductos), "Productos", 30F, null, true));
            }));

        definiciones.Add(new DefinicionReporte(
            "Productos por rango de precios",
            "Productos cuyo precio está dentro del rango indicado, opcionalmente de una sola categoría. Deje vacío un límite para no aplicarlo.",
            "Where acumulativo (categoría, mínimo, máximo) + OrderBy + ThenBy",
            () => new ListaOrdenable<Producto>(reportes.ProductosPorRangoDePrecio(
                UiHelper.LeerMontoOpcional(txtMinimo, errores),
                UiHelper.LeerMontoOpcional(txtMaximo, errores),
                UiHelper.SeleccionDe(cboCategoria))),
            grid =>
            {
                grid.Columns.Add(Columna(nameof(Producto.Nombre), "Producto", 36F));
                grid.Columns.Add(Columna(nameof(Producto.CategoriaNombre), "Categoría", 22F));
                grid.Columns.Add(Columna(nameof(Producto.SuplidorNombre), "Suplidor", 24F));
                grid.Columns.Add(Columna(nameof(Producto.PrecioUnitario), "Precio (RD$)", 18F, "N2", true));
            },
            usaParametros: true));

        definiciones.Add(new DefinicionReporte(
            "Promedio de precios por categoría",
            "Precio promedio, mínimo y máximo de los productos de cada categoría.",
            "GroupBy con clave compuesta + Count + Average + Min + Max",
            () => new ListaOrdenable<PromedioCategoria>(reportes.PromedioPreciosPorCategoria()),
            grid =>
            {
                grid.Columns.Add(Columna(nameof(PromedioCategoria.Categoria), "Categoría", 30F));
                grid.Columns.Add(Columna(nameof(PromedioCategoria.CantidadProductos), "Productos", 12F, null, true));
                grid.Columns.Add(Columna(nameof(PromedioCategoria.PrecioPromedio), "Promedio (RD$)", 20F, "N2", true));
                grid.Columns.Add(Columna(nameof(PromedioCategoria.PrecioMinimo), "Mínimo (RD$)", 19F, "N2", true));
                grid.Columns.Add(Columna(nameof(PromedioCategoria.PrecioMaximo), "Máximo (RD$)", 19F, "N2", true));
            }));

        definiciones.Add(new DefinicionReporte(
            "Producto más caro por categoría",
            "El producto de mayor precio de cada categoría.",
            "GroupBy + OrderByDescending + ThenBy + First + Select",
            () => new ListaOrdenable<ProductoDestacado>(reportes.ProductoMasCaroPorCategoria()),
            grid =>
            {
                grid.Columns.Add(Columna(nameof(ProductoDestacado.Categoria), "Categoría", 24F));
                grid.Columns.Add(Columna(nameof(ProductoDestacado.Producto), "Producto", 36F));
                grid.Columns.Add(Columna(nameof(ProductoDestacado.Suplidor), "Suplidor", 24F));
                grid.Columns.Add(Columna(nameof(ProductoDestacado.Precio), "Precio (RD$)", 16F, "N2", true));
            }));

        definiciones.Add(new DefinicionReporte(
            "Categorías sin productos",
            "Categorías activas que no tienen ningún producto activo asociado.",
            "GroupJoin + Where(sin elementos) — equivale a LEFT JOIN … IS NULL",
            () => new ListaOrdenable<CategoriaVacia>(reportes.CategoriasSinProductos()),
            grid =>
            {
                grid.Columns.Add(Columna(nameof(CategoriaVacia.Categoria), "Categoría", 32F));
                grid.Columns.Add(Columna(nameof(CategoriaVacia.Descripcion), "Descripción", 68F));
            }));
    }

    private void lista_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0)
        {
            return;
        }

        bool seleccionado = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

        using (SolidBrush fondo = new SolidBrush(seleccionado ? Tema.Primario : Tema.Superficie))
        {
            e.Graphics.FillRectangle(fondo, e.Bounds);
        }

        string texto = lista.Items[e.Index].ToString() ?? string.Empty;
        Rectangle area = new Rectangle(e.Bounds.X + 14, e.Bounds.Y, e.Bounds.Width - 24, e.Bounds.Height);

        using (SolidBrush pincel = new SolidBrush(seleccionado ? Color.White : Tema.Texto))
        using (StringFormat formato = new StringFormat())
        {
            formato.LineAlignment = StringAlignment.Center;
            formato.Trimming = StringTrimming.EllipsisCharacter;
            e.Graphics.DrawString(texto, Tema.FuenteBase, pincel, area, formato);
        }
    }

    private void lista_SelectedIndexChanged(object? sender, EventArgs e)
    {
        MostrarReporteSeleccionado();
    }

    private void cboCategoria_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!cargandoCategorias)
        {
            MostrarReporteSeleccionado();
        }
    }

    private void btnConsultar_Click(object? sender, EventArgs e)
    {
        MostrarReporteSeleccionado();
    }

    private void MostrarReporteSeleccionado()
    {
        DefinicionReporte? definicion = lista.SelectedItem as DefinicionReporte;

        if (definicion == null)
        {
            return;
        }

        lblReporteTitulo.Text = definicion.Titulo;
        lblReporteDescripcion.Text = definicion.Descripcion;
        lblReporteTecnica.Text = "LINQ: " + definicion.Tecnica;
        panelParametros.Visible = definicion.UsaParametros;

        object? resultado = null;
        bool correcto = UiHelper.Ejecutar(FindForm(), () => { resultado = definicion.Ejecutar(); });

        if (!correcto || resultado == null)
        {
            return;
        }

        grid.DataSource = null;
        grid.Columns.Clear();
        definicion.DefinirColumnas(grid);
        grid.DataSource = resultado;

        lblEstado.Text = grid.Rows.Count + " fila(s)";
    }

    private void CargarCategorias()
    {
        IReadOnlyList<Categoria>? activas = null;

        if (!UiHelper.Ejecutar(FindForm(), () => { activas = categorias.ObtenerActivos(); }) || activas == null)
        {
            return;
        }

        cargandoCategorias = true;

        try
        {
            UiHelper.LlenarComboConTodos(
                cboCategoria,
                "Todas las categorías",
                activas.Select(c => new ItemCombo(c.Id, c.Nombre)));
        }
        finally
        {
            cargandoCategorias = false;
        }
    }

    private static DataGridViewTextBoxColumn Columna(string campo, string encabezado, float peso, string? formato = null, bool derecha = false)
    {
        DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
        columna.DataPropertyName = campo;
        columna.HeaderText = encabezado;
        columna.Name = "col" + campo;
        columna.FillWeight = peso;
        columna.MinimumWidth = 6;
        columna.ReadOnly = true;
        columna.SortMode = DataGridViewColumnSortMode.Automatic;

        if (formato != null)
        {
            columna.DefaultCellStyle.Format = formato;
        }

        if (derecha)
        {
            columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        return columna;
    }

    private sealed class DefinicionReporte
    {
        public DefinicionReporte(
            string titulo,
            string descripcion,
            string tecnica,
            Func<object> ejecutar,
            Action<DataGridView> definirColumnas,
            bool usaParametros = false)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Tecnica = tecnica;
            Ejecutar = ejecutar;
            DefinirColumnas = definirColumnas;
            UsaParametros = usaParametros;
        }

        public string Titulo { get; }

        public string Descripcion { get; }

        public string Tecnica { get; }

        public Func<object> Ejecutar { get; }

        public Action<DataGridView> DefinirColumnas { get; }

        public bool UsaParametros { get; }

        public override string ToString() => Titulo;
    }
}
