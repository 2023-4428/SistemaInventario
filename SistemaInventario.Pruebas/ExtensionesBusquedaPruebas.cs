using System.Collections.Generic;
using System.Linq;
using SistemaInventario.LogicaNegocio.Utilidades;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ExtensionesBusquedaPruebas
{
    private sealed class Articulo
    {
        public Articulo(string nombre, string? categoria)
        {
            Nombre = nombre;
            Categoria = categoria;
        }

        public string Nombre { get; }
        public string? Categoria { get; }
    }

    private static List<Articulo> Muestra() => new List<Articulo>
    {
        new Articulo("Licuadora de Vaso 600 W", "Hogar y Cocina"),
        new Articulo("Audífonos Bluetooth", "Electrónica"),
        new Articulo("Taladro Inalámbrico", "Herramientas"),
        new Articulo("Cable HDMI", null)
    };

    private static IReadOnlyList<Articulo> Buscar(string? texto) =>
        Muestra().Buscar(texto, a => a.Nombre, a => a.Categoria);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void TextoVacio_DevuelveTodosLosElementos(string? texto)
    {
        Assert.Equal(4, Buscar(texto).Count);
    }

    [Theory]
    [InlineData("licuadora")]
    [InlineData("LICUADORA")]
    [InlineData("LiCuAdOrA")]
    public void IgnoraMayusculasYMinusculas(string texto)
    {
        IReadOnlyList<Articulo> resultado = Buscar(texto);

        Assert.Single(resultado);
        Assert.Equal("Licuadora de Vaso 600 W", resultado[0].Nombre);
    }

    [Theory]
    [InlineData("cuad")]
    [InlineData("dora de")]
    [InlineData("600")]
    public void AdmiteCoincidenciaParcial(string texto)
    {
        Assert.Single(Buscar(texto));
    }

    [Theory]
    [InlineData("audifonos")]
    [InlineData("AUDÍFONOS")]
    [InlineData("electronica")]
    [InlineData("inalambrico")]
    public void IgnoraAcentos(string texto)
    {
        Assert.True(Buscar(texto).Count >= 1);
    }

    [Fact]
    public void VariasPalabras_DebenCoincidirTodas()
    {
        Assert.Single(Buscar("licuadora vaso"));
        Assert.Empty(Buscar("licuadora taladro"));
    }

    [Fact]
    public void BuscaEnTodosLosCamposIndicados()
    {
        IReadOnlyList<Articulo> resultado = Buscar("herramientas");

        Assert.Single(resultado);
        Assert.Equal("Taladro Inalámbrico", resultado[0].Nombre);
    }

    [Fact]
    public void CamposNulos_NoProvocanErrores()
    {
        IReadOnlyList<Articulo> resultado = Buscar("hdmi");

        Assert.Single(resultado);
    }

    [Fact]
    public void SinCoincidencias_DevuelveListaVacia()
    {
        Assert.Empty(Buscar("zzzz"));
    }

    [Fact]
    public void Normalizar_QuitaAcentosEspaciosYMayusculas()
    {
        Assert.Equal("aeiou n", ExtensionesBusqueda.Normalizar("  ÁÉÍÓÚ Ñ  ").Replace("ñ", "n"));
        Assert.Equal(string.Empty, ExtensionesBusqueda.Normalizar(null));
    }
}
