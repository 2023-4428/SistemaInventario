using System;
using SistemaInventario.Comun.Excepciones;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ExcepcionesPruebas
{
    [Theory]
    [InlineData(2627, true, false)]
    [InlineData(2601, true, false)]
    [InlineData(547, false, true)]
    [InlineData(1205, false, false)]
    public void ErrorBaseDatos_ClasificaLosCodigosDeSqlServer(int codigo, bool esUnica, bool esClaveForanea)
    {
        ErrorBaseDatosException ex = new ErrorBaseDatosException("mensaje", null, codigo);

        Assert.Equal(esUnica, ex.EsViolacionUnica);
        Assert.Equal(esClaveForanea, ex.EsViolacionClaveForanea);
        Assert.Equal(codigo, ex.CodigoSql);
    }

    [Fact]
    public void ErrorBaseDatos_SinCodigo_NoEsDuplicadoNiClaveForanea()
    {
        ErrorBaseDatosException ex = new ErrorBaseDatosException("sin conexión", new Exception("interna"));

        Assert.False(ex.EsViolacionUnica);
        Assert.False(ex.EsViolacionClaveForanea);
        Assert.Null(ex.CodigoSql);
        Assert.NotNull(ex.InnerException);
    }

    [Fact]
    public void Validacion_ResumeTodosLosMensajes()
    {
        ValidacionException ex = new ValidacionException(new[]
        {
            new ErrorValidacion("Nombre", "El nombre es obligatorio."),
            new ErrorValidacion("Precio", "El precio debe ser mayor que 0.")
        });

        Assert.Equal(2, ex.Errores.Count);
        Assert.Contains("El nombre es obligatorio.", ex.Message);
        Assert.Contains("El precio debe ser mayor que 0.", ex.Message);
    }

    [Fact]
    public void EntidadNoEncontrada_IndicaEntidadEId()
    {
        EntidadNoEncontradaException ex = new EntidadNoEncontradaException("categoría", 42);

        Assert.Equal("categoría", ex.Entidad);
        Assert.Equal(42, ex.Id);
        Assert.Contains("42", ex.Message);
    }
}
