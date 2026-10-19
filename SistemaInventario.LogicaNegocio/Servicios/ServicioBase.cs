using System;
using System.Collections.Generic;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Utilidades;
using SistemaInventario.LogicaNegocio.Validacion;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>
/// Servicio genérico (patrón <i>Template Method</i>): implementa el flujo común de validar, guardar,
/// eliminar y restaurar. Los servicios concretos personalizan el comportamiento con los métodos virtuales.
/// </summary>
/// <typeparam name="T">Tipo de entidad administrada.</typeparam>
public abstract class ServicioBase<T> : IServicio<T> where T : EntidadBase
{
    private readonly IRepositorio<T> repositorio;
    private readonly IValidador<T> validador;

    /// <summary>Inicializa el servicio con sus dependencias (inyección por constructor).</summary>
    /// <param name="repositorio">Repositorio de la entidad.</param>
    /// <param name="validador">Validador de la entidad.</param>
    protected ServicioBase(IRepositorio<T> repositorio, IValidador<T> validador)
    {
        this.repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        this.validador = validador ?? throw new ArgumentNullException(nameof(validador));
    }

    /// <summary>Nombre legible de la entidad, en minúsculas, para los mensajes ("categoría").</summary>
    protected abstract string NombreEntidad { get; }

    /// <summary>Selectores de los textos sobre los que se busca en los listados.</summary>
    protected abstract Func<T, string?>[] CamposBusqueda { get; }

    /// <summary>Campo con restricción de unicidad (para traducir errores de duplicado); vacío si no hay.</summary>
    protected virtual string CampoUnico => string.Empty;

    /// <inheritdoc />
    public IReadOnlyList<T> ObtenerActivos() => repositorio.ObtenerActivos();

    /// <inheritdoc />
    public IReadOnlyList<T> ObtenerEliminados() => repositorio.ObtenerEliminados();

    /// <inheritdoc />
    public T ObtenerPorId(int id)
    {
        T? entidad = repositorio.ObtenerPorId(id);

        if (entidad == null)
        {
            throw new EntidadNoEncontradaException(NombreEntidad, id);
        }

        return entidad;
    }

    /// <inheritdoc />
    public int Crear(T entidad)
    {
        if (entidad == null)
        {
            throw new ArgumentNullException(nameof(entidad));
        }

        Normalizar(entidad);
        ValidarEntidad(entidad);

        return GuardarTraduciendoDuplicados(() => repositorio.Insertar(entidad));
    }

    /// <inheritdoc />
    public void Actualizar(T entidad)
    {
        if (entidad == null)
        {
            throw new ArgumentNullException(nameof(entidad));
        }

        T existente = ObtenerPorId(entidad.Id);

        if (!existente.Activo)
        {
            throw new ReglaNegocioException(
                $"No se puede modificar este registro de {NombreEntidad} porque está en la Papelera. Restáurelo primero.");
        }

        Normalizar(entidad);
        ValidarEntidad(entidad);

        GuardarTraduciendoDuplicados(() =>
        {
            repositorio.Actualizar(entidad);
            return entidad.Id;
        });
    }

    /// <inheritdoc />
    public void Eliminar(int id)
    {
        T entidad = ObtenerPorId(id);

        if (!entidad.Activo)
        {
            throw new ReglaNegocioException($"Este registro de {NombreEntidad} ya se encuentra en la Papelera.");
        }

        AntesDeEliminar(entidad);
        repositorio.EliminarLogico(id);
    }

    /// <inheritdoc />
    public void Restaurar(int id)
    {
        T entidad = ObtenerPorId(id);

        if (entidad.Activo)
        {
            throw new ReglaNegocioException($"Este registro de {NombreEntidad} ya está activo.");
        }

        AntesDeRestaurar(entidad);
        repositorio.Restaurar(id);
    }

    /// <inheritdoc />
    public void EliminarDefinitivo(int id)
    {
        T entidad = ObtenerPorId(id);

        if (entidad.Activo)
        {
            throw new ReglaNegocioException(
                $"Para eliminar definitivamente este registro de {NombreEntidad} primero envíelo a la Papelera.");
        }

        AntesDeEliminarDefinitivo(entidad);

        try
        {
            repositorio.EliminarDefinitivo(id);
        }
        catch (ErrorBaseDatosException ex) when (ex.EsViolacionClaveForanea)
        {
            throw new ReglaNegocioException(
                $"No se puede eliminar definitivamente este registro de {NombreEntidad} porque otros registros dependen de él.");
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<T> Buscar(IEnumerable<T> fuente, string? texto)
    {
        return fuente.Buscar(texto, CamposBusqueda);
    }

    /// <summary>Normaliza los datos antes de validar (por ejemplo, recorta espacios).</summary>
    /// <param name="entidad">Entidad a normalizar.</param>
    protected virtual void Normalizar(T entidad)
    {
    }

    /// <summary>Agrega al resultado los errores de reglas que requieren consultar la base de datos.</summary>
    /// <param name="entidad">Entidad que se está guardando.</param>
    /// <param name="resultado">Resultado de la validación de formato, al que se pueden agregar errores.</param>
    protected virtual void ValidarReglasDeNegocio(T entidad, ResultadoValidacion resultado)
    {
    }

    /// <summary>Verifica las reglas que se deben cumplir antes de eliminar; lanza <see cref="ReglaNegocioException"/> si no.</summary>
    /// <param name="entidad">Entidad que se va a eliminar.</param>
    protected virtual void AntesDeEliminar(T entidad)
    {
    }

    /// <summary>Verifica las reglas que se deben cumplir antes de restaurar; lanza <see cref="ReglaNegocioException"/> si no.</summary>
    /// <param name="entidad">Entidad que se va a restaurar.</param>
    protected virtual void AntesDeRestaurar(T entidad)
    {
    }

    /// <summary>
    /// Verifica las reglas que se deben cumplir antes de borrar definitivamente; lanza <see cref="ReglaNegocioException"/> si no.
    /// </summary>
    /// <param name="entidad">Entidad que se va a borrar definitivamente (ya está en la Papelera).</param>
    protected virtual void AntesDeEliminarDefinitivo(T entidad)
    {
    }

    private void ValidarEntidad(T entidad)
    {
        ResultadoValidacion resultado = validador.Validar(entidad);
        ValidarReglasDeNegocio(entidad, resultado);
        resultado.LanzarSiHayErrores();
    }

    private TResultado GuardarTraduciendoDuplicados<TResultado>(Func<TResultado> accion)
    {
        try
        {
            return accion();
        }
        catch (ErrorBaseDatosException ex) when (ex.EsViolacionUnica && CampoUnico.Length > 0)
        {
            throw new ValidacionException(
                CampoUnico,
                "Ya existe un registro con ese valor (revise también la Papelera).");
        }
    }
}
