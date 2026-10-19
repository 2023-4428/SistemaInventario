using System.Drawing;

namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>Paleta de colores y fuentes compartidas por toda la interfaz.</summary>
internal static class Tema
{
    public static readonly Color Primario = Color.FromArgb(37, 99, 235);
    public static readonly Color PrimarioHover = Color.FromArgb(29, 78, 216);
    public static readonly Color Peligro = Color.FromArgb(220, 38, 38);
    public static readonly Color PeligroHover = Color.FromArgb(185, 28, 28);
    public static readonly Color Exito = Color.FromArgb(22, 163, 74);
    public static readonly Color Lateral = Color.FromArgb(17, 24, 39);
    public static readonly Color LateralHover = Color.FromArgb(31, 41, 55);
    public static readonly Color LateralActivo = Color.FromArgb(55, 65, 81);
    public static readonly Color Fondo = Color.FromArgb(243, 244, 246);
    public static readonly Color Superficie = Color.White;
    public static readonly Color Borde = Color.FromArgb(209, 213, 219);
    public static readonly Color Texto = Color.FromArgb(17, 24, 39);
    public static readonly Color TextoSuave = Color.FromArgb(107, 114, 128);
    public static readonly Color FilaAlterna = Color.FromArgb(249, 250, 251);
    public static readonly Color EncabezadoGrid = Color.FromArgb(243, 244, 246);
    public static readonly Color Seleccion = Color.FromArgb(219, 234, 254);

    public static readonly Font FuenteBase = new Font("Segoe UI", 9.5F);
    public static readonly Font FuenteNegrita = new Font("Segoe UI Semibold", 9.5F);
    public static readonly Font FuentePequena = new Font("Segoe UI", 8.5F);
    public static readonly Font FuenteSubtitulo = new Font("Segoe UI", 10F);
    public static readonly Font FuenteTitulo = new Font("Segoe UI Semibold", 17F);
    public static readonly Font FuenteMarca = new Font("Segoe UI Semibold", 15F);
}
