using System;
using System.IO;
using SistemaInventario.Comun;
using SistemaInventario.Comun.Excepciones;
using Xunit;

namespace SistemaInventario.Pruebas;

public class LoggerPruebas : IDisposable
{
    private readonly string carpeta = Path.Combine(Path.GetTempPath(), "SistemaInventarioPruebas_" + Guid.NewGuid().ToString("N"));

    public void Dispose()
    {
        if (Directory.Exists(carpeta))
        {
            Directory.Delete(carpeta, true);
        }
    }

    [Fact]
    public void Error_EscribeFechaNivelMensajeYExcepcionEnElArchivo()
    {
        string ruta = Path.Combine(carpeta, "errores.log");
        Logger logger = new Logger(ruta);

        logger.Error("Falló la consulta de productos", new InvalidOperationException("detalle técnico"));

        string contenido = File.ReadAllText(ruta);
        Assert.Contains("[ERROR]", contenido);
        Assert.Contains("Falló la consulta de productos", contenido);
        Assert.Contains("System.InvalidOperationException", contenido);
        Assert.Contains("detalle técnico", contenido);
    }

    [Fact]
    public void VariosRegistros_SeAgreganAlFinalDelArchivo()
    {
        string ruta = Path.Combine(carpeta, "sub", "errores.log");
        Logger logger = new Logger(ruta);

        logger.Error("primero");
        logger.Advertencia("segundo");
        logger.Informacion("tercero");

        string[] lineas = File.ReadAllLines(ruta);
        Assert.Equal(3, lineas.Length);
        Assert.Contains("[ADVERTENCIA]", lineas[1]);
        Assert.Contains("[INFO]", lineas[2]);
    }

    [Fact]
    public void ElLogger_ExponeLaRutaDelArchivo()
    {
        string ruta = Path.Combine(carpeta, "errores.log");

        Assert.Equal(ruta, new Logger(ruta).RutaArchivo);
        Assert.EndsWith("errores.log", new Logger().RutaArchivo);
    }

    [Fact]
    public void Constructor_ConRutaVacia_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => { new Logger("  "); });
    }

    [Fact]
    public void SiNoPuedeEscribir_NoLanzaExcepciones()
    {
        Directory.CreateDirectory(carpeta);
        string archivoComoCarpeta = Path.Combine(carpeta, "archivo.txt");
        File.WriteAllText(archivoComoCarpeta, "x");

        // La ruta cuelga de un archivo (no de una carpeta): escribir allí es imposible.
        Logger logger = new Logger(Path.Combine(archivoComoCarpeta, "errores.log"));

        Exception? capturada = Record(() => logger.Error("no debe romper la aplicación"));

        Assert.Null(capturada);
    }

    [Fact]
    public void LoggerNulo_NoHaceNada()
    {
        ILogger logger = new LoggerNulo();

        logger.Error("x", new Exception("y"));
        logger.Advertencia("x");
        logger.Informacion("x");

        Assert.Equal(string.Empty, logger.RutaArchivo);
    }

    private static Exception? Record(Action accion)
    {
        try
        {
            accion();
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
