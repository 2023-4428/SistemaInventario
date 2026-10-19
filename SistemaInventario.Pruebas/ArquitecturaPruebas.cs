using System;
using System.IO;
using System.Linq;
using Xunit;

namespace SistemaInventario.Pruebas;

/// <summary>
/// Pruebas que vigilan las reglas de la arquitectura por capas leyendo los archivos del proyecto.
/// </summary>
public class ArquitecturaPruebas
{
    private static string RaizDeCodigo()
    {
        string? definida = Environment.GetEnvironmentVariable("SISTEMAINVENTARIO_SRC");
        if (!string.IsNullOrWhiteSpace(definida))
        {
            return definida;
        }

        DirectoryInfo? directorio = new DirectoryInfo(AppContext.BaseDirectory);
        while (directorio != null && !File.Exists(Path.Combine(directorio.FullName, "SistemaInventario.sln")))
        {
            directorio = directorio.Parent;
        }

        Assert.NotNull(directorio);
        return directorio!.FullName;
    }

    private static string Proyecto(string capa) =>
        File.ReadAllText(Path.Combine(RaizDeCodigo(), "SistemaInventario." + capa, "SistemaInventario." + capa + ".csproj"));

    [Fact]
    public void LaSolucion_TieneAlMenosCuatroProyectosDeLasCapasExigidas()
    {
        foreach (string capa in new[] { "Entidades", "AccesoDatos", "LogicaNegocio", "Presentacion" })
        {
            Assert.True(
                File.Exists(Path.Combine(RaizDeCodigo(), "SistemaInventario." + capa, "SistemaInventario." + capa + ".csproj")),
                "Falta el proyecto " + capa);
        }
    }

    [Fact]
    public void Presentacion_NoReferenciaDirectamenteAAccesoDatos()
    {
        Assert.DoesNotContain("AccesoDatos", Proyecto("Presentacion"));
    }

    [Fact]
    public void Presentacion_NoUsaTiposDeAccesoDatosEnSuCodigo()
    {
        string carpeta = Path.Combine(RaizDeCodigo(), "SistemaInventario.Presentacion");

        string[] archivosConReferencia = Directory
            .GetFiles(carpeta, "*.cs", SearchOption.AllDirectories)
            .Where(ruta => !ruta.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar))
            .Where(ruta => File.ReadAllText(ruta).Contains("SistemaInventario.AccesoDatos"))
            .ToArray();

        Assert.Empty(archivosConReferencia);
    }

    [Fact]
    public void Entidades_NoDependeDeNingunaOtraCapa()
    {
        Assert.DoesNotContain("ProjectReference", Proyecto("Entidades"));
    }

    [Fact]
    public void AccesoDatos_NoReferenciaLogicaNegocioNiPresentacion()
    {
        string proyecto = Proyecto("AccesoDatos");

        Assert.DoesNotContain("LogicaNegocio", proyecto);
        Assert.DoesNotContain("Presentacion", proyecto);
    }

    [Fact]
    public void LogicaNegocio_NoReferenciaPresentacion()
    {
        Assert.DoesNotContain("Presentacion", Proyecto("LogicaNegocio"));
    }
}
