using System.Collections.Generic;
using SistemaInventario.Entidades;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>
/// Contrato genérico de los servicios de negocio: listados, altas, cambios, borrado lógico y restauración.
/// La capa de presentación solo conoce estas interfaces, nunca los repositorios.
/// </summary>
/// <typeparam name="T">Tipo de entidad que administra el servicio.</typeparam>
public interface IServicio<T> where T : EntidadBase
{
    /// <summary>Obtiene los registros activos.</summary>
    /// <returns>Lista de entidades activas.</returns>
    IReadOnlyList<T> ObtenerActivos();

    /// <summary>Obtiene los registros eliminados (Papelera).</summary>
    /// <returns>Lista de entidades eliminadas.</returns>
    IReadOnlyList<T> ObtenerEliminados();

    /// <summary>Obtiene un registro por su identificador.</summary>
    /// <param name="id">Identificador del registro.</param>
    /// <returns>La entidad encontrada.</returns>
    /// <exception cref="SistemaInventario.Comun.Excepciones.EntidadNoEncontradaException">Si no existe.</exception>
    T ObtenerPorId(int id);

    /// <summary>Valida y crea un registro nuevo.</summary>
    /// <param name="entidad">Entidad a crear.</param>
    /// <returns>Identificador del registro creado.</returns>
    /// <exception cref="SistemaInventario.Comun.Excepciones.ValidacionException">Si los datos no son válidos.</exception>
    int Crear(T entidad);

    /// <summary>Valida y actualiza un registro existente.</summary>
    /// <param name="entidad">Entidad con los valores nuevos.</param>
    /// <exception cref="SistemaInventario.Comun.Excepciones.ValidacionException">Si los datos no son válidos.</exception>
    /// <exception cref="SistemaInventario.Comun.Excepciones.ReglaNegocioException">Si el registro está eliminado.</exception>
    void Actualizar(T entidad);

    /// <summary>Elimina lógicamente un registro (lo envía a la Papelera).</summary>
    /// <param name="id">Identificador del registro.</param>
    /// <exception cref="SistemaInventario.Comun.Excepciones.ReglaNegocioException">Si incumple una regla de negocio.</exception>
    void Eliminar(int id);

    /// <summary>Restaura un registro que está en la Papelera.</summary>
    /// <param name="id">Identificador del registro.</param>
    /// <exception cref="SistemaInventario.Comun.Excepciones.ReglaNegocioException">Si incumple una regla de negocio.</exception>
    void Restaurar(int id);

    /// <summary>
    /// Borra físicamente un registro que está en la Papelera. No se puede deshacer.
    /// Solo se permite si el registro ya fue eliminado (lógicamente) y nada depende de él.
    /// </summary>
    /// <param name="id">Identificador del registro.</param>
    /// <exception cref="SistemaInventario.Comun.Excepciones.ReglaNegocioException">
    /// Si el registro no está en la Papelera o si otros registros dependen de él.
    /// </exception>
    void EliminarDefinitivo(int id);

    /// <summary>
    /// Filtra en memoria una lista ya cargada por coincidencia parcial (sin distinguir mayúsculas ni acentos).
    /// Se usa para el filtrado en tiempo real de los listados sin volver a consultar la base de datos.
    /// </summary>
    /// <param name="fuente">Registros a filtrar.</param>
    /// <param name="texto">Texto buscado; vacío devuelve todos.</param>
    /// <returns>Registros que coinciden.</returns>
    IReadOnlyList<T> Buscar(IEnumerable<T> fuente, string? texto);
}
