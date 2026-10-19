using System;
using SistemaInventario.AccesoDatos;
using SistemaInventario.AccesoDatos.Repositorios;
using SistemaInventario.Comun;
using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Servicios;
using SistemaInventario.LogicaNegocio.Validacion;

namespace SistemaInventario.LogicaNegocio.Composicion;

/// <summary>
/// Raíz de composición de la aplicación: crea el grafo de objetos (repositorios, validadores y servicios)
/// conectándolos mediante inyección por constructor.
/// <para>
/// Existe en la capa de Lógica de Negocio para que la capa de Presentación pueda obtener los servicios
/// sin referenciar jamás a la capa de Acceso a Datos.
/// </para>
/// </summary>
public sealed class ContenedorServicios
{
    private readonly ConexionBD conexion;

    /// <summary>Construye todos los servicios de la aplicación.</summary>
    /// <param name="cadenaConexion">Cadena de conexión de SQL Server.</param>
    /// <param name="logger">Logger compartido por todas las capas.</param>
    public ContenedorServicios(string cadenaConexion, ILogger logger)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        Logger = logger;

        conexion = ConexionBD.Instancia;
        conexion.Configurar(cadenaConexion);

        // Capa de acceso a datos
        CategoriaRepositorio repositorioCategorias = new CategoriaRepositorio(conexion, logger);
        SuplidorRepositorio repositorioSuplidores = new SuplidorRepositorio(conexion, logger);
        ProductoRepositorio repositorioProductos = new ProductoRepositorio(conexion, logger);
        FacturaRepositorio repositorioFacturas = new FacturaRepositorio(conexion, logger);

        // Validadores
        ValidadorFoto validadorFoto = new ValidadorFoto();
        IValidador<Categoria> validadorCategoria = new ValidadorCategoria(validadorFoto);
        IValidador<Suplidor> validadorSuplidor = new ValidadorSuplidor();
        IValidador<Producto> validadorProducto = new ValidadorProducto();
        IValidador<Factura> validadorFactura = new ValidadorFactura();

        // Servicios
        Categorias = new CategoriaServicio(repositorioCategorias, repositorioProductos, validadorCategoria, validadorFoto);
        Suplidores = new SuplidorServicio(repositorioSuplidores, repositorioProductos, validadorSuplidor);
        Productos = new ProductoServicio(repositorioProductos, repositorioCategorias, repositorioSuplidores, validadorProducto);
        Facturas = new FacturaServicio(repositorioFacturas, repositorioProductos, validadorFactura);
        Reportes = new ReporteServicio(repositorioProductos, repositorioCategorias, repositorioSuplidores);
    }

    /// <summary>Logger compartido.</summary>
    public ILogger Logger { get; }

    /// <summary>Servicio de categorías.</summary>
    public ICategoriaServicio Categorias { get; }

    /// <summary>Servicio de suplidores.</summary>
    public ISuplidorServicio Suplidores { get; }

    /// <summary>Servicio de productos.</summary>
    public IProductoServicio Productos { get; }

    /// <summary>Servicio de facturas.</summary>
    public IFacturaServicio Facturas { get; }

    /// <summary>Servicio de reportes LINQ.</summary>
    public IReporteServicio Reportes { get; }

    /// <summary>Comprueba que la base de datos esté disponible.</summary>
    /// <param name="mensajeError">Mensaje de error si la prueba falla; <c>null</c> si tuvo éxito.</param>
    /// <returns><c>true</c> si se pudo abrir la conexión.</returns>
    public bool ProbarConexion(out string? mensajeError)
    {
        bool disponible = conexion.ProbarConexion(out mensajeError);

        if (!disponible)
        {
            Logger.Error("No se pudo conectar con la base de datos: " + mensajeError);
        }

        return disponible;
    }
}
