using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun.Excepciones;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>
/// Reglas de negocio de productos: la categoría y el suplidor deben existir y estar activos, tanto al
/// guardar como al restaurar un producto desde la Papelera.
/// </summary>
public sealed class ProductoServicio : ServicioBase<Producto>, IProductoServicio
{
    private static readonly Func<Producto, string?>[] camposBusqueda =
    {
        producto => producto.Nombre,
        producto => producto.CategoriaNombre,
        producto => producto.SuplidorNombre,
        producto => producto.PrecioUnitario.ToString("0.00", CultureInfo.InvariantCulture)
    };

    private readonly IProductoRepositorio productos;
    private readonly ICategoriaRepositorio categorias;
    private readonly ISuplidorRepositorio suplidores;

    /// <summary>Crea el servicio con sus dependencias (inyección por constructor).</summary>
    /// <param name="productos">Repositorio de productos.</param>
    /// <param name="categorias">Repositorio de categorías (para verificar que la categoría esté activa).</param>
    /// <param name="suplidores">Repositorio de suplidores (para verificar que el suplidor esté activo).</param>
    /// <param name="validador">Validador de productos.</param>
    public ProductoServicio(
        IProductoRepositorio productos,
        ICategoriaRepositorio categorias,
        ISuplidorRepositorio suplidores,
        IValidador<Producto> validador)
        : base(productos, validador)
    {
        this.productos = productos;
        this.categorias = categorias ?? throw new ArgumentNullException(nameof(categorias));
        this.suplidores = suplidores ?? throw new ArgumentNullException(nameof(suplidores));
    }

    /// <inheritdoc />
    protected override string NombreEntidad => "producto";

    /// <inheritdoc />
    protected override Func<Producto, string?>[] CamposBusqueda => camposBusqueda;

    /// <inheritdoc />
    protected override string CampoUnico => nameof(Producto.Nombre);

    /// <summary>
    /// Consulta LINQ: aplica de forma acumulativa los filtros de categoría, suplidor y rango de precios,
    /// y luego la búsqueda parcial por texto.
    /// </summary>
    /// <param name="fuente">Productos a filtrar.</param>
    /// <param name="filtro">Criterios a aplicar.</param>
    /// <returns>Productos que cumplen todos los criterios.</returns>
    public IReadOnlyList<Producto> Filtrar(IEnumerable<Producto> fuente, FiltroProducto filtro)
    {
        if (fuente == null)
        {
            throw new ArgumentNullException(nameof(fuente));
        }

        if (filtro == null)
        {
            throw new ArgumentNullException(nameof(filtro));
        }

        IEnumerable<Producto> consulta = fuente;

        if (filtro.CategoriaId.HasValue)
        {
            consulta = consulta.Where(producto => producto.CategoriaId == filtro.CategoriaId.Value);
        }

        if (filtro.SuplidorId.HasValue)
        {
            consulta = consulta.Where(producto => producto.SuplidorId == filtro.SuplidorId.Value);
        }

        if (filtro.PrecioMinimo.HasValue)
        {
            consulta = consulta.Where(producto => producto.PrecioUnitario >= filtro.PrecioMinimo.Value);
        }

        if (filtro.PrecioMaximo.HasValue)
        {
            consulta = consulta.Where(producto => producto.PrecioUnitario <= filtro.PrecioMaximo.Value);
        }

        return Buscar(consulta, filtro.Texto);
    }

    /// <inheritdoc />
    protected override void Normalizar(Producto entidad)
    {
        entidad.Nombre = (entidad.Nombre ?? string.Empty).Trim();
    }

    /// <inheritdoc />
    protected override void ValidarReglasDeNegocio(Producto entidad, ResultadoValidacion resultado)
    {
        if (!resultado.TieneErroresEn(nameof(Producto.CategoriaId)) && !categorias.ExisteActivo(entidad.CategoriaId))
        {
            resultado.Agregar(nameof(Producto.CategoriaId), "La categoría seleccionada no existe o está eliminada.");
        }

        if (!resultado.TieneErroresEn(nameof(Producto.SuplidorId)) && !suplidores.ExisteActivo(entidad.SuplidorId))
        {
            resultado.Agregar(nameof(Producto.SuplidorId), "El suplidor seleccionado no existe o está eliminado.");
        }

        bool datosListosParaComparar =
            !resultado.TieneErroresEn(nameof(Producto.Nombre)) &&
            !resultado.TieneErroresEn(nameof(Producto.CategoriaId)) &&
            !resultado.TieneErroresEn(nameof(Producto.SuplidorId));

        if (datosListosParaComparar &&
            productos.ExisteDuplicado(entidad.Nombre, entidad.CategoriaId, entidad.SuplidorId, entidad.Id))
        {
            resultado.Agregar(
                nameof(Producto.Nombre),
                "Ya existe un producto con ese nombre, categoría y suplidor (revise también la Papelera).");
        }
    }

    /// <inheritdoc />
    protected override void AntesDeEliminarDefinitivo(Producto entidad)
    {
        int lineas = productos.ContarEnFacturas(entidad.Id);

        if (lineas > 0)
        {
            throw new ReglaNegocioException(
                $"No se puede eliminar definitivamente el producto \"{entidad.Nombre}\" porque aparece en {lineas} línea(s) de factura. " +
                "Los productos facturados se conservan para no alterar el historial de ventas.");
        }
    }

    /// <inheritdoc />
    protected override void AntesDeRestaurar(Producto entidad)
    {
        if (!categorias.ExisteActivo(entidad.CategoriaId))
        {
            throw new ReglaNegocioException(
                $"No se puede restaurar el producto \"{entidad.Nombre}\" porque su categoría \"{entidad.CategoriaNombre}\" " +
                "está eliminada. Restaure primero la categoría desde la Papelera.");
        }

        if (!suplidores.ExisteActivo(entidad.SuplidorId))
        {
            throw new ReglaNegocioException(
                $"No se puede restaurar el producto \"{entidad.Nombre}\" porque su suplidor \"{entidad.SuplidorNombre}\" " +
                "está eliminado. Restaure primero el suplidor desde la Papelera.");
        }
    }
}
