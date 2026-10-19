using System;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>
/// Reglas de negocio de categorías: nombre único (incluso frente a la Papelera) y
/// prohibición de eliminar categorías con productos activos.
/// </summary>
public sealed class CategoriaServicio : ServicioBase<Categoria>, ICategoriaServicio
{
    private static readonly Func<Categoria, string?>[] camposBusqueda =
    {
        categoria => categoria.Nombre,
        categoria => categoria.Descripcion
    };

    private readonly ICategoriaRepositorio categorias;
    private readonly IProductoRepositorio productos;
    private readonly IValidador<FotoCategoria> validadorFoto;

    /// <summary>Crea el servicio con sus dependencias (inyección por constructor).</summary>
    /// <param name="categorias">Repositorio de categorías.</param>
    /// <param name="productos">Repositorio de productos (para verificar productos asociados).</param>
    /// <param name="validador">Validador de categorías.</param>
    /// <param name="validadorFoto">Validador de fotos.</param>
    public CategoriaServicio(
        ICategoriaRepositorio categorias,
        IProductoRepositorio productos,
        IValidador<Categoria> validador,
        IValidador<FotoCategoria> validadorFoto)
        : base(categorias, validador)
    {
        this.categorias = categorias;
        this.productos = productos ?? throw new ArgumentNullException(nameof(productos));
        this.validadorFoto = validadorFoto ?? throw new ArgumentNullException(nameof(validadorFoto));
    }

    /// <inheritdoc />
    protected override string NombreEntidad => "categoría";

    /// <inheritdoc />
    protected override Func<Categoria, string?>[] CamposBusqueda => camposBusqueda;

    /// <inheritdoc />
    protected override string CampoUnico => nameof(Categoria.Nombre);

    /// <inheritdoc />
    public ResultadoValidacion ValidarFoto(FotoCategoria foto)
    {
        if (foto == null)
        {
            throw new ArgumentNullException(nameof(foto));
        }

        return validadorFoto.Validar(foto);
    }

    /// <inheritdoc />
    protected override void Normalizar(Categoria entidad)
    {
        entidad.Nombre = (entidad.Nombre ?? string.Empty).Trim();
        entidad.Descripcion = (entidad.Descripcion ?? string.Empty).Trim();
    }

    /// <inheritdoc />
    protected override void ValidarReglasDeNegocio(Categoria entidad, ResultadoValidacion resultado)
    {
        if (resultado.TieneErroresEn(nameof(Categoria.Nombre)))
        {
            return;
        }

        if (categorias.ExisteNombre(entidad.Nombre, entidad.Id))
        {
            resultado.Agregar(
                nameof(Categoria.Nombre),
                "Ya existe una categoría con ese nombre (revise también la Papelera).");
        }
    }

    /// <inheritdoc />
    protected override void AntesDeEliminar(Categoria entidad)
    {
        int cantidad = productos.ContarActivosPorCategoria(entidad.Id);

        if (cantidad > 0)
        {
            throw new ReglaNegocioException(
                $"No se puede eliminar la categoría \"{entidad.Nombre}\" porque tiene {cantidad} producto(s) activo(s) asociado(s). " +
                "Elimine o reasigne esos productos primero.");
        }
    }

    /// <inheritdoc />
    protected override void AntesDeEliminarDefinitivo(Categoria entidad)
    {
        int cantidad = productos.ContarPorCategoria(entidad.Id);

        if (cantidad > 0)
        {
            throw new ReglaNegocioException(
                $"No se puede eliminar definitivamente la categoría \"{entidad.Nombre}\" porque tiene {cantidad} producto(s) " +
                "asociado(s), activos o en la Papelera. Elimine definitivamente esos productos primero.");
        }
    }
}
