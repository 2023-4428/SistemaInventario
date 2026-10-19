using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Interfaces;

/// <summary>Repositorio de categorías (incluye la persistencia de sus fotos).</summary>
public interface ICategoriaRepositorio : IRepositorio<Categoria>
{
    /// <summary>Indica si existe otra categoría (activa o eliminada) con el mismo nombre.</summary>
    /// <param name="nombre">Nombre a buscar.</param>
    /// <param name="excluirId">Identificador a excluir de la búsqueda (0 al crear).</param>
    /// <returns><c>true</c> si el nombre ya está en uso.</returns>
    bool ExisteNombre(string nombre, int excluirId);
}
