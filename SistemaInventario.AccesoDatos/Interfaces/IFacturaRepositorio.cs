using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Interfaces;

/// <summary>
/// Repositorio de facturas. Persiste la factura y sus líneas como una unidad en una sola transacción.
/// Al consultar una factura por su identificador se incluyen sus líneas.
/// </summary>
public interface IFacturaRepositorio : IRepositorio<Factura>
{
}
