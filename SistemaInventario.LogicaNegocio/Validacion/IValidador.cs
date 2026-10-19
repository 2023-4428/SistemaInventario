namespace SistemaInventario.LogicaNegocio.Validacion;

/// <summary>Contrato genérico de un validador de entidades.</summary>
/// <typeparam name="T">Tipo de entidad que valida.</typeparam>
public interface IValidador<in T>
{
    /// <summary>Valida los campos de la entidad aplicando las reglas del dominio.</summary>
    /// <param name="entidad">Entidad a validar.</param>
    /// <returns>Resultado con los errores encontrados (vacío si es válida).</returns>
    ResultadoValidacion Validar(T entidad);
}
