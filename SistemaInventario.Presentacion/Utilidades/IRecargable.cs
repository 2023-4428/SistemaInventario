namespace SistemaInventario.Presentacion.Utilidades;

/// <summary>Vista que puede volver a cargar sus datos cuando el usuario regresa a ella.</summary>
internal interface IRecargable
{
    /// <summary>Vuelve a consultar los datos y refresca la pantalla.</summary>
    void Recargar();
}
