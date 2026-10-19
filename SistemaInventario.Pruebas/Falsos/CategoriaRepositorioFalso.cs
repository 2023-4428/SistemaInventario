using System;
using System.Linq;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Entidades;

namespace SistemaInventario.Pruebas.Falsos;

/// <summary>Repositorio de categorías en memoria.</summary>
public sealed class CategoriaRepositorioFalso : RepositorioFalso<Categoria>, ICategoriaRepositorio
{
    public bool ExisteNombre(string nombre, int excluirId)
    {
        return Datos.Any(c => c.Id != excluirId && string.Equals(c.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
    }
}
