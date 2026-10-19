using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.Presentacion.Utilidades;

namespace SistemaInventario.Presentacion.Formularios;

/// <summary>Diálogo para crear o editar un suplidor.</summary>
internal sealed partial class FrmSuplidorEditar : Form
{
    private readonly ISuplidorServicio servicio;
    private readonly Suplidor? suplidorOriginal;
    private readonly Dictionary<string, Control> controlesPorCampo = new Dictionary<string, Control>();

    /// <summary>Constructor solo para el diseñador visual de Visual Studio; no se usa en tiempo de ejecución.</summary>
    [Obsolete("Solo para el diseñador de Visual Studio. Use el constructor con parámetros.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public FrmSuplidorEditar()
    {
        servicio = null!;
        InitializeComponent();
    }

    /// <summary>Crea el diálogo.</summary>
    /// <param name="servicio">Servicio de suplidores.</param>
    /// <param name="suplidor">Suplidor a editar, o <c>null</c> para crear uno nuevo.</param>
    public FrmSuplidorEditar(ISuplidorServicio servicio, Suplidor? suplidor)
    {
        this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        suplidorOriginal = suplidor;

        // El aspecto del formulario vive en FrmSuplidorEditar.Designer.cs (se edita con el diseñador visual).
        InitializeComponent();

        Text = suplidor == null ? "Nuevo suplidor" : "Editar suplidor";

        // Las longitudes máximas salen de la entidad para que exista una sola fuente de verdad.
        txtEmpresa.MaxLength = Suplidor.LongitudMaximaEmpresa;
        txtContacto.MaxLength = Suplidor.LongitudMaximaContacto;
        txtCorreo.MaxLength = Suplidor.LongitudMaximaCorreo;
        txtSitioWeb.MaxLength = Suplidor.LongitudMaximaSitioWeb;

        controlesPorCampo[nameof(Suplidor.NombreEmpresa)] = txtEmpresa;
        controlesPorCampo[nameof(Suplidor.NombreContacto)] = txtContacto;
        controlesPorCampo[nameof(Suplidor.Telefono)] = txtTelefono;
        controlesPorCampo[nameof(Suplidor.Correo)] = txtCorreo;
        controlesPorCampo[nameof(Suplidor.SitioWeb)] = txtSitioWeb;
        UiHelper.LimpiarErrorAlEditar(errores, txtEmpresa, txtContacto, txtTelefono, txtCorreo, txtSitioWeb);

        if (suplidor != null)
        {
            txtEmpresa.Text = suplidor.NombreEmpresa;
            txtContacto.Text = suplidor.NombreContacto;
            txtTelefono.Text = suplidor.Telefono;
            txtCorreo.Text = suplidor.Correo;
            txtSitioWeb.Text = suplidor.SitioWeb ?? string.Empty;
        }
    }

    /// <summary>Identificador del suplidor guardado (válido cuando el diálogo se cierra con Aceptar).</summary>
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

        Suplidor suplidor = new Suplidor();
        suplidor.Id = suplidorOriginal == null ? 0 : suplidorOriginal.Id;
        suplidor.NombreEmpresa = txtEmpresa.Text;
        suplidor.NombreContacto = txtContacto.Text;
        suplidor.Telefono = txtTelefono.Text;
        suplidor.Correo = txtCorreo.Text;
        suplidor.SitioWeb = txtSitioWeb.Text;

        bool correcto = UiHelper.Ejecutar(
            this,
            () =>
            {
                if (suplidor.EsNueva)
                {
                    IdGuardado = servicio.Crear(suplidor);
                }
                else
                {
                    servicio.Actualizar(suplidor);
                    IdGuardado = suplidor.Id;
                }
            },
            erroresValidacion => UiHelper.MostrarErrores(errores, controlesPorCampo, erroresValidacion, this));

        if (correcto)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
