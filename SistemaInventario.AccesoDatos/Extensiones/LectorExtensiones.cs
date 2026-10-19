using System;
using Microsoft.Data.SqlClient;

namespace SistemaInventario.AccesoDatos.Extensiones;

/// <summary>Métodos de extensión para leer columnas por nombre de un <see cref="SqlDataReader"/>.</summary>
internal static class LectorExtensiones
{
    /// <summary>Lee una columna entera.</summary>
    public static int LeerEntero(this SqlDataReader lector, string columna)
    {
        return lector.GetInt32(lector.GetOrdinal(columna));
    }

    /// <summary>Lee una columna de texto; un <c>NULL</c> se devuelve como cadena vacía.</summary>
    public static string LeerTexto(this SqlDataReader lector, string columna)
    {
        int indice = lector.GetOrdinal(columna);
        return lector.IsDBNull(indice) ? string.Empty : lector.GetString(indice);
    }

    /// <summary>Lee una columna de texto que admite <c>NULL</c>.</summary>
    public static string? LeerTextoNulo(this SqlDataReader lector, string columna)
    {
        int indice = lector.GetOrdinal(columna);
        return lector.IsDBNull(indice) ? null : lector.GetString(indice);
    }

    /// <summary>Lee una columna decimal.</summary>
    public static decimal LeerDecimal(this SqlDataReader lector, string columna)
    {
        return lector.GetDecimal(lector.GetOrdinal(columna));
    }

    /// <summary>Lee una columna booleana (<c>BIT</c>).</summary>
    public static bool LeerBooleano(this SqlDataReader lector, string columna)
    {
        return lector.GetBoolean(lector.GetOrdinal(columna));
    }

    /// <summary>Lee una columna de fecha y hora.</summary>
    public static DateTime LeerFecha(this SqlDataReader lector, string columna)
    {
        return lector.GetDateTime(lector.GetOrdinal(columna));
    }

    /// <summary>Lee una columna binaria; un <c>NULL</c> se devuelve como arreglo vacío.</summary>
    public static byte[] LeerBytes(this SqlDataReader lector, string columna)
    {
        int indice = lector.GetOrdinal(columna);
        return lector.IsDBNull(indice) ? Array.Empty<byte>() : lector.GetFieldValue<byte[]>(indice);
    }
}
