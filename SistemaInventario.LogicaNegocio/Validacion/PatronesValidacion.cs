using System;
using System.Text.RegularExpressions;

namespace SistemaInventario.LogicaNegocio.Validacion;

/// <summary>Expresiones regulares y comprobaciones reutilizables por los validadores.</summary>
public static class PatronesValidacion
{
    /// <summary>Teléfono dominicano: 809, 829 o 849 seguido de XXX-XXXX.</summary>
    public static readonly Regex Telefono = new Regex(
        @"^(809|829|849)-\d{3}-\d{4}$",
        RegexOptions.ECMAScript | RegexOptions.Compiled);

    /// <summary>Correo electrónico con formato usuario@dominio.ext.</summary>
    public static readonly Regex Correo = new Regex(
        @"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9\-]+(\.[A-Za-z0-9\-]+)*\.[A-Za-z]{2,}$",
        RegexOptions.ECMAScript | RegexOptions.Compiled);

    /// <summary>Comprueba el formato de un teléfono dominicano.</summary>
    /// <param name="valor">Texto a comprobar.</param>
    /// <returns><c>true</c> si cumple el formato.</returns>
    public static bool EsTelefonoValido(string valor) => Telefono.IsMatch(valor);

    /// <summary>Comprueba el formato de un correo electrónico.</summary>
    /// <param name="valor">Texto a comprobar.</param>
    /// <returns><c>true</c> si cumple el formato.</returns>
    public static bool EsCorreoValido(string valor) => Correo.IsMatch(valor);

    /// <summary>
    /// Indica si el valor es una cédula (11 dígitos) o un RNC (9 dígitos) dominicano. Se aceptan guiones
    /// (000-0000000-0 y 0-00-00000-0) porque solo se comprueba la cantidad de dígitos.
    /// </summary>
    /// <param name="valor">Texto a verificar.</param>
    /// <returns><c>true</c> si tiene 9 u 11 dígitos y solo dígitos o guiones.</returns>
    public static bool EsCedulaORncValido(string valor)
    {
        int digitos = 0;

        foreach (char caracter in valor)
        {
            if (caracter >= '0' && caracter <= '9')
            {
                digitos++;
            }
            else if (caracter != '-')
            {
                return false;
            }
        }

        return digitos == 9 || digitos == 11;
    }

    /// <summary>Comprueba que el texto sea una URL absoluta http o https con host.</summary>
    /// <param name="valor">Texto a comprobar.</param>
    /// <returns><c>true</c> si es una URL válida.</returns>
    public static bool EsUrlValida(string valor)
    {
        if (!Uri.TryCreate(valor, UriKind.Absolute, out Uri? uri) || uri == null)
        {
            return false;
        }

        bool esHttp = uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
        return esHttp && uri.Host.Length > 0;
    }

    /// <summary>Comprueba que un decimal tenga como máximo dos cifras decimales.</summary>
    /// <param name="valor">Valor a comprobar.</param>
    /// <returns><c>true</c> si tiene 2 decimales o menos.</returns>
    public static bool TieneMaximoDosDecimales(decimal valor) => decimal.Round(valor, 2) == valor;
}
