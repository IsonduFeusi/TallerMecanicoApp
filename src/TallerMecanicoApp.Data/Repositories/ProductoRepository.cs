using Microsoft.EntityFrameworkCore;
using TallerMecanicoApp.Data.Context;
using TallerMecanicoApp.Data.Entities;

namespace TallerMecanicoApp.Data.Repositories;

public interface IProductoRepository : IRepository<Producto>
{
    Task<IEnumerable<Producto>> GetProductosBajoStockAsync();
    Task<IEnumerable<Producto>> BuscarPorCategoriaAsync(string categoria);
    Task<IEnumerable<Producto>> BuscarPorNombreAsync(string nombre);
    Task<Producto?> BuscarPorCodigoBarrasAsync(string codigoBarras);
    Task<IEnumerable<string>> GetCategoriasAsync();
}

public class ProductoRepository : Repository<Producto>, IProductoRepository
{
    public ProductoRepository(TallerMecanicoContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Producto>> GetProductosBajoStockAsync()
    {
        return await _dbSet
            .Where(p => p.Stock <= p.StockMinimo)
            .OrderBy(p => p.Stock)
            .ThenBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Producto>> BuscarPorCategoriaAsync(string categoria)
    {
        return await _dbSet
            .Where(p => p.Categoria == categoria)
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Producto>> BuscarPorNombreAsync(string nombre)
    {
        return await _dbSet
            .Where(p => p.Nombre.Contains(nombre) || p.Marca!.Contains(nombre))
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<Producto?> BuscarPorCodigoBarrasAsync(string codigoBarras)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.CodigoBarras == codigoBarras);
    }

    public async Task<IEnumerable<string>> GetCategoriasAsync()
    {
        return await _dbSet
            .Select(p => p.Categoria)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _dbSet
            .OrderBy(p => p.Categoria)
            .ThenBy(p => p.Nombre)
            .ToListAsync();
    }
}