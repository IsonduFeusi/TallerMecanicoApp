using Microsoft.EntityFrameworkCore;
using TallerMecanicoApp.Data.Context;
using TallerMecanicoApp.Data.Entities;

namespace TallerMecanicoApp.Data.Repositories;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<IEnumerable<Cliente>> BuscarPorNombreAsync(string nombre);
    Task<Cliente?> BuscarPorTelefonoAsync(string telefono);
    Task<IEnumerable<Cliente>> GetClientesConVehiculosAsync();
}

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(TallerMecanicoContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Cliente>> BuscarPorNombreAsync(string nombre)
    {
        return await _dbSet
            .Where(c => c.Nombre.Contains(nombre))
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }

    public async Task<Cliente?> BuscarPorTelefonoAsync(string telefono)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Telefono == telefono);
    }

    public async Task<IEnumerable<Cliente>> GetClientesConVehiculosAsync()
    {
        return await _dbSet
            .Include(c => c.Vehiculos)
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _dbSet
            .Include(c => c.Vehiculos)
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }
}