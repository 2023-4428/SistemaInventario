using SistemaInventario.Entidades;
using SistemaInventario.LogicaNegocio.Validacion;

namespace SistemaInventario.LogicaNegocio.Servicios;

/// <summary>Servicio de negocio de categorías.</summary>
public interface ICategoriaServicio : IServicio<Categoria>
{
    /// <summary>Valida una foto (extensión, tamaño y contenido) sin guardarla, para dar respuesta inmediata en la interfaz.</summary>
    /// <param name="foto">Foto a validar.</param>
    /// <returns>Resultado con los errores encontrados.</returns>
    ResultadoValidacion ValidarFoto(FotoCategoria foto);
}
