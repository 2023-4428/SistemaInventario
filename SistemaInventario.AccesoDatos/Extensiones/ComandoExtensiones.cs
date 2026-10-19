using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SistemaInventario.AccesoDatos.Extensiones;

/// <summary>Métodos de extensión para crear parámetros SQL tipados (evitan <c>AddWithValue</c>).</summary>
internal static class ComandoExtensiones
{
    /// <summary>Agrega un parámetro con tipo explícito. Un valor <c>null</c> se envía como <c>DBNull</c>.</summary>
    /// <param name="comando">Comando al que se agrega el parámetro.</param>
    /// <param name="nombre">Nombre del parámetro (incluye la arroba).</param>
    /// <param name="tipo">Tipo SQL del parámetro.</param>
    /// <param name="valor">Valor del parámetro.</param>
    /// <param name="tamano">Tamaño del parámetro; -1 para MAX; 0 si no aplica.</param>
    /// <returns>El parámetro agregado.</returns>
    public static SqlParameter AgregarParametro(
        this SqlCommand comando,
        string nombre,
        SqlDbType tipo,
        object? valor,
        int tamano = 0)
    {
        SqlParameter parametro = tamano != 0
            ? new SqlParameter(nombre, tipo, tamano)
            : new SqlParameter(nombre, tipo);

        parametro.Value = valor ?? DBNull.Value;
        comando.Parameters.Add(parametro);
        return parametro;
    }

    /// <summary>Agrega un parámetro <c>DECIMAL</c> con precisión y escala explícitas.</summary>
    /// <param name="comando">Comando al que se agrega el parámetro.</param>
    /// <param name="nombre">Nombre del parámetro (incluye la arroba).</param>
    /// <param name="valor">Valor decimal.</param>
    /// <param name="precision">Cantidad total de dígitos.</param>
    /// <param name="escala">Cantidad de decimales.</param>
    /// <returns>El parámetro agregado.</returns>
    public static SqlParameter AgregarParametroDecimal(
        this SqlCommand comando,
        string nombre,
        decimal valor,
        byte precision = 18,
        byte escala = 2)
    {
        SqlParameter parametro = new SqlParameter(nombre, SqlDbType.Decimal);
        parametro.Precision = precision;
        parametro.Scale = escala;
        parametro.Value = valor;
        comando.Parameters.Add(parametro);
        return parametro;
    }
}
