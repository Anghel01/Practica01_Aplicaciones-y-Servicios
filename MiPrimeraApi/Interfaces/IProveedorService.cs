using MiPrimeraApi.Models;

namespace MiPrimeraApi.Interfaces;

public interface IProveedorService
{
    Task<IEnumerable<Proveedor>> ObtenerTodosAsync();
    Task<Proveedor?> BuscarPorIdAsync(int id);
    Task<Proveedor> CrearProveedorAsync(Proveedor proveedor);
}
