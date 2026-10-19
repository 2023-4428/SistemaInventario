using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SistemaInventario.LogicaNegocio.Utilidades;

/// <summary>
/// Búsqueda parcial genérica, reutilizada por todos los listados (método genérico propio).
/// Ignora mayúsculas/minúsculas y acentos, y admite varias palabras (todas deben coincidir).
/// </summary>
public static class ExtensionesBusqueda
{
    /// <summary>Convierte un texto a minúsculas y sin acentos para poder compararlo.</summary>
    /// <param name="texto">Texto original (puede ser <c>null</c>).</param>
    /// <returns>Texto normalizado; cadena vacía si no hay contenido.</returns>
    public static string Normalizar(string? texto)
    {
        if (texto == null || texto.Trim().Length == 0)
        {
            return string.Empty;
        }

        string descompuesto = texto.Trim().Normalize(NormalizationForm.FormD);
        StringBuilder resultado = new StringBuilder(descompuesto.Length);

        foreach (char caracter in descompuesto)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
            {
                resultado.Append(caracter);
            }
        }

        return resultado.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();
    }

    /// <summary>
    /// Filtra una secuencia por coincidencia parcial del texto en cualquiera de los campos indicados.
    /// </summary>
    /// <typeparam name="T">Tipo de los elementos.</typeparam>
    /// <param name="fuente">Elementos a filtrar.</param>
    /// <param name="texto">Texto buscado; si está vacío se devuelven todos los elementos.</param>
    /// <param name="campos">Selectores que devuelven los textos donde se busca.</param>
    /// <returns>Lista con los elementos que coinciden.</returns>
    public static IReadOnlyList<T> Buscar<T>(
        this IEnumerable<T> fuente,
        string? texto,
        params Func<T, string?>[] campos)
    {
        if (fuente == null)
        {
            throw new ArgumentNullException(nameof(fuente));
        }

        if (campos == null)
        {
            throw new ArgumentNullException(nameof(campos));
        }

        string criterio = Normalizar(texto);
        if (criterio.Length == 0)
        {
            return fuente.ToList();
        }

        string[] terminos = criterio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        return fuente
            .Where(elemento =>
            {
                string contenido = string.Join(" ", campos.Select(campo => Normalizar(campo(elemento))));
                return terminos.All(termino => contenido.IndexOf(termino, StringComparison.Ordinal) >= 0);
            })
            .ToList();
    }
}
