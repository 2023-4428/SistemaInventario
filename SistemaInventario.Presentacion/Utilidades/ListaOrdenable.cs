using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>
/// <see cref="BindingList{T}"/> que soporta ordenamiento, para que el usuario pueda ordenar
/// las columnas del <c>DataGridView</c> haciendo clic en su encabezado (clase genérica propia).
/// </summary>
/// <typeparam name="T">Tipo de los elementos.</typeparam>
internal sealed class ListaOrdenable<T> : BindingList<T>
{
    private bool estaOrdenada;
    private PropertyDescriptor? propiedadOrden;
    private ListSortDirection direccionOrden = ListSortDirection.Ascending;

    /// <summary>Crea la lista con una copia de los elementos indicados.</summary>
    /// <param name="elementos">Elementos iniciales.</param>
    public ListaOrdenable(IEnumerable<T> elementos)
        : base(new List<T>(elementos))
    {
    }

    /// <inheritdoc />
    protected override bool SupportsSortingCore => true;

    /// <inheritdoc />
    protected override bool IsSortedCore => estaOrdenada;

    /// <inheritdoc />
    protected override PropertyDescriptor? SortPropertyCore => propiedadOrden;

    /// <inheritdoc />
    protected override ListSortDirection SortDirectionCore => direccionOrden;

    /// <inheritdoc />
    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        List<T>? lista = Items as List<T>;
        if (lista == null)
        {
            return;
        }

        ComparadorGenerico comparador = new ComparadorGenerico();
        List<T> ordenados = direction == ListSortDirection.Ascending
            ? lista.OrderBy(elemento => prop.GetValue(elemento), comparador).ToList()
            : lista.OrderByDescending(elemento => prop.GetValue(elemento), comparador).ToList();

        lista.Clear();
        lista.AddRange(ordenados);

        propiedadOrden = prop;
        direccionOrden = direction;
        estaOrdenada = true;

        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    /// <inheritdoc />
    protected override void RemoveSortCore()
    {
        estaOrdenada = false;
        propiedadOrden = null;
    }

    private sealed class ComparadorGenerico : IComparer<object?>
    {
        public int Compare(object? x, object? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            string? textoX = x as string;
            string? textoY = y as string;
            if (textoX != null && textoY != null)
            {
                return string.Compare(textoX, textoY, StringComparison.CurrentCultureIgnoreCase);
            }

            IComparable? comparable = x as IComparable;
            if (comparable != null && x.GetType() == y.GetType())
            {
                return comparable.CompareTo(y);
            }

            return string.Compare(x.ToString(), y.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
