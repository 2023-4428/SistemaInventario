using System;
using SistemaInventario.Entidades;

namespace SistemaInventario.Pruebas.Soporte;

/// <summary>Fábricas de entidades y archivos de ejemplo para las pruebas.</summary>
public static class ConstructorDatos
{
    public static byte[] BytesJpeg(int longitud = 64)
    {
        byte[] bytes = new byte[longitud];
        bytes[0] = 0xFF;
        bytes[1] = 0xD8;
        bytes[2] = 0xFF;
        return bytes;
    }

    public static byte[] BytesPng(int longitud = 64)
    {
        byte[] bytes = new byte[longitud];
        byte[] firma = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        Array.Copy(firma, bytes, firma.Length);
        return bytes;
    }

    public static FotoCategoria FotoJpg(string nombre = "foto.jpg") =>
        new FotoCategoria { NombreArchivo = nombre, Contenido = BytesJpeg() };

    public static FotoCategoria FotoPng(string nombre = "foto.png") =>
        new FotoCategoria { NombreArchivo = nombre, Contenido = BytesPng() };

    public static Categoria CategoriaValida(string nombre = "Electrónica") =>
        new Categoria { Nombre = nombre, Descripcion = "Descripción de la categoría." };

    public static Suplidor SuplidorValido(string empresa = "Distribuidora Caribe SRL") =>
        new Suplidor
        {
            NombreEmpresa = empresa,
            NombreContacto = "María Fernández",
            Telefono = "809-555-0101",
            Correo = "ventas@empresa.com",
            SitioWeb = "https://www.empresa.com"
        };

    public static Producto ProductoValido(int categoriaId, int suplidorId, string nombre = "Licuadora", decimal precio = 3200m) =>
        new Producto { Nombre = nombre, PrecioUnitario = precio, CategoriaId = categoriaId, SuplidorId = suplidorId };

    public static Factura FacturaValida(string cliente = "Juan Pérez", params (Producto Producto, int Cantidad)[] lineas)
    {
        Factura factura = new Factura { ClienteNombre = cliente };

        foreach ((Producto producto, int cantidad) in lineas)
        {
            factura.AgregarDetalle(new FacturaDetalle
            {
                ProductoId = producto.Id,
                ProductoNombre = producto.Nombre,
                Cantidad = cantidad,
                PrecioUnitario = producto.PrecioUnitario
            });
        }

        return factura;
    }
}
