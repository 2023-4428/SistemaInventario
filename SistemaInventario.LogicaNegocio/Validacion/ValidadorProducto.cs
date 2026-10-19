using System.Globalization;
using SistemaInventario.Entidades;

namespace SistemaInventario.LogicaNegocio.Validacion;

/// <summary>Valida los campos de un <see cref="Producto"/>: nombre, precio (&gt; 0, 2 decimales) y referencias.</summary>
public sealed class ValidadorProducto : IValidador<Producto>
{
    /// <inheritdoc />
    public ResultadoValidacion Validar(Producto entidad)
    {
        ResultadoValidacion resultado = new ResultadoValidacion();

        string nombre = (entidad.Nombre ?? string.Empty).Trim();
        if (nombre.Length == 0)
        {
            resultado.Agregar(nameof(Producto.Nombre), "El nombre del producto es obligatorio.");
        }
        else if (nombre.Length < Producto.LongitudMinimaNombre || nombre.Length > Producto.LongitudMaximaNombre)
        {
            resultado.Agregar(
                nameof(Producto.Nombre),
                $"El nombre debe tener entre {Producto.LongitudMinimaNombre} y {Producto.LongitudMaximaNombre} caracteres.");
        }

        if (entidad.PrecioUnitario <= 0)
        {
            resultado.Agregar(nameof(Producto.PrecioUnitario), "El precio debe ser mayor que 0.");
        }
        else if (entidad.PrecioUnitario > Producto.PrecioMaximo)
        {
            resultado.Agregar(
                nameof(Producto.PrecioUnitario),
                $"El precio no puede exceder {Producto.PrecioMaximo.ToString("N2", CultureInfo.CurrentCulture)}.");
        }
        else if (!PatronesValidacion.TieneMaximoDosDecimales(entidad.PrecioUnitario))
        {
            resultado.Agregar(nameof(Producto.PrecioUnitario), "El precio admite como máximo 2 decimales.");
        }

        if (entidad.CategoriaId <= 0)
        {
            resultado.Agregar(nameof(Producto.CategoriaId), "Seleccione una categoría.");
        }

        if (entidad.SuplidorId <= 0)
        {
            resultado.Agregar(nameof(Producto.SuplidorId), "Seleccione un suplidor.");
        }

        return resultado;
    }
}
