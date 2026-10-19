namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>Elemento de un ComboBox de filtro; un <see cref="Id"/> nulo representa la opción "Todas".</summary>
internal sealed class ItemCombo
{
    /// <summary>Crea el elemento.</summary>
    /// <param name="id">Identificador del registro, o <c>null</c> para "Todos".</param>
    /// <param name="texto">Texto visible.</param>
    public ItemCombo(int? id, string texto)
    {
        Id = id;
        Texto = texto;
    }

    /// <summary>Identificador del registro (<c>null</c> = todos).</summary>
    public int? Id { get; }

    /// <summary>Texto que se muestra en la lista.</summary>
    public string Texto { get; }

    /// <inheritdoc />
    public override string ToString() => Texto;
}
