using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using SistemaInventario.AccesoDatos.Extensiones;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun;
using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Repositorios;

/// <summary>
/// Repositorio de facturas. Crear una factura guarda el encabezado y todas sus líneas dentro de una misma
/// transacción: o se guarda todo o no se guarda nada.
/// </summary>
public sealed class FacturaRepositorio : RepositorioBase<Factura>, IFacturaRepositorio
{
    /// <summary>Crea el repositorio con sus dependencias.</summary>
    /// <param name="conexionBD">Singleton de conexión.</param>
    /// <param name="registro">Logger de errores.</param>
    public FacturaRepositorio(ConexionBD conexionBD, ILogger registro)
        : base(conexionBD, registro)
    {
    }

    /// <inheritdoc />
    protected override string NombreTabla => "Factura";

    /// <inheritdoc />
    protected override string NombreEntidad => "factura";

    /// <inheritdoc />
    protected override string ClausulaOrden => "t.Id DESC";

    /// <inheritdoc />
    protected override string SqlSeleccion =>
        "SELECT t.Id, t.Numero, t.ClienteNombre, t.ClienteDocumento, t.Subtotal, t.Itbis, t.Total, " +
        "t.Activo, t.FechaCreacion, t.FechaModificacion, " +
        "(SELECT COUNT(*) FROM dbo.FacturaDetalle d WHERE d.FacturaId = t.Id) AS CantidadLineas " +
        "FROM dbo.Factura t";

    /// <inheritdoc />
    protected override string SqlInsertar =>
        "INSERT INTO dbo.Factura (ClienteNombre, ClienteDocumento, Subtotal, Itbis, Total) " +
        "OUTPUT INSERTED.Id VALUES (@ClienteNombre, @ClienteDocumento, @Subtotal, @Itbis, @Total);";

    /// <inheritdoc />
    protected override string SqlActualizar =>
        "UPDATE dbo.Factura SET ClienteNombre = @ClienteNombre, ClienteDocumento = @ClienteDocumento, " +
        "FechaModificacion = SYSDATETIME() WHERE Id = @Id;";

    /// <inheritdoc />
    protected override Factura Mapear(SqlDataReader lector)
    {
        Factura factura = new Factura
        {
            Numero = lector.LeerTexto("Numero"),
            ClienteNombre = lector.LeerTexto("ClienteNombre"),
            ClienteDocumento = lector.LeerTextoNulo("ClienteDocumento"),
            Subtotal = lector.LeerDecimal("Subtotal"),
            Itbis = lector.LeerDecimal("Itbis"),
            Total = lector.LeerDecimal("Total"),
            CantidadLineas = lector.LeerEntero("CantidadLineas")
        };

        MapearCamposBase(lector, factura);
        return factura;
    }

    /// <inheritdoc />
    protected override void AsignarParametros(SqlCommand comando, Factura entidad)
    {
        comando.AgregarParametro("@ClienteNombre", SqlDbType.NVarChar, entidad.ClienteNombre, Factura.LongitudMaximaCliente);
        comando.AgregarParametro("@ClienteDocumento", SqlDbType.NVarChar, entidad.ClienteDocumento, Factura.LongitudMaximaDocumento);
        comando.AgregarParametroDecimal("@Subtotal", entidad.Subtotal);
        comando.AgregarParametroDecimal("@Itbis", entidad.Itbis);
        comando.AgregarParametroDecimal("@Total", entidad.Total);
    }

    /// <summary>Obtiene la factura por identificador, incluyendo sus líneas.</summary>
    /// <param name="id">Identificador de la factura.</param>
    /// <returns>La factura con sus líneas, o <c>null</c> si no existe.</returns>
    public override Factura? ObtenerPorId(int id)
    {
        Factura? factura = base.ObtenerPorId(id);

        if (factura != null)
        {
            List<FacturaDetalle> detalles = CargarDetalles(factura.Id);
            factura.EstablecerDetalles(detalles);
            factura.CantidadLineas = detalles.Count;
        }

        return factura;
    }

    /// <summary>Inserta la factura y sus líneas en una sola transacción.</summary>
    /// <param name="entidad">Factura a insertar.</param>
    /// <returns>Identificador generado.</returns>
    public override int Insertar(Factura entidad)
    {
        if (entidad == null)
        {
            throw new ArgumentNullException(nameof(entidad));
        }

        return EjecutarEnTransaccion("insertar factura", (conexion, transaccion) =>
        {
            int id = InsertarEn(conexion, transaccion, entidad);

            foreach (FacturaDetalle detalle in entidad.Detalles)
            {
                InsertarDetalle(conexion, transaccion, id, detalle);
            }

            return id;
        });
    }

    /// <inheritdoc />
    protected override void EliminarDependientesEn(SqlConnection conexion, SqlTransaction transaccion, int id)
    {
        using SqlCommand comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText = "DELETE FROM dbo.FacturaDetalle WHERE FacturaId = @FacturaId";
        comando.AgregarParametro("@FacturaId", SqlDbType.Int, id);
        comando.ExecuteNonQuery();
    }

    private List<FacturaDetalle> CargarDetalles(int facturaId)
    {
        return Ejecutar("cargar las líneas de la factura", conexion =>
        {
            using SqlCommand comando = conexion.CreateCommand();
            comando.CommandText =
                "SELECT d.Id, d.FacturaId, d.ProductoId, p.Nombre AS ProductoNombre, d.Cantidad, d.PrecioUnitario " +
                "FROM dbo.FacturaDetalle d INNER JOIN dbo.Producto p ON p.Id = d.ProductoId " +
                "WHERE d.FacturaId = @FacturaId ORDER BY d.Id";
            comando.AgregarParametro("@FacturaId", SqlDbType.Int, facturaId);

            List<FacturaDetalle> detalles = new List<FacturaDetalle>();

            using SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                detalles.Add(new FacturaDetalle
                {
                    Id = lector.LeerEntero("Id"),
                    FacturaId = lector.LeerEntero("FacturaId"),
                    ProductoId = lector.LeerEntero("ProductoId"),
                    ProductoNombre = lector.LeerTexto("ProductoNombre"),
                    Cantidad = lector.LeerEntero("Cantidad"),
                    PrecioUnitario = lector.LeerDecimal("PrecioUnitario")
                });
            }

            return detalles;
        });
    }

    private static void InsertarDetalle(SqlConnection conexion, SqlTransaction transaccion, int facturaId, FacturaDetalle detalle)
    {
        using SqlCommand comando = conexion.CreateCommand();
        comando.Transaction = transaccion;
        comando.CommandText =
            "INSERT INTO dbo.FacturaDetalle (FacturaId, ProductoId, Cantidad, PrecioUnitario, Subtotal) " +
            "OUTPUT INSERTED.Id VALUES (@FacturaId, @ProductoId, @Cantidad, @PrecioUnitario, @Subtotal);";
        comando.AgregarParametro("@FacturaId", SqlDbType.Int, facturaId);
        comando.AgregarParametro("@ProductoId", SqlDbType.Int, detalle.ProductoId);
        comando.AgregarParametro("@Cantidad", SqlDbType.Int, detalle.Cantidad);
        comando.AgregarParametroDecimal("@PrecioUnitario", detalle.PrecioUnitario);
        comando.AgregarParametroDecimal("@Subtotal", detalle.Subtotal);

        detalle.Id = Convert.ToInt32(comando.ExecuteScalar(), CultureInfo.InvariantCulture);
        detalle.FacturaId = facturaId;
    }
}
