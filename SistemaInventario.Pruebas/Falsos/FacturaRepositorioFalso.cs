using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Entidades;

namespace SistemaInventario.Pruebas.Falsos;

/// <summary>Repositorio de facturas en memoria.</summary>
public sealed class FacturaRepositorioFalso : RepositorioFalso<Factura>, IFacturaRepositorio
{
}
