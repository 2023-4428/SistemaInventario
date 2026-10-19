using System;

namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>Lista de una pestaña de la Papelera: informa cuántos registros tiene y cuándo se restauró o borró alguno.</summary>
internal interface IListaPapelera : IRecargable
{
    /// <summary>Cantidad de registros eliminados que muestra la lista.</summary>
    int Cantidad { get; }

    /// <summary>Se produce cuando cambia la cantidad de registros eliminados.</summary>
    event EventHandler? CantidadCambio;

    /// <summary>Se produce después de restaurar o borrar definitivamente un registro.</summary>
    event EventHandler? RegistroModificado;
}
