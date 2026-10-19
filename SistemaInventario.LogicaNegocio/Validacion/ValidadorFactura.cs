using System.Collections.Generic;
using System.Globalization;
using SistemaInventario.Entidades;

namespace SistemaInventario.LogicaNegocio.Validacion;

/// <summary>
/// Valida el formato de una <see cref="Factura"/>: cliente, documento (cédula o RNC), al menos una línea,
/// cantidades y precios correctos y que no se repita un producto en varias líneas.
/// </summary>
public sealed class ValidadorFactura : IValidador<Factura>
{
    /// <inheritdoc />
    public ResultadoValidacion Validar(Factura entidad)
    {
        ResultadoValidacion resultado = new ResultadoValidacion();

        string cliente = (entidad.ClienteNombre ?? string.Empty).Trim();
        if (cliente.Length == 0)
        {
            resultado.Agregar(nameof(Factura.ClienteNombre), "El nombre del cliente es obligatorio.");
        }
        else if (cliente.Length < Factura.LongitudMinimaCliente || cliente.Length > Factura.LongitudMaximaCliente)
        {
            resultado.Agregar(
                nameof(Factura.ClienteNombre),
                $"El nombre del cliente debe tener entre {Factura.LongitudMinimaCliente} y {Factura.LongitudMaximaCliente} caracteres.");
        }

        string documento = (entidad.ClienteDocumento ?? string.Empty).Trim();
        if (documento.Length > 0 &&
            (documento.Length > Factura.LongitudMaximaDocumento || !PatronesValidacion.EsCedulaORncValido(documento)))
        {
            resultado.Agregar(
                nameof(Factura.ClienteDocumento),
                "La cédula debe tener 11 dígitos o el RNC 9 dígitos (se aceptan guiones).");
        }

        ValidarLineas(entidad, resultado);
        return resultado;
    }

    private static void ValidarLineas(Factura entidad, ResultadoValidacion resultado)
    {
        if (entidad.Detalles.Count == 0)
        {
            resultado.Agregar(nameof(Factura.Detalles), "Agregue al menos un producto a la factura.");
            return;
        }

        if (entidad.Detalles.Count > Factura.MaximoLineas)
        {
            resultado.Agregar(
                nameof(Factura.Detalles),
                $"Una factura admite como máximo {Factura.MaximoLineas} líneas.");
        }

        HashSet<int> productosVistos = new HashSet<int>();
        int numero = 0;

        foreach (FacturaDetalle detalle in entidad.Detalles)
        {
            numero++;

            if (detalle.ProductoId <= 0)
            {
                resultado.Agregar(nameof(Factura.Detalles), $"Línea {numero}: seleccione un producto.");
            }
            else if (!productosVistos.Add(detalle.ProductoId))
            {
                resultado.Agregar(
                    nameof(Factura.Detalles),
                    $"Línea {numero}: el producto \"{detalle.ProductoNombre}\" está repetido. Sume las cantidades en una sola línea.");
            }

            if (detalle.Cantidad <= 0 || detalle.Cantidad > FacturaDetalle.CantidadMaxima)
            {
                resultado.Agregar(
                    nameof(Factura.Detalles),
                    $"Línea {numero}: la cantidad debe estar entre 1 y {FacturaDetalle.CantidadMaxima.ToString("N0", CultureInfo.CurrentCulture)}.");
            }

            if (detalle.PrecioUnitario <= 0 || !PatronesValidacion.TieneMaximoDosDecimales(detalle.PrecioUnitario))
            {
                resultado.Agregar(
                    nameof(Factura.Detalles),
                    $"Línea {numero}: el precio debe ser mayor que 0 y tener máximo 2 decimales.");
            }
        }
    }
}
