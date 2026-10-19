using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaInventario.LogicaNegocio.Composicion;
using SistemaInventario.Presentacion.Controles;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Formularios;

/// <summary>Ventana principal: barra lateral de navegación y área de contenido con las pantallas del sistema.</summary>
internal sealed partial class FrmPrincipal : Form
{
    private const int AnchoBarraLateral = 240;

    private readonly ContenedorServicios servicios;
    private readonly List<ItemNavegacion> items = new List<ItemNavegacion>();
    private readonly HashSet<Control> vistasYaMostradas = new HashSet<Control>();
    private ItemNavegacion? itemActual;

    /// <summary>Constructor solo para el diseñador visual de Visual Studio; no se usa en tiempo de ejecución.</summary>
    [Obsolete("Solo para el diseñador de Visual Studio. Use el constructor con parámetros.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public FrmPrincipal()
    {
        servicios = null!;
        InitializeComponent();
    }

    /// <summary>Crea la ventana principal con los servicios ya armados (inyección por constructor).</summary>
    /// <param name="servicios">Contenedor con los servicios de la capa de negocio.</param>
    public FrmPrincipal(ContenedorServicios servicios)
    {
        this.servicios = servicios ?? throw new ArgumentNullException(nameof(servicios));

        // El aspecto de la ventana (barra lateral, panel de contenido, pie) vive en FrmPrincipal.Designer.cs
        // y se edita con el diseñador visual. Los botones de navegación se crean aquí porque dependen de las pantallas.
        InitializeComponent();
        Text = UiHelper.TituloAplicacion;

        AgregarVista("Categorías", new UcCategorias(servicios.Categorias));
        AgregarVista("Suplidores", new UcSuplidores(servicios.Suplidores));
        AgregarVista("Productos", new UcProductos(servicios.Productos, servicios.Categorias, servicios.Suplidores));
        AgregarVista("Facturación", new UcFacturas(servicios.Facturas, servicios.Productos));
        AgregarVista("Papelera", new UcPapelera(servicios.Categorias, servicios.Suplidores, servicios.Productos, servicios.Facturas));
        AgregarVista("Reportes (LINQ)", new UcReportes(servicios.Reportes, servicios.Categorias));

        Navegar(items[0]);
    }

    private void btnErrores_Click(object? sender, EventArgs e)
    {
        AbrirRegistroDeErrores();
    }

    private static Button CrearBotonLateral(string texto, Font fuente)
    {
        Button boton = new Button();
        boton.Text = texto;
        boton.TextAlign = ContentAlignment.MiddleLeft;
        boton.FlatStyle = FlatStyle.Flat;
        boton.FlatAppearance.BorderSize = 0;
        boton.FlatAppearance.MouseOverBackColor = Tema.LateralHover;
        boton.BackColor = Tema.Lateral;
        boton.ForeColor = Color.White;
        boton.Font = fuente;
        boton.Size = new Size(AnchoBarraLateral, 50);
        boton.Margin = new Padding(0);
        boton.Padding = new Padding(22, 0, 0, 0);
        boton.Cursor = Cursors.Hand;
        boton.UseVisualStyleBackColor = false;
        return boton;
    }

    private void AgregarVista(string titulo, Control vista)
    {
        Button boton = CrearBotonLateral(titulo, Tema.FuenteBase);
        ItemNavegacion item = new ItemNavegacion(boton, vista);
        boton.Click += (remitente, argumentos) => Navegar(item);

        vista.Dock = DockStyle.Fill;
        vista.Visible = false;

        flujoNavegacion.Controls.Add(boton);
        panelContenido.Controls.Add(vista);
        items.Add(item);
    }

    private void Navegar(ItemNavegacion destino)
    {
        if (itemActual != null)
        {
            itemActual.Vista.Visible = false;
            itemActual.Boton.BackColor = Tema.Lateral;
        }

        itemActual = destino;
        destino.Boton.BackColor = Tema.LateralActivo;
        destino.Vista.Visible = true;
        destino.Vista.BringToFront();

        // La primera vez, la propia pantalla carga sus datos al mostrarse; las siguientes, se refrescan.
        if (!vistasYaMostradas.Add(destino.Vista))
        {
            IRecargable? recargable = destino.Vista as IRecargable;

            if (recargable != null)
            {
                recargable.Recargar();
            }
        }
    }

    private void AbrirRegistroDeErrores()
    {
        string ruta = servicios.Logger.RutaArchivo;

        if (!File.Exists(ruta))
        {
            UiHelper.Informar(this, "Todavía no se ha registrado ningún error.\n\nArchivo de registro:\n" + ruta);
            return;
        }

        try
        {
            ProcessStartInfo informacion = new ProcessStartInfo(ruta);
            informacion.UseShellExecute = true;
            Process.Start(informacion);
        }
        catch (Win32Exception ex)
        {
            UiHelper.Advertir(this, "No se pudo abrir el archivo automáticamente.\n\n" + ruta + "\n\n" + ex.Message);
        }
    }

    private sealed class ItemNavegacion
    {
        public ItemNavegacion(Button boton, Control vista)
        {
            Boton = boton;
            Vista = vista;
        }

        public Button Boton { get; }

        public Control Vista { get; }
    }
}
