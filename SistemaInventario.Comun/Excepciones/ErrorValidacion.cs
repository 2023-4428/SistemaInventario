namespace SistemaInventario.Comun.Excepciones;

/// <summary>
/// Describe un error de validación asociado a un campo.
/// La interfaz usa <see cref="Campo"/> para decidir en qué control mostrar el mensaje con <c>ErrorProvider</c>.
/// </summary>
/// <param name="Campo">Nombre de la propiedad/campo con el error (por ejemplo, "Nombre").</param>
/// <param name="Mensaje">Mensaje legible para el usuario.</param>
public sealed record ErrorValidacion(string Campo, string Mensaje);
