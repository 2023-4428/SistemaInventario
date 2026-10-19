using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaInventario.Entidades;

namespace SistemaInventario.Presentacion.Controles;

/// <summary>
/// Miniatura de una foto de categoría con un botón para quitarla.
/// El aspecto se edita con el diseñador visual (TarjetaFoto.Designer.cs).
/// </summary>
internal sealed partial class TarjetaFoto : UserControl
{
    private MemoryStream? flujo;
    private Image? imagen;

    /// <summary>Crea una tarjeta vacía (la usa el diseñador visual).</summary>
    public TarjetaFoto()
    {
        InitializeComponent();
        Foto = new FotoCategoria();
        Disposed += (remitente, argumentos) => LiberarImagen();
    }

    /// <summary>Crea la miniatura a partir de la foto.</summary>
    /// <param name="foto">Foto a mostrar.</param>
    /// <exception cref="ArgumentException">Si los bytes no corresponden a una imagen válida.</exception>
    public TarjetaFoto(FotoCategoria foto)
        : this()
    {
        if (foto == null)
        {
            throw new ArgumentNullException(nameof(foto));
        }

        Foto = foto;

        // El flujo debe permanecer abierto mientras la imagen exista (requisito de GDI+).
        flujo = new MemoryStream(foto.Contenido);

        try
        {
            imagen = Image.FromStream(flujo);
        }
        catch (ArgumentException)
        {
            LiberarImagen();
            throw;
        }

        visor.Image = imagen;
        lblNombreArchivo.Text = Recortar(foto.NombreArchivo, 18);
        ayuda.SetToolTip(visor, foto.NombreArchivo + " (" + (foto.TamanoBytes / 1024) + " KB)");
    }

    /// <summary>Se produce cuando el usuario pulsa "Quitar".</summary>
    public event EventHandler? QuitarSolicitado;

    /// <summary>Foto representada por la miniatura.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FotoCategoria Foto { get; private set; }

    private void btnQuitar_Click(object? sender, EventArgs e)
    {
        EventHandler? manejador = QuitarSolicitado;

        if (manejador != null)
        {
            manejador(this, EventArgs.Empty);
        }
    }

    private void LiberarImagen()
    {
        visor.Image = null;

        if (imagen != null)
        {
            imagen.Dispose();
            imagen = null;
        }

        if (flujo != null)
        {
            flujo.Dispose();
            flujo = null;
        }
    }

    private static string Recortar(string texto, int maximo)
    {
        return texto.Length <= maximo ? texto : texto.Substring(0, maximo - 1) + "…";
    }
}
