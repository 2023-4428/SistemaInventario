using System.Collections.Generic;
using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Interfaces;

/// <summary>
/// Patrón <b>Repository</b> genérico: operaciones CRUD con borrado lógico (soft-delete)
/// comunes a todas las entidades.
/// </summary>
/// <typeparam name="T">Tipo de entidad administrada por el repositorio.</typeparam>
public interface IRepositorio<T> where T : EntidadBase
{
    /// <summary>Obtiene los registros activos (no eliminados).</summary>
    /// <returns>Lista de solo lectura de entidades activas.</returns>
    IReadOnlyList<T> ObtenerActivos();

    /// <summary>Obtiene los registros eliminados lógicamente (contenido de la Papelera).</summary>
    /// <returns>Lista de solo lectura de entidades eliminadas.</returns>
    IReadOnlyList<T> ObtenerEliminados();

    /// <summary>Obtiene un registro por su identificador, esté activo o no.</summary>
    /// <param name="id">Identificador del registro.</param>
    /// <returns>La entidad, o <c>null</c> si no existe.</returns>
    T? ObtenerPorId(int id);

    /// <summary>Indica si existe un registro activo con el identificador dado.</summary>
    /// <param name="id">Identificador a verificar.</param>
    /// <returns><c>true</c> si existe y está activo.</returns>
    bool ExisteActivo(int id);

    /// <summary>Inserta un registro nuevo y asigna su identificador a la entidad.</summary>
    /// <param name="entidad">Entidad a insertar.</param>
    /// <returns>Identificador generado.</returns>
    int Insertar(T entidad);

    /// <summary>Actualiza los datos de un registro existente.</summary>
    /// <param name="entidad">Entidad con los valores nuevos.</param>
    void Actualizar(T entidad);

    /// <summary>Marca un registro como eliminado (<c>Activo = 0</c>) sin borrarlo físicamente.</summary>
    /// <param name="id">Identificador del registro.</param>
    void EliminarLogico(int id);

    /// <summary>Restaura un registro eliminado lógicamente (<c>Activo = 1</c>).</summary>
    /// <param name="id">Identificador del registro.</param>
    void Restaurar(int id);

    /// <summary>
    /// Borra físicamente un registro que ya está en la Papelera (<c>Activo = 0</c>), junto con sus datos dependientes
    /// (por ejemplo, las fotos de una categoría). Esta operación no se puede deshacer.
    /// </summary>
    /// <param name="id">Identificador del registro.</param>
    /// <exception cref="SistemaInventario.Comun.Excepciones.EntidadNoEncontradaException">
    /// Si no existe un registro eliminado lógicamente con ese identificador.
    /// </exception>
    void EliminarDefinitivo(int id);
}
