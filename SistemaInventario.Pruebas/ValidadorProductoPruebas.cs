using System.Globalization;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ValidadorProductoPruebas
{
    private readonly ValidadorProducto validador = new ValidadorProducto();

    private static decimal Decimal(string texto) => decimal.Parse(texto, CultureInfo.InvariantCulture);

    [Fact]
    public void ProductoValido_NoTieneErrores()
    {
        Assert.True(validador.Validar(ConstructorDatos.ProductoValido(1, 1)).EsValido);
    }

    [Theory]
    [InlineData("0.01", true)]
    [InlineData("1250.50", true)]
    [InlineData("99999999.99", true)]
    [InlineData("0", false)]
    [InlineData("-5", false)]
    [InlineData("-0.01", false)]
    [InlineData("10.555", false)]
    [InlineData("0.001", false)]
    [InlineData("100000000", false)]
    public void Precio_DebeSerMayorACeroConMaximoDosDecimales(string precio, bool esperadoValido)
    {
        Producto producto = ConstructorDatos.ProductoValido(1, 1, "Producto", Decimal(precio));

        ResultadoValidacion resultado = validador.Validar(producto);

        Assert.Equal(esperadoValido, !resultado.TieneErroresEn(nameof(Producto.PrecioUnitario)));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("a")]
    public void NombreVacioOCorto_EsInvalido(string nombre)
    {
        Producto producto = ConstructorDatos.ProductoValido(1, 1, nombre);

        Assert.True(validador.Validar(producto).TieneErroresEn(nameof(Producto.Nombre)));
    }

    [Fact]
    public void NombreMuyLargo_EsInvalido()
    {
        Producto producto = ConstructorDatos.ProductoValido(1, 1, new string('x', Producto.LongitudMaximaNombre + 1));

        Assert.True(validador.Validar(producto).TieneErroresEn(nameof(Producto.Nombre)));
    }

    [Fact]
    public void SinCategoriaNiSuplidor_SeReportanAmbosCampos()
    {
        Producto producto = ConstructorDatos.ProductoValido(0, 0);

        ResultadoValidacion resultado = validador.Validar(producto);

        Assert.True(resultado.TieneErroresEn(nameof(Producto.CategoriaId)));
        Assert.True(resultado.TieneErroresEn(nameof(Producto.SuplidorId)));
    }
}
