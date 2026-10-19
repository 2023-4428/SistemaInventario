using System;
using System.Collections.Generic;
using System.Globalization;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>
/// Reglas de negocio de facturas: los productos deben existir y estar activos, el precio de cada línea se toma
/// del producto en el momento de facturar y los totales (con ITBIS del 18 %) se recalculan siempre en el servidor.
/// </summary>
public sealed class FacturaServicio : ServicioBase<Factura>, IFacturaServicio
{
    private static readonly Func<Factura, string?>[] camposBusqueda =
    {
        factura => factura.Numero,
        factura => factura.ClienteNombre,
        factura => factura.ClienteDocumento,
        factura => factura.Total.ToString("0.00", CultureInfo.InvariantCulture)
    };

    private readonly IProductoRepositorio productos;

    /// <summary>Crea el servicio con sus dependencias (inyección por constructor).</summary>
    /// <param name="facturas">Repositorio de facturas.</param>
    /// <param name="productos">Repositorio de productos (para verificar y obtener el precio vigente).</param>
    /// <param name="validador">Validador de facturas.</param>
    public FacturaServicio(
        IFacturaRepositorio facturas,
        IProductoRepositorio productos,
        IValidador<Factura> validador)
        : base(facturas, validador)
    {
        this.productos = productos ?? throw new ArgumentNullException(nameof(productos));
    }

    /// <inheritdoc />
    protected override string NombreEntidad => "factura";

    /// <inheritdoc />
    protected override Func<Factura, string?>[] CamposBusqueda => camposBusqueda;

    /// <inheritdoc />
    protected override void Normalizar(Factura entidad)
    {
        entidad.ClienteNombre = (entidad.ClienteNombre ?? string.Empty).Trim();

        string documento = (entidad.ClienteDocumento ?? string.Empty).Trim();
        entidad.ClienteDocumento = documento.Length == 0 ? null : documento;

        if (!entidad.EsNueva)
        {
            return;
        }

        // Factura nueva: el precio y el nombre salen del producto vigente, no de lo que envíe la pantalla.
        foreach (FacturaDetalle detalle in entidad.Detalles)
        {
            Producto? producto = productos.ObtenerPorId(detalle.ProductoId);

            if (producto != null && producto.Activo)
            {
                detalle.PrecioUnitario = producto.PrecioUnitario;
                detalle.ProductoNombre = producto.Nombre;
            }
        }

        entidad.RecalcularTotales();
    }

    /// <inheritdoc />
    protected override void ValidarReglasDeNegocio(Factura entidad, ResultadoValidacion resultado)
    {
        if (!entidad.EsNueva)
        {
            return;
        }

        HashSet<int> revisados = new HashSet<int>();

        foreach (FacturaDetalle detalle in entidad.Detalles)
        {
            if (detalle.ProductoId <= 0 || !revisados.Add(detalle.ProductoId))
            {
                continue;
            }

            if (!productos.ExisteActivo(detalle.ProductoId))
            {
                string nombre = detalle.ProductoNombre.Length == 0 ? "seleccionado" : "\"" + detalle.ProductoNombre + "\"";
                resultado.Agregar(
                    nameof(Factura.Detalles),
                    $"El producto {nombre} ya no existe o está eliminado. Quítelo de la factura.");
            }
        }
    }
}
