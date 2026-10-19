using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.LogicaNegocio.Validacion;
using SistemaInventario.Presentacion.Controles;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Formularios;

/// <summary>Diálogo para crear o editar una categoría y administrar sus fotos (0 a 5).</summary>
internal sealed partial class FrmCategoriaEditar : Form
{
    private readonly ICategoriaServicio servicio;
    private readonly Categoria? categoriaOriginal;
    private readonly List<FotoCategoria> fotos = new List<FotoCategoria>();
    private readonly Dictionary<string, Control> controlesPorCampo = new Dictionary<string, Control>();

    /// <summary>Constructor solo para el diseñador visual de Visual Studio; no se usa en tiempo de ejecución.</summary>
    [Obsolete("Solo para el diseñador de Visual Studio. Use el constructor con parámetros.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public FrmCategoriaEditar()
    {
        servicio = null!;
        InitializeComponent();
    }

    /// <summary>Crea el diálogo.</summary>
    /// <param name="servicio">Servicio de categorías.</param>
    /// <param name="categoria">Categoría a editar con sus fotos cargadas, o <c>null</c> para crear una nueva.</param>
    public FrmCategoriaEditar(ICategoriaServicio servicio, Categoria? categoria)
    {
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        categoriaOriginal = categoria;

        // El aspecto del formulario vive en FrmCategoriaEditar.Designer.cs (se edita con el diseñador visual).
        InitializeComponent();

        Text = categoria == null ? "Nueva categoría" : "Editar categoría";
        txtNombre.MaxLength = Categoria.LongitudMaximaNombre;
        txtDescripcion.MaxLength = Categoria.LongitudMaximaDescripcion;
        lblAyudaFotos.Text = "Hasta " + Categoria.MaximoFotos + " · .jpg, .jpeg o .png · máx. 2 MB cada una";

        controlesPorCampo[nameof(Categoria.Nombre)] = txtNombre;
        controlesPorCampo[nameof(Categoria.Descripcion)] = txtDescripcion;
        controlesPorCampo[nameof(Categoria.Fotos)] = btnAgregarFoto;
        UiHelper.LimpiarErrorAlEditar(errores, txtNombre, txtDescripcion);
        txtDescripcion.TextChanged += (remitente, argumentos) => ActualizarContadores();

        if (categoria != null)
        {
            txtNombre.Text = categoria.Nombre;
            txtDescripcion.Text = categoria.Descripcion;

            foreach (FotoCategoria foto in categoria.Fotos)
            {
                if (!IntentarAgregarFoto(foto))
                {
                    // La imagen guardada no se puede mostrar, pero se conserva para no perderla al guardar.
                    fotos.Add(foto);
                }
            }
        }

        ActualizarContadores();
    }

    /// <summary>Identificador de la categoría guardada (válido cuando el diálogo se cierra con Aceptar).</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int IdGuardado { get; private set; }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        Guardar();
    }

    private void btnAgregarFoto_Click(object? sender, EventArgs e)
    {
        AgregarFotos();
    }

    private void Guardar()
    {
        errores.Clear();

        Categoria categoria = new Categoria();
        categoria.Id = categoriaOriginal == null ? 0 : categoriaOriginal.Id;
        categoria.Nombre = txtNombre.Text;
        categoria.Descripcion = txtDescripcion.Text;
        categoria.EstablecerFotos(fotos);

        bool correcto = UiHelper.Ejecutar(
            this,
            () =>
            {
                if (categoria.EsNueva)
                {
                    IdGuardado = servicio.Crear(categoria);
                }
                else
                {
                    servicio.Actualizar(categoria);
                    IdGuardado = categoria.Id;
                }
            },
            erroresValidacion => UiHelper.MostrarErrores(errores, controlesPorCampo, erroresValidacion, this));

        if (correcto)
        {
            DialogResult = DialogResult.OK;
        }
    }

    private void AgregarFotos()
    {
        errores.SetError(btnAgregarFoto, string.Empty);

        using (OpenFileDialog dialogo = new OpenFileDialog())
        {
            dialogo.Title = "Seleccione las fotos de la categoría";
            dialogo.Filter = "Imágenes (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            dialogo.Multiselect = true;
            dialogo.CheckFileExists = true;

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            List<string> rechazos = new List<string>();

            foreach (string ruta in dialogo.FileNames)
            {
                if (fotos.Count >= Categoria.MaximoFotos)
                {
                    rechazos.Add("Se alcanzó el máximo de " + Categoria.MaximoFotos + " fotos; no se agregó " + Path.GetFileName(ruta) + ".");
                    break;
                }

                string? motivo = CargarFoto(ruta);

                if (motivo != null)
                {
                    rechazos.Add(motivo);
                }
            }

            ActualizarContadores();

            if (rechazos.Count > 0)
            {
                UiHelper.Advertir(this, string.Join(Environment.NewLine, rechazos), "Algunas fotos no se agregaron");
            }
        }
    }

    /// <summary>Lee y valida un archivo; devuelve el motivo del rechazo o <c>null</c> si se agregó.</summary>
    private string? CargarFoto(string ruta)
    {
        FileInfo info = new FileInfo(ruta);

        if (info.Length > ValidadorFoto.TamanoMaximoBytes)
        {
            return info.Name + ": supera el tamaño máximo de 2 MB.";
        }

        byte[] contenido;

        try
        {
            contenido = File.ReadAllBytes(ruta);
        }
        catch (IOException ex)
        {
            return info.Name + ": no se pudo leer el archivo (" + ex.Message + ").";
        }
        catch (UnauthorizedAccessException ex)
        {
            return info.Name + ": no se tiene permiso para leer el archivo (" + ex.Message + ").";
        }

        FotoCategoria foto = new FotoCategoria();
        foto.NombreArchivo = info.Name;
        foto.Contenido = contenido;

        ResultadoValidacion resultado = servicio.ValidarFoto(foto);

        if (!resultado.EsValido)
        {
            return info.Name + ": " + resultado.Errores[0].Mensaje;
        }

        if (!IntentarAgregarFoto(foto))
        {
            return info.Name + ": el archivo está dañado o no es una imagen válida.";
        }

        return null;
    }

    private bool IntentarAgregarFoto(FotoCategoria foto)
    {
        TarjetaFoto tarjeta;

        try
        {
            tarjeta = new TarjetaFoto(foto);
        }
        catch (ArgumentException)
        {
            return false;
        }

        tarjeta.QuitarSolicitado += (remitente, argumentos) => QuitarFoto(tarjeta);
        fotos.Add(foto);
        panelFotos.Controls.Add(tarjeta);
        return true;
    }

    private void QuitarFoto(TarjetaFoto tarjeta)
    {
        fotos.Remove(tarjeta.Foto);
        panelFotos.Controls.Remove(tarjeta);
        tarjeta.Dispose();
        errores.SetError(btnAgregarFoto, string.Empty);
        ActualizarContadores();
    }

    private void ActualizarContadores()
    {
        lblContador.Text = txtDescripcion.TextLength + "/" + Categoria.LongitudMaximaDescripcion;
        lblInfoFotos.Text = fotos.Count + " de " + Categoria.MaximoFotos + " fotos";
        btnAgregarFoto.Enabled = fotos.Count < Categoria.MaximoFotos;
        btnAgregarFoto.BackColor = btnAgregarFoto.Enabled ? Color.White : Tema.Borde;
    }
}
