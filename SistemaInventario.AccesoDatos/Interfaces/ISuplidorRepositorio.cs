using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Interfaces;

/// <summary>Repositorio de suplidores.</summary>
public interface ISuplidorRepositorio : IRepositorio<Suplidor>
{
    /// <summary>Indica si existe otro suplidor (activo o eliminado) con el mismo nombre de empresa.</summary>
    /// <param name="nombreEmpresa">Nombre de empresa a buscar.</param>
    /// <param name="excluirId">Identificador a excluir de la búsqueda (0 al crear).</param>
    /// <returns><c>true</c> si el nombre ya está en uso.</returns>
    bool ExisteNombreEmpresa(string nombreEmpresa, int excluirId);
}
