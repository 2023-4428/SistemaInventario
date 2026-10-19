using System;

namespace SistemaInventario.Entidades;

/// <summary>
/// Clase base de todas las entidades de dominio. Concentra los campos comunes:
/// identificador, estado de borrado lógico (soft-delete) y fechas de auditoría.
/// </summary>
public abstract class EntidadBase
{
    /// <summary>Identificador único. Vale 0 mientras la entidad no se ha guardado.</summary>
    public int Id { get; set; }

    /// <summary>Indica si el registro está activo (<c>true</c>) o eliminado lógicamente (<c>false</c>).</summary>
    public bool Activo { get; set; } = true;

    /// <summary>Fecha y hora en que se creó el registro.</summary>
    public DateTime FechaCreacion { get; set; }

    /// <summary>Fecha y hora de la última modificación (también se actualiza al eliminar o restaurar).</summary>
    public DateTime FechaModificacion { get; set; }

    /// <summary>Indica si la entidad todavía no existe en la base de datos.</summary>
    public bool EsNueva => Id == 0;
}
