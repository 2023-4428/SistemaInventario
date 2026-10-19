using SistemaInventario.Entidades;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>
/// Servicio de negocio de facturas. Eliminar una factura la anula (va a la Papelera); las líneas y los totales
/// se validan y se recalculan en el servicio, nunca se confía en los valores que envía la pantalla.
/// </summary>
public interface IFacturaServicio : IServicio<Factura>
{
}
