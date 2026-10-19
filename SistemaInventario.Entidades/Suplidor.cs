namespace SistemaInventario.Entidades;

/// <summary>Empresa que suministra productos.</summary>
public class Suplidor : EntidadBase
{
    /// <summary>Longitud mínima del nombre de la empresa y del contacto.</summary>
    public const int LongitudMinimaNombre = 2;

    /// <summary>Longitud máxima del nombre de la empresa.</summary>
    public const int LongitudMaximaEmpresa = 150;

    /// <summary>Longitud máxima del nombre del contacto.</summary>
    public const int LongitudMaximaContacto = 100;

    /// <summary>Longitud máxima del correo electrónico.</summary>
    public const int LongitudMaximaCorreo = 150;

    /// <summary>Longitud máxima del sitio web.</summary>
    public const int LongitudMaximaSitioWeb = 250;

    /// <summary>Nombre de la empresa (obligatorio y único).</summary>
    public string NombreEmpresa { get; set; } = string.Empty;

    /// <summary>Nombre de la persona de contacto (obligatorio).</summary>
    public string NombreContacto { get; set; } = string.Empty;

    /// <summary>Teléfono con formato dominicano: 809/829/849-XXX-XXXX.</summary>
    public string Telefono { get; set; } = string.Empty;

    /// <summary>Correo electrónico de contacto.</summary>
    public string Correo { get; set; } = string.Empty;

    /// <summary>Sitio web (opcional). Si se indica debe ser una URL http o https válida.</summary>
    public string? SitioWeb { get; set; }

    /// <inheritdoc />
    public override string ToString() => NombreEmpresa;
}
