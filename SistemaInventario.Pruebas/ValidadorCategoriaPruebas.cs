using System.Collections.Generic;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ValidadorCategoriaPruebas
{
    private readonly ValidadorCategoria validador = new ValidadorCategoria(new ValidadorFoto());

    [Fact]
    public void CategoriaValida_NoTieneErrores()
    {
        ResultadoValidacion resultado = validador.Validar(ConstructorDatos.CategoriaValida());

        Assert.True(resultado.EsValido);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("ab")]
    [InlineData(" a ")]
    public void NombreVacioOCorto_EsInvalido(string nombre)
    {
        ResultadoValidacion resultado = validador.Validar(ConstructorDatos.CategoriaValida(nombre));

        Assert.False(resultado.EsValido);
        Assert.True(resultado.TieneErroresEn(nameof(Categoria.Nombre)));
    }

    [Theory]
    [InlineData(3, true)]
    [InlineData(100, true)]
    [InlineData(101, false)]
    public void LongitudDelNombre_RespetaLosLimites(int longitud, bool esperadoValido)
    {
        ResultadoValidacion resultado = validador.Validar(ConstructorDatos.CategoriaValida(new string('a', longitud)));

        Assert.Equal(esperadoValido, !resultado.TieneErroresEn(nameof(Categoria.Nombre)));
    }

    [Fact]
    public void DescripcionVacia_EsInvalida()
    {
        Categoria categoria = ConstructorDatos.CategoriaValida();
        categoria.Descripcion = "  ";

        ResultadoValidacion resultado = validador.Validar(categoria);

        Assert.True(resultado.TieneErroresEn(nameof(Categoria.Descripcion)));
    }

    [Theory]
    [InlineData(500, true)]
    [InlineData(501, false)]
    public void LongitudDeLaDescripcion_RespetaElMaximoDe500(int longitud, bool esperadoValido)
    {
        Categoria categoria = ConstructorDatos.CategoriaValida();
        categoria.Descripcion = new string('d', longitud);

        ResultadoValidacion resultado = validador.Validar(categoria);

        Assert.Equal(esperadoValido, !resultado.TieneErroresEn(nameof(Categoria.Descripcion)));
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    public void CantidadDeFotos_AdmiteDeCeroACinco(int cantidad, bool esperadoValido)
    {
        Categoria categoria = ConstructorDatos.CategoriaValida();
        List<FotoCategoria> fotos = new List<FotoCategoria>();
        for (int i = 0; i < cantidad; i++)
        {
            fotos.Add(ConstructorDatos.FotoJpg("foto" + i + ".jpg"));
        }

        categoria.EstablecerFotos(fotos);
        ResultadoValidacion resultado = validador.Validar(categoria);

        Assert.Equal(esperadoValido, resultado.EsValido);
    }

    [Fact]
    public void FotoInvalida_SeReportaEnElCampoFotos()
    {
        Categoria categoria = ConstructorDatos.CategoriaValida();
        categoria.AgregarFoto(new FotoCategoria { NombreArchivo = "documento.pdf", Contenido = new byte[] { 1, 2, 3 } });

        ResultadoValidacion resultado = validador.Validar(categoria);

        Assert.False(resultado.EsValido);
        Assert.True(resultado.TieneErroresEn(nameof(Categoria.Fotos)));
    }
}
