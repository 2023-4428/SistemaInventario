using System;
using System.Collections.Generic;
using System.Linq;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Entidades;

namespace SistemaInventario.Pruebas.Falsos;

/// <summary>Repositorio genérico en memoria para probar los servicios sin base de datos.</summary>
/// <typeparam name="T">Tipo de entidad.</typeparam>
public class RepositorioFalso<T> : IRepositorio<T> where T : EntidadBase
{
    private int siguienteId = 1;

    /// <summary>Datos almacenados (activos y eliminados).</summary>
    protected List<T> Datos { get; } = new List<T>();

    /// <summary>Excepción que se lanzará al insertar (para simular errores de base de datos).</summary>
    public Exception? ErrorAlInsertar { get; set; }

    /// <summary>Agrega directamente una entidad al almacén (preparación de escenarios).</summary>
    public T Agregar(T entidad, bool activo = true)
    {
        entidad.Id = siguienteId++;
        entidad.Activo = activo;
        entidad.FechaCreacion = DateTime.Now;
        entidad.FechaModificacion = DateTime.Now;
        Datos.Add(entidad);
        return entidad;
    }

    public IReadOnlyList<T> ObtenerActivos() => Datos.Where(e => e.Activo).ToList();

    public IReadOnlyList<T> ObtenerEliminados() => Datos.Where(e => !e.Activo).ToList();

    public virtual T? ObtenerPorId(int id) => Datos.FirstOrDefault(e => e.Id == id);

    public bool ExisteActivo(int id) => Datos.Any(e => e.Id == id && e.Activo);

    public int Insertar(T entidad)
    {
        if (ErrorAlInsertar != null)
        {
            throw ErrorAlInsertar;
        }

        entidad.Id = siguienteId++;
        entidad.Activo = true;
        entidad.FechaCreacion = DateTime.Now;
        entidad.FechaModificacion = DateTime.Now;
        Datos.Add(entidad);
        return entidad.Id;
    }

    public void Actualizar(T entidad)
    {
        int indice = Datos.FindIndex(e => e.Id == entidad.Id);
        if (indice < 0)
        {
            throw new InvalidOperationException("Registro inexistente en el repositorio falso.");
        }

        entidad.Activo = Datos[indice].Activo;
        Datos[indice] = entidad;
    }

    public void EliminarLogico(int id) => CambiarEstado(id, false);

    public void Restaurar(int id) => CambiarEstado(id, true);

    public void EliminarDefinitivo(int id)
    {
        int indice = Datos.FindIndex(e => e.Id == id && !e.Activo);
        if (indice < 0)
        {
            throw new InvalidOperationException("Solo se pueden borrar definitivamente registros de la Papelera.");
        }

        Datos.RemoveAt(indice);
    }

    private void CambiarEstado(int id, bool activo)
    {
        T entidad = Datos.First(e => e.Id == id);
        entidad.Activo = activo;
        entidad.FechaModificacion = DateTime.Now;
    }
}
