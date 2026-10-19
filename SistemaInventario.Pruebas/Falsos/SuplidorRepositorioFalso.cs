using System;
using System.Linq;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Entidades;

namespace SistemaInventario.Pruebas.Falsos;

/// <summary>Repositorio de suplidores en memoria.</summary>
public sealed class SuplidorRepositorioFalso : RepositorioFalso<Suplidor>, ISuplidorRepositorio
{
    public bool ExisteNombreEmpresa(string nombreEmpresa, int excluirId)
    {
        return Datos.Any(s => s.Id != excluirId && string.Equals(s.NombreEmpresa, nombreEmpresa, StringComparison.OrdinalIgnoreCase));
    }
}
