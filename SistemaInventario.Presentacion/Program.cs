using System;
using System.IO;
using System.Windows.Forms;
using SistemaInventario.Comun;
using SistemaInventario.LogicaNegocio.Composicion;
using SistemaInventario.Presentacion.Formularios;

namespace SistemaInventario.Presentacion;

/// <summary>Punto de entrada de la aplicación.</summary>
internal static class Program
{
    /// <summary>Configura la aplicación, arma las dependencias y abre la ventana principal.</summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

        ILogger logger = new Logger();

        Application.ThreadException += (remitente, argumentos) =>
            ManejarExcepcionNoControlada(logger, argumentos.Exception);
        AppDomain.CurrentDomain.UnhandledException += (remitente, argumentos) =>
            logger.Error("Excepción no controlada que cierra la aplicación.", argumentos.ExceptionObject as Exception);

        ContenedorServicios servicios;

        try
        {
            servicios = new ContenedorServicios(ConfiguracionApp.ObtenerCadenaConexion(), logger);
        }
        catch (Exception ex) when (ex is FileNotFoundException || ex is InvalidOperationException)
        {
            logger.Error("No se pudo leer la configuración de la aplicación.", ex);
            MessageBox.Show(
                "No se pudo leer la configuración de la aplicación.\n\n" + ex.Message,
                "Sistema de Inventario",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        if (!servicios.ProbarConexion(out string? mensajeError))
        {
            DialogResult respuesta = MessageBox.Show(
                "No se pudo conectar con la base de datos.\n\n" + mensajeError + "\n\n" +
                "Verifique que SQL Server esté en ejecución, que haya ejecutado database\\schema.sql y datos.sql, " +
                "y que la cadena de conexión de appsettings.json sea correcta.\n\n" +
                "¿Desea abrir la aplicación de todos modos?",
                "Sistema de Inventario",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button2);

            if (respuesta == DialogResult.No)
            {
                return;
            }
        }

        Application.Run(new FrmPrincipal(servicios));
    }

    private static void ManejarExcepcionNoControlada(ILogger logger, Exception excepcion)
    {
        logger.Error("Excepción no controlada en la interfaz.", excepcion);

        MessageBox.Show(
            "Ocurrió un error inesperado y se guardó el detalle en el archivo errores.log.\n\n" + excepcion.Message,
            "Sistema de Inventario",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}
