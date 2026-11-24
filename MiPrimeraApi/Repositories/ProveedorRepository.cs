using Microsoft.EntityFrameworkCore;
using MiPrimeraApi.Data;
using MiPrimeraApi.Interfaces;
using MiPrimeraApi.Models;

namespace MiPrimeraApi.Repositories;

public class ProveedorRepository : IProveedorRepository
{
    private readonly ApplicationDbContext _context;

    public ProveedorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Proveedor>> GetAllAsync()
    {
        return await _context.Proveedores.AsNoTracking().ToListAsync();
    }

    public async Task<Proveedor?> GetByIdAsync(int id)
    {
        return await _context.Proveedores.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Proveedor> AddAsync(Proveedor proveedor)
    {
        await _context.Proveedores.AddAsync(proveedor);
        return proveedor;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
