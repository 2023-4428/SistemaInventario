using System;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>
/// Reglas de negocio de suplidores: nombre de empresa único (incluso frente a la Papelera) y
/// prohibición de eliminar suplidores con productos activos.
/// </summary>
public sealed class SuplidorServicio : ServicioBase<Suplidor>, ISuplidorServicio
{
    private static readonly Func<Suplidor, string?>[] camposBusqueda =
    {
        suplidor => suplidor.NombreEmpresa,
        suplidor => suplidor.NombreContacto,
        suplidor => suplidor.Telefono,
        suplidor => suplidor.Correo,
        suplidor => suplidor.SitioWeb
    };

    private readonly ISuplidorRepositorio suplidores;
    private readonly IProductoRepositorio productos;

    /// <summary>Crea el servicio con sus dependencias (inyección por constructor).</summary>
    /// <param name="suplidores">Repositorio de suplidores.</param>
    /// <param name="productos">Repositorio de productos (para verificar productos asociados).</param>
    /// <param name="validador">Validador de suplidores.</param>
    public SuplidorServicio(
        ISuplidorRepositorio suplidores,
        IProductoRepositorio productos,
        IValidador<Suplidor> validador)
        : base(suplidores, validador)
    {
        this.suplidores = suplidores;
        this.productos = productos ?? throw new ArgumentNullException(nameof(productos));
    }

    /// <inheritdoc />
    protected override string NombreEntidad => "suplidor";

    /// <inheritdoc />
    protected override Func<Suplidor, string?>[] CamposBusqueda => camposBusqueda;

    /// <inheritdoc />
    protected override string CampoUnico => nameof(Suplidor.NombreEmpresa);

    /// <inheritdoc />
    protected override void Normalizar(Suplidor entidad)
    {
        entidad.NombreEmpresa = (entidad.NombreEmpresa ?? string.Empty).Trim();
        entidad.NombreContacto = (entidad.NombreContacto ?? string.Empty).Trim();
        entidad.Telefono = (entidad.Telefono ?? string.Empty).Trim();
        entidad.Correo = (entidad.Correo ?? string.Empty).Trim();

        string sitio = (entidad.SitioWeb ?? string.Empty).Trim();
        entidad.SitioWeb = sitio.Length == 0 ? null : sitio;
    }

    /// <inheritdoc />
    protected override void ValidarReglasDeNegocio(Suplidor entidad, ResultadoValidacion resultado)
    {
        if (resultado.TieneErroresEn(nameof(Suplidor.NombreEmpresa)))
        {
            return;
        }

        if (suplidores.ExisteNombreEmpresa(entidad.NombreEmpresa, entidad.Id))
        {
            resultado.Agregar(
                nameof(Suplidor.NombreEmpresa),
                "Ya existe un suplidor con ese nombre de empresa (revise también la Papelera).");
        }
    }

    /// <inheritdoc />
    protected override void AntesDeEliminar(Suplidor entidad)
    {
        int cantidad = productos.ContarActivosPorSuplidor(entidad.Id);

        if (cantidad > 0)
        {
            throw new ReglaNegocioException(
                $"No se puede eliminar el suplidor \"{entidad.NombreEmpresa}\" porque tiene {cantidad} producto(s) activo(s) asociado(s). " +
                "Elimine o reasigne esos productos primero.");
        }
    }

    /// <inheritdoc />
    protected override void AntesDeEliminarDefinitivo(Suplidor entidad)
    {
        int cantidad = productos.ContarPorSuplidor(entidad.Id);

        if (cantidad > 0)
        {
            throw new ReglaNegocioException(
                $"No se puede eliminar definitivamente el suplidor \"{entidad.NombreEmpresa}\" porque tiene {cantidad} producto(s) " +
                "asociado(s), activos o en la Papelera. Elimine definitivamente esos productos primero.");
        }
    }
}
