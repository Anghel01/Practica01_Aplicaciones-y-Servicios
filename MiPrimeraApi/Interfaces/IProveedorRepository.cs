using MiPrimeraApi.Models;

namespace MiPrimeraApi.Interfaces;

public interface IProveedorRepository
{
    Task<IEnumerable<Proveedor>> GetAllAsync();
    Task<Proveedor?> GetByIdAsync(int id);
    Task<Proveedor> AddAsync(Proveedor proveedor);
    Task SaveChangesAsync();
}
