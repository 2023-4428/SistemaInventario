using System;

namespace SistemaInventario.Comun.Excepciones;

/// <summary>
/// Envuelve los errores de SQL Server (<c>SqlException</c>) con un mensaje amigable.
/// Así las capas superiores no dependen del proveedor de datos.
/// </summary>
public sealed class ErrorBaseDatosException : Exception
{
    /// <summary>Número de error de SQL Server para violación de restricción UNIQUE.</summary>
    public const int ViolacionUnica = 2627;

    /// <summary>Número de error de SQL Server para índice único duplicado.</summary>
    public const int IndiceUnicoDuplicado = 2601;

    /// <summary>Número de error de SQL Server para violación de clave foránea.</summary>
    public const int ViolacionClaveForanea = 547;

    /// <summary>Crea la excepción.</summary>
    /// <param name="mensaje">Mensaje amigable para el usuario.</param>
    /// <param name="excepcionInterna">La excepción original del proveedor de datos.</param>
    /// <param name="codigoSql">Número de error de SQL Server, si se conoce.</param>
    public ErrorBaseDatosException(string mensaje, Exception? excepcionInterna, int? codigoSql = null)
        : base(mensaje, excepcionInterna)
    {
        CodigoSql = codigoSql;
    }

    /// <summary>Número de error de SQL Server (<c>SqlException.Number</c>), si existe.</summary>
    public int? CodigoSql { get; }

    /// <summary>Indica si el error corresponde a un valor duplicado en una restricción única.</summary>
    public bool EsViolacionUnica => CodigoSql == ViolacionUnica || CodigoSql == IndiceUnicoDuplicado;

    /// <summary>Indica si el error corresponde a una violación de integridad referencial.</summary>
    public bool EsViolacionClaveForanea => CodigoSql == ViolacionClaveForanea;
}
