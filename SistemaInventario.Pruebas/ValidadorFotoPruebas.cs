using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;
using SistemaInventario.Pruebas.Soporte;
using Xunit;

namespace SistemaInventario.Pruebas;

public class ValidadorFotoPruebas
{
    private readonly ValidadorFoto validador = new ValidadorFoto();

    [Theory]
    [InlineData("foto.jpg")]
    [InlineData("foto.jpeg")]
    [InlineData("FOTO.JPG")]
    [InlineData("mi foto (1).Jpeg")]
    public void ArchivosJpegValidos_SeAceptan(string nombre)
    {
        FotoCategoria foto = new FotoCategoria { NombreArchivo = nombre, Contenido = ConstructorDatos.BytesJpeg() };

        Assert.True(validador.Validar(foto).EsValido);
    }

    [Fact]
    public void ArchivoPngValido_SeAcepta()
    {
        Assert.True(validador.Validar(ConstructorDatos.FotoPng()).EsValido);
    }

    [Theory]
    [InlineData("foto.gif")]
    [InlineData("foto.bmp")]
    [InlineData("foto.pdf")]
    [InlineData("foto")]
    [InlineData("foto.jpg.exe")]
    public void ExtensionesNoPermitidas_SeRechazan(string nombre)
    {
        FotoCategoria foto = new FotoCategoria { NombreArchivo = nombre, Contenido = ConstructorDatos.BytesJpeg() };

        ResultadoValidacion resultado = validador.Validar(foto);

        Assert.False(resultado.EsValido);
        Assert.True(resultado.TieneErroresEn(nameof(Categoria.Fotos)));
    }

    [Fact]
    public void ArchivoVacio_SeRechaza()
    {
        FotoCategoria foto = new FotoCategoria { NombreArchivo = "foto.jpg", Contenido = new byte[0] };

        Assert.False(validador.Validar(foto).EsValido);
    }

    [Fact]
    public void ArchivoDeExactamenteDosMegabytes_SeAcepta()
    {
        FotoCategoria foto = new FotoCategoria
        {
            NombreArchivo = "grande.jpg",
            Contenido = ConstructorDatos.BytesJpeg((int)ValidadorFoto.TamanoMaximoBytes)
        };

        Assert.True(validador.Validar(foto).EsValido);
    }

    [Fact]
    public void ArchivoMayorADosMegabytes_SeRechaza()
    {
        FotoCategoria foto = new FotoCategoria
        {
            NombreArchivo = "enorme.jpg",
            Contenido = ConstructorDatos.BytesJpeg((int)ValidadorFoto.TamanoMaximoBytes + 1)
        };

        ResultadoValidacion resultado = validador.Validar(foto);

        Assert.False(resultado.EsValido);
        Assert.Contains("2 MB", resultado.Errores[0].Mensaje);
    }

    [Fact]
    public void ContenidoQueNoCorrespondeALaExtension_SeRechaza()
    {
        FotoCategoria pngFalso = new FotoCategoria { NombreArchivo = "falso.png", Contenido = ConstructorDatos.BytesJpeg() };
        FotoCategoria jpgFalso = new FotoCategoria { NombreArchivo = "falso.jpg", Contenido = ConstructorDatos.BytesPng() };
        FotoCategoria textoRenombrado = new FotoCategoria { NombreArchivo = "texto.jpg", Contenido = new byte[] { 72, 111, 108, 97, 33 } };

        Assert.False(validador.Validar(pngFalso).EsValido);
        Assert.False(validador.Validar(jpgFalso).EsValido);
        Assert.False(validador.Validar(textoRenombrado).EsValido);
    }
}
