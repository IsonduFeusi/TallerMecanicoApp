using Microsoft.EntityFrameworkCore;
using TallerMecanicoApp.Data.Context;
using TallerMecanicoApp.Data.Entities;

namespace TallerMecanicoApp.Data.Repositories;

public interface IVehiculoRepository : IRepository<Vehiculo>
{
    Task<Vehiculo?> BuscarPorPlacaAsync(string placa);
    Task<IEnumerable<Vehiculo>> BuscarPorMarcaModeloAsync(string marca, string modelo);
    Task<IEnumerable<Vehiculo>> GetVehiculosConHistorialAsync();
    Task<Vehiculo?> GetVehiculoCompletoAsync(int id);
}

public class VehiculoRepository : Repository<Vehiculo>, IVehiculoRepository
{
    public VehiculoRepository(TallerMecanicoContext context) : base(context)
    {
    }

    public async Task<Vehiculo?> BuscarPorPlacaAsync(string placa)
    {
        return await _dbSet
            .Include(v => v.Cliente)
            .Include(v => v.Reparaciones)
            .Include(v => v.Fotos)
            .FirstOrDefaultAsync(v => v.Placa == placa);
    }

    public async Task<IEnumerable<Vehiculo>> BuscarPorMarcaModeloAsync(string marca, string modelo)
    {
        var query = _dbSet.Include(v => v.Cliente).AsQueryable();

        if (!string.IsNullOrEmpty(marca))
        {
            query = query.Where(v => v.Marca.Contains(marca));
        }

        if (!string.IsNullOrEmpty(modelo))
        {
            query = query.Where(v => v.Modelo.Contains(modelo));
        }

        return await query.OrderBy(v => v.Marca).ThenBy(v => v.Modelo).ToListAsync();
    }

    public async Task<IEnumerable<Vehiculo>> GetVehiculosConHistorialAsync()
    {
        return await _dbSet
            .Include(v => v.Cliente)
            .Include(v => v.Reparaciones)
            .OrderBy(v => v.Cliente.Nombre)
            .ThenBy(v => v.Marca)
            .ToListAsync();
    }

    public async Task<Vehiculo?> GetVehiculoCompletoAsync(int id)
    {
        return await _dbSet
            .Include(v => v.Cliente)
            .Include(v => v.Reparaciones)
            .Include(v => v.Fotos)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public override async Task<IEnumerable<Vehiculo>> GetAllAsync()
    {
        return await _dbSet
            .Include(v => v.Cliente)
            .OrderBy(v => v.Cliente.Nombre)
            .ThenBy(v => v.Marca)
            .ToListAsync();
    }
}