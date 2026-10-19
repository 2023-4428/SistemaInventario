using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ValidadorSuplidorPruebas
{
    private readonly ValidadorSuplidor validador = new ValidadorSuplidor();

    [Fact]
    public void SuplidorValido_NoTieneErrores()
    {
        Assert.True(validador.Validar(ConstructorDatos.SuplidorValido()).EsValido);
    }

    [Theory]
    [InlineData("809-555-0101")]
    [InlineData("829-123-4567")]
    [InlineData("849-000-0000")]
    public void TelefonosDominicanosValidos_SeAceptan(string telefono)
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.Telefono = telefono;

        Assert.False(validador.Validar(suplidor).TieneErroresEn(nameof(Suplidor.Telefono)));
    }

    [Theory]
    [InlineData("")]
    [InlineData("800-555-0101")]
    [InlineData("809-5550101")]
    [InlineData("8095550101")]
    [InlineData("809-555-010")]
    [InlineData("809-555-01011")]
    [InlineData("(809) 555-0101")]
    [InlineData("809-555-010a")]
    [InlineData("+1-809-555-0101")]
    public void TelefonosInvalidos_SeRechazan(string telefono)
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.Telefono = telefono;

        Assert.True(validador.Validar(suplidor).TieneErroresEn(nameof(Suplidor.Telefono)));
    }

    [Theory]
    [InlineData("ventas@empresa.com")]
    [InlineData("a.b+c@sub.dominio.com.do")]
    [InlineData("info@mi-empresa.do")]
    public void CorreosValidos_SeAceptan(string correo)
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.Correo = correo;

        Assert.False(validador.Validar(suplidor).TieneErroresEn(nameof(Suplidor.Correo)));
    }

    [Theory]
    [InlineData("")]
    [InlineData("ventas@")]
    [InlineData("ventas.empresa.com")]
    [InlineData("@empresa.com")]
    [InlineData("ventas@empresa")]
    [InlineData("ventas @empresa.com")]
    public void CorreosInvalidos_SeRechazan(string correo)
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.Correo = correo;

        Assert.True(validador.Validar(suplidor).TieneErroresEn(nameof(Suplidor.Correo)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("https://www.empresa.com")]
    [InlineData("http://empresa.do")]
    [InlineData("https://empresa.com/catalogo?id=5")]
    public void SitioWebOpcionalOValido_SeAcepta(string? sitio)
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.SitioWeb = sitio;

        Assert.False(validador.Validar(suplidor).TieneErroresEn(nameof(Suplidor.SitioWeb)));
    }

    [Theory]
    [InlineData("www.empresa.com")]
    [InlineData("empresa")]
    [InlineData("ftp://empresa.com")]
    [InlineData("https://")]
    [InlineData("https://exa mple.com")]
    public void SitioWebInvalido_SeRechaza(string sitio)
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.SitioWeb = sitio;

        Assert.True(validador.Validar(suplidor).TieneErroresEn(nameof(Suplidor.SitioWeb)));
    }

    [Fact]
    public void NombresVacios_SeRechazan()
    {
        Suplidor suplidor = ConstructorDatos.SuplidorValido();
        suplidor.NombreEmpresa = " ";
        suplidor.NombreContacto = string.Empty;

        ResultadoValidacion resultado = validador.Validar(suplidor);

        Assert.True(resultado.TieneErroresEn(nameof(Suplidor.NombreEmpresa)));
        Assert.True(resultado.TieneErroresEn(nameof(Suplidor.NombreContacto)));
    }
}
