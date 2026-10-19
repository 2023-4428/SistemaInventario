using System.Data;
using Microsoft.Data.SqlClient;
using SistemaInventario.AccesoDatos.Extensiones;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Comun;
using SistemaInventario.Entidades;

namespace SistemaInventario.AccesoDatos.Repositorios;

/// <summary>Repositorio de suplidores.</summary>
public sealed class SuplidorRepositorio : RepositorioBase<Suplidor>, ISuplidorRepositorio
{
    /// <summary>Crea el repositorio con sus dependencias.</summary>
    /// <param name="conexionBD">Singleton de conexión.</param>
    /// <param name="registro">Logger de errores.</param>
    public SuplidorRepositorio(ConexionBD conexionBD, ILogger registro)
        : base(conexionBD, registro)
    {
    }

    /// <inheritdoc />
    protected override string NombreTabla => "Suplidor";

    /// <inheritdoc />
    protected override string NombreEntidad => "suplidor";

    /// <inheritdoc />
    protected override string ClausulaOrden => "t.NombreEmpresa";

    /// <inheritdoc />
    protected override string SqlSeleccion =>
        "SELECT t.Id, t.NombreEmpresa, t.NombreContacto, t.Telefono, t.Correo, t.SitioWeb, " +
        "t.Activo, t.FechaCreacion, t.FechaModificacion FROM dbo.Suplidor t";

    /// <inheritdoc />
    protected override string SqlInsertar =>
        "INSERT INTO dbo.Suplidor (NombreEmpresa, NombreContacto, Telefono, Correo, SitioWeb) " +
        "OUTPUT INSERTED.Id VALUES (@NombreEmpresa, @NombreContacto, @Telefono, @Correo, @SitioWeb);";

    /// <inheritdoc />
    protected override string SqlActualizar =>
        "UPDATE dbo.Suplidor SET NombreEmpresa = @NombreEmpresa, NombreContacto = @NombreContacto, " +
        "Telefono = @Telefono, Correo = @Correo, SitioWeb = @SitioWeb, FechaModificacion = SYSDATETIME() " +
        "WHERE Id = @Id;";

    /// <inheritdoc />
    protected override Suplidor Mapear(SqlDataReader lector)
    {
        Suplidor suplidor = new Suplidor
        {
            NombreEmpresa = lector.LeerTexto("NombreEmpresa"),
            NombreContacto = lector.LeerTexto("NombreContacto"),
            Telefono = lector.LeerTexto("Telefono"),
            Correo = lector.LeerTexto("Correo"),
            SitioWeb = lector.LeerTextoNulo("SitioWeb")
        };

        MapearCamposBase(lector, suplidor);
        return suplidor;
    }

    /// <inheritdoc />
    protected override void AsignarParametros(SqlCommand comando, Suplidor entidad)
    {
        comando.AgregarParametro("@NombreEmpresa", SqlDbType.NVarChar, entidad.NombreEmpresa, Suplidor.LongitudMaximaEmpresa);
        comando.AgregarParametro("@NombreContacto", SqlDbType.NVarChar, entidad.NombreContacto, Suplidor.LongitudMaximaContacto);
        comando.AgregarParametro("@Telefono", SqlDbType.NVarChar, entidad.Telefono, 12);
        comando.AgregarParametro("@Correo", SqlDbType.NVarChar, entidad.Correo, Suplidor.LongitudMaximaCorreo);
        comando.AgregarParametro("@SitioWeb", SqlDbType.NVarChar, entidad.SitioWeb, Suplidor.LongitudMaximaSitioWeb);
    }

    /// <inheritdoc />
    public bool ExisteNombreEmpresa(string nombreEmpresa, int excluirId)
    {
        return ExisteValor("NombreEmpresa", nombreEmpresa, Suplidor.LongitudMaximaEmpresa, excluirId);
    }
}
