using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaInventario.Entidades;

/// <summary>
/// Factura de venta con sus líneas. Eliminar una factura la anula lógicamente (va a la Papelera).
/// Los totales (subtotal, ITBIS y total) siempre se calculan a partir de las líneas.
/// </summary>
public class Factura : EntidadBase
{
    /// <summary>Tasa del ITBIS (18 %).</summary>
    public const decimal TasaItbis = 0.18m;

    /// <summary>Longitud mínima del nombre del cliente.</summary>
    public const int LongitudMinimaCliente = 2;

    /// <summary>Longitud máxima del nombre del cliente.</summary>
    public const int LongitudMaximaCliente = 150;

    /// <summary>Longitud máxima del documento del cliente (cédula o RNC, con o sin guiones).</summary>
    public const int LongitudMaximaDocumento = 15;

    /// <summary>Cantidad máxima de líneas por factura.</summary>
    public const int MaximoLineas = 50;

    private readonly List<FacturaDetalle> detalles = new List<FacturaDetalle>();

    /// <summary>Número de la factura (por ejemplo, F-00000012). Lo genera la base de datos.</summary>
    public string Numero { get; set; } = string.Empty;

    /// <summary>Nombre del cliente (obligatorio).</summary>
    public string ClienteNombre { get; set; } = string.Empty;

    /// <summary>Cédula (11 dígitos) o RNC (9 dígitos) del cliente. Es opcional.</summary>
    public string? ClienteDocumento { get; set; }

    /// <summary>Suma de los importes de las líneas, sin ITBIS.</summary>
    public decimal Subtotal { get; set; }

    /// <summary>ITBIS calculado sobre el subtotal.</summary>
    public decimal Itbis { get; set; }

    /// <summary>Total a pagar (subtotal más ITBIS).</summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Cantidad de líneas. Es un dato de resumen que se llena al listar facturas, para no cargar las líneas en los listados.
    /// </summary>
    public int CantidadLineas { get; set; }

    /// <summary>Líneas de la factura (solo se cargan al consultar una factura por su identificador).</summary>
    public IReadOnlyList<FacturaDetalle> Detalles => detalles;

    /// <summary>Agrega una línea a la factura y recalcula los totales.</summary>
    /// <param name="detalle">Línea a agregar.</param>
    public void AgregarDetalle(FacturaDetalle detalle)
    {
        if (detalle == null)
        {
            throw new ArgumentNullException(nameof(detalle));
        }

        detalles.Add(detalle);
        RecalcularTotales();
    }

    /// <summary>Quita una línea de la factura y recalcula los totales.</summary>
    /// <param name="detalle">Línea a quitar.</param>
    /// <returns><c>true</c> si la línea estaba en la factura.</returns>
    public bool QuitarDetalle(FacturaDetalle detalle)
    {
        bool quitado = detalles.Remove(detalle);
        RecalcularTotales();
        return quitado;
    }

    /// <summary>Reemplaza todas las líneas por las indicadas y recalcula los totales.</summary>
    /// <param name="nuevosDetalles">Conjunto final de líneas.</param>
    public void EstablecerDetalles(IEnumerable<FacturaDetalle> nuevosDetalles)
    {
        if (nuevosDetalles == null)
        {
            throw new ArgumentNullException(nameof(nuevosDetalles));
        }

        List<FacturaDetalle> copia = new List<FacturaDetalle>(nuevosDetalles);
        detalles.Clear();
        detalles.AddRange(copia);
        RecalcularTotales();
    }

    /// <summary>Recalcula subtotal, ITBIS y total a partir de las líneas (redondeo a 2 decimales).</summary>
    public void RecalcularTotales()
    {
        Subtotal = detalles.Sum(detalle => detalle.Subtotal);
        Itbis = Math.Round(Subtotal * TasaItbis, 2, MidpointRounding.AwayFromZero);
        Total = Subtotal + Itbis;
        CantidadLineas = detalles.Count;
    }

    /// <inheritdoc />
    public override string ToString() => Numero.Length == 0 ? "Factura nueva" : Numero;
}
