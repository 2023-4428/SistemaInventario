using System.Linq;
using SistemaInventario.Entidades;

namespace SistemaInventario.LogicaNegocio.Validacion;

/// <summary>Valida las fotos de categorías: extensión .jpg/.jpeg/.png, máximo 2 MB y contenido real de imagen.</summary>
public sealed class ValidadorFoto : IValidador<FotoCategoria>
{
    /// <summary>Tamaño máximo permitido por foto: 2 MB.</summary>
    public const long TamanoMaximoBytes = 2L * 1024 * 1024;

    private static readonly string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png" };

    /// <inheritdoc />
    public ResultadoValidacion Validar(FotoCategoria entidad)
    {
        ResultadoValidacion resultado = new ResultadoValidacion();
        string campo = nameof(Categoria.Fotos);
        string extensionArchivo = entidad.Extension;
        bool extensionValida = extensionesPermitidas.Contains(extensionArchivo);

        if (string.IsNullOrWhiteSpace(entidad.NombreArchivo))
        {
            resultado.Agregar(campo, "La foto debe tener un nombre de archivo.");
        }
        else if (!extensionValida)
        {
            resultado.Agregar(campo, "Solo se permiten archivos .jpg, .jpeg o .png.");
        }

        if (entidad.Contenido.Length == 0)
        {
            resultado.Agregar(campo, "El archivo de la foto está vacío.");
        }
        else if (entidad.TamanoBytes > TamanoMaximoBytes)
        {
            resultado.Agregar(campo, "La foto no puede superar los 2 MB.");
        }
        else if (extensionValida && !FirmaCoincide(extensionArchivo, entidad.Contenido))
        {
            resultado.Agregar(campo, "El contenido del archivo no corresponde a una imagen válida del tipo indicado.");
        }

        return resultado;
    }

    private static bool FirmaCoincide(string extensionArchivo, byte[] contenido)
    {
        if (extensionArchivo == ".png")
        {
            byte[] firmaPng = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
            return Comienza(contenido, firmaPng);
        }

        byte[] firmaJpeg = { 0xFF, 0xD8, 0xFF };
        return Comienza(contenido, firmaJpeg);
    }

    private static bool Comienza(byte[] contenido, byte[] firma)
    {
        if (contenido.Length < firma.Length)
        {
            return false;
        }

        for (int i = 0; i < firma.Length; i++)
        {
            if (contenido[i] != firma[i])
            {
                return false;
            }
        }

        return true;
    }
}
