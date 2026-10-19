using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SistemaInventario.Comun.Excepciones;

namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>
/// Funciones de apoyo de la interfaz: mensajes, manejo de errores esperados y pequeños ajustes de controles.
/// La apariencia de las pantallas NO se construye aquí: vive en los archivos <c>.Designer.cs</c> de cada pantalla.
/// </summary>
internal static class UiHelper
{
    /// <summary>Formato de fecha y hora de las columnas.</summary>
    public const string FormatoFechaHora = "dd/MM/yyyy HH:mm";

    /// <summary>Título de los cuadros de mensaje.</summary>
    public const string TituloAplicacion = "Sistema de Inventario";

    // ------------------------------------------------------------------ mensajes

    /// <summary>Muestra un mensaje informativo.</summary>
    public static void Informar(IWin32Window? duenio, string mensaje, string titulo = TituloAplicacion)
    {
        MessageBox.Show(duenio, mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>Muestra una advertencia.</summary>
    public static void Advertir(IWin32Window? duenio, string mensaje, string titulo = TituloAplicacion)
    {
        MessageBox.Show(duenio, mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    /// <summary>Muestra un error.</summary>
    public static void MostrarError(IWin32Window? duenio, string mensaje, string titulo = TituloAplicacion)
    {
        MessageBox.Show(duenio, mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    /// <summary>Pide confirmación al usuario (el botón predeterminado es "No").</summary>
    /// <returns><c>true</c> si el usuario confirma.</returns>
    public static bool Confirmar(IWin32Window? duenio, string mensaje, string titulo = TituloAplicacion)
    {
        DialogResult respuesta = MessageBox.Show(
            duenio,
            mensaje,
            titulo,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);

        return respuesta == DialogResult.Yes;
    }

    /// <summary>
    /// Ejecuta una acción de negocio mostrando al usuario, de forma amigable, los errores esperados:
    /// validación, reglas de negocio, registros inexistentes y errores de base de datos.
    /// </summary>
    /// <param name="duenio">Ventana propietaria de los mensajes.</param>
    /// <param name="accion">Acción a ejecutar.</param>
    /// <param name="alValidar">
    /// Qué hacer con los errores de validación (normalmente, mostrarlos con <c>ErrorProvider</c>).
    /// Si es <c>null</c> se muestran en un cuadro de mensaje.
    /// </param>
    /// <returns><c>true</c> si la acción terminó sin errores.</returns>
    public static bool Ejecutar(
        IWin32Window? duenio,
        Action accion,
        Action<IReadOnlyList<ErrorValidacion>>? alValidar = null)
    {
        try
        {
            accion();
            return true;
        }
        catch (ValidacionException ex)
        {
            if (alValidar != null)
            {
                alValidar(ex.Errores);
            }
            else
            {
                Advertir(duenio, ex.Message, "Revise los datos");
            }

            return false;
        }
        catch (ReglaNegocioException ex)
        {
            Advertir(duenio, ex.Message, "No se pudo completar la operación");
            return false;
        }
        catch (EntidadNoEncontradaException ex)
        {
            Advertir(
                duenio,
                ex.Message + "\n\nEs posible que el registro haya sido eliminado desde otra sesión. Actualice el listado.",
                "Registro no encontrado");
            return false;
        }
        catch (ErrorBaseDatosException ex)
        {
            MostrarError(
                duenio,
                ex.Message + "\n\nEl detalle técnico se guardó en el archivo errores.log.",
                "Error de base de datos");
            return false;
        }
    }

    // ------------------------------------------------------------------ ErrorProvider

    /// <summary>
    /// Muestra cada error junto al control de su campo con <see cref="ErrorProvider"/>.
    /// Los errores de campos sin control asociado se muestran en un cuadro de mensaje.
    /// </summary>
    /// <param name="proveedor">Proveedor de errores del formulario.</param>
    /// <param name="controlesPorCampo">Relación entre el nombre del campo y su control.</param>
    /// <param name="errores">Errores de validación a mostrar.</param>
    /// <param name="duenio">Ventana propietaria de los mensajes.</param>
    public static void MostrarErrores(
        ErrorProvider proveedor,
        IDictionary<string, Control> controlesPorCampo,
        IEnumerable<ErrorValidacion> errores,
        IWin32Window? duenio)
    {
        Dictionary<Control, List<string>> porControl = new Dictionary<Control, List<string>>();
        List<string> sinControl = new List<string>();

        foreach (ErrorValidacion error in errores)
        {
            Control? control;
            if (controlesPorCampo.TryGetValue(error.Campo, out control) && control != null)
            {
                List<string>? mensajes;
                if (!porControl.TryGetValue(control, out mensajes) || mensajes == null)
                {
                    mensajes = new List<string>();
                    porControl[control] = mensajes;
                }

                mensajes.Add(error.Mensaje);
            }
            else
            {
                sinControl.Add(error.Mensaje);
            }
        }

        foreach (KeyValuePair<Control, List<string>> par in porControl)
        {
            proveedor.SetError(par.Key, string.Join(Environment.NewLine, par.Value));
        }

        if (sinControl.Count > 0)
        {
            Advertir(duenio, string.Join(Environment.NewLine, sinControl), "Revise los datos");
        }

        Control? primero = porControl.Keys.OrderBy(c => c.TabIndex).FirstOrDefault();
        if (primero != null)
        {
            primero.Focus();
        }
    }

    /// <summary>Quita el error de un control en cuanto el usuario modifica su valor.</summary>
    /// <param name="proveedor">Proveedor de errores del formulario.</param>
    /// <param name="controles">Controles a vigilar.</param>
    public static void LimpiarErrorAlEditar(ErrorProvider proveedor, params Control[] controles)
    {
        foreach (Control control in controles)
        {
            Control copia = control;
            copia.TextChanged += (remitente, argumentos) => proveedor.SetError(copia, string.Empty);
        }
    }

    // ------------------------------------------------------------------ ajustes de controles

    /// <summary>
    /// Los botones planos no muestran su estado deshabilitado por sí solos: esta ayuda los pinta de gris
    /// mientras estén deshabilitados y les devuelve su color original al habilitarse.
    /// </summary>
    /// <param name="botones">Botones a ajustar (con el color ya definido en el diseñador).</param>
    public static void GrisarAlDeshabilitar(params Button[] botones)
    {
        foreach (Button boton in botones)
        {
            Button copia = boton;
            Color original = copia.BackColor;

            copia.EnabledChanged += (remitente, argumentos) =>
                copia.BackColor = copia.Enabled ? original : Tema.Borde;

            if (!copia.Enabled)
            {
                copia.BackColor = Tema.Borde;
            }
        }
    }

    /// <summary>Alinea a la derecha el encabezado de las columnas cuyo contenido está alineado a la derecha (números).</summary>
    /// <param name="grid">Tabla a ajustar.</param>
    public static void AlinearColumnasNumericas(DataGridView grid)
    {
        foreach (DataGridViewColumn columna in grid.Columns)
        {
            if (columna.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
            {
                columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
    }

    /// <summary>
    /// Llena una lista desplegable de filtro con la opción "todos" (<see cref="ItemCombo.Id"/> nulo) más los elementos dados,
    /// conservando la selección anterior si todavía existe.
    /// </summary>
    /// <param name="combo">Lista desplegable.</param>
    /// <param name="textoTodos">Texto de la opción que representa a todos los registros.</param>
    /// <param name="elementos">Opciones disponibles.</param>
    public static void LlenarComboConTodos(ComboBox combo, string textoTodos, IEnumerable<ItemCombo> elementos)
    {
        ItemCombo? previo = combo.SelectedItem as ItemCombo;
        int? idPrevio = previo == null ? null : previo.Id;

        combo.BeginUpdate();
        combo.Items.Clear();
        combo.Items.Add(new ItemCombo(null, textoTodos));

        foreach (ItemCombo elemento in elementos)
        {
            combo.Items.Add(elemento);
        }

        int indice = 0;
        if (idPrevio.HasValue)
        {
            for (int i = 1; i < combo.Items.Count; i++)
            {
                ItemCombo? candidato = combo.Items[i] as ItemCombo;
                if (candidato != null && candidato.Id == idPrevio)
                {
                    indice = i;
                    break;
                }
            }
        }

        combo.SelectedIndex = indice;
        combo.EndUpdate();
    }

    /// <summary>Obtiene el identificador elegido en una lista de filtro (<c>null</c> = todos).</summary>
    /// <param name="combo">Lista desplegable llenada con <see cref="LlenarComboConTodos"/>.</param>
    /// <returns>Identificador seleccionado, o <c>null</c> para "todos".</returns>
    public static int? SeleccionDe(ComboBox combo)
    {
        ItemCombo? item = combo.SelectedItem as ItemCombo;
        return item == null ? null : item.Id;
    }

    /// <summary>
    /// Lee un monto opcional de un cuadro de texto. Si está vacío devuelve <c>null</c>; si no es un número válido
    /// muestra el error junto al cuadro y devuelve <c>null</c>.
    /// </summary>
    /// <param name="caja">Cuadro de texto con el monto.</param>
    /// <param name="proveedor">Proveedor de errores donde mostrar el aviso.</param>
    /// <returns>El monto, o <c>null</c> si está vacío o es inválido.</returns>
    public static decimal? LeerMontoOpcional(TextBox caja, ErrorProvider proveedor)
    {
        string texto = caja.Text.Trim();

        if (texto.Length == 0)
        {
            proveedor.SetError(caja, string.Empty);
            return null;
        }

        decimal valor;
        if (decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out valor) && valor >= 0)
        {
            proveedor.SetError(caja, string.Empty);
            return valor;
        }

        proveedor.SetError(caja, "Escriba un número válido (por ejemplo, 1500 o 1500.50).");
        return null;
    }
}
