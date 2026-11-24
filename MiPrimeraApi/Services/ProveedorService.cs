using MiPrimeraApi.Interfaces;
using MiPrimeraApi.Models;

namespace MiPrimeraApi.Services;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepository _repository;

    public ProveedorService(IProveedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Proveedor>> ObtenerTodosAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Proveedor?> BuscarPorIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Proveedor> CrearProveedorAsync(Proveedor proveedor)
    {
        await _repository.AddAsync(proveedor);
        await _repository.SaveChangesAsync();
        return proveedor;
    }
}
