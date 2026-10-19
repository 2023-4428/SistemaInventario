using SistemaInventario.Entidades;

namespace SistemaInventario.LogicaNegocio.Validacion;

/// <summary>Valida los campos de un <see cref="Suplidor"/>: nombres, teléfono dominicano, correo y sitio web.</summary>
public sealed class ValidadorSuplidor : IValidador<Suplidor>
{
    /// <inheritdoc />
    public ResultadoValidacion Validar(Suplidor entidad)
    {
        ResultadoValidacion resultado = new ResultadoValidacion();

        ValidarTexto(
            resultado,
            nameof(Suplidor.NombreEmpresa),
            "El nombre de la empresa",
            entidad.NombreEmpresa,
            Suplidor.LongitudMinimaNombre,
            Suplidor.LongitudMaximaEmpresa);

        ValidarTexto(
            resultado,
            nameof(Suplidor.NombreContacto),
            "El nombre del contacto",
            entidad.NombreContacto,
            Suplidor.LongitudMinimaNombre,
            Suplidor.LongitudMaximaContacto);

        string telefono = (entidad.Telefono ?? string.Empty).Trim();
        if (telefono.Length == 0)
        {
            resultado.Agregar(nameof(Suplidor.Telefono), "El teléfono es obligatorio.");
        }
        else if (!PatronesValidacion.EsTelefonoValido(telefono))
        {
            resultado.Agregar(
                nameof(Suplidor.Telefono),
                "El teléfono debe tener el formato 809-XXX-XXXX (también se aceptan los prefijos 829 y 849).");
        }

        string correo = (entidad.Correo ?? string.Empty).Trim();
        if (correo.Length == 0)
        {
            resultado.Agregar(nameof(Suplidor.Correo), "El correo electrónico es obligatorio.");
        }
        else if (correo.Length > Suplidor.LongitudMaximaCorreo || !PatronesValidacion.EsCorreoValido(correo))
        {
            resultado.Agregar(nameof(Suplidor.Correo), "El correo electrónico no tiene un formato válido (ejemplo: ventas@empresa.com).");
        }

        string sitioWeb = (entidad.SitioWeb ?? string.Empty).Trim();
        if (sitioWeb.Length > 0)
        {
            if (sitioWeb.Length > Suplidor.LongitudMaximaSitioWeb)
            {
                resultado.Agregar(
                    nameof(Suplidor.SitioWeb),
                    $"El sitio web no puede exceder {Suplidor.LongitudMaximaSitioWeb} caracteres.");
            }
            else if (!PatronesValidacion.EsUrlValida(sitioWeb))
            {
                resultado.Agregar(
                    nameof(Suplidor.SitioWeb),
                    "El sitio web debe ser una URL válida que comience con http:// o https:// (ejemplo: https://www.empresa.com).");
            }
        }

        return resultado;
    }

    private static void ValidarTexto(
        ResultadoValidacion resultado,
        string campo,
        string descripcion,
        string? valor,
        int minimo,
        int maximo)
    {
        string texto = (valor ?? string.Empty).Trim();

        if (texto.Length == 0)
        {
            resultado.Agregar(campo, $"{descripcion} es obligatorio.");
        }
        else if (texto.Length < minimo || texto.Length > maximo)
        {
            resultado.Agregar(campo, $"{descripcion} debe tener entre {minimo} y {maximo} caracteres.");
        }
    }
}
