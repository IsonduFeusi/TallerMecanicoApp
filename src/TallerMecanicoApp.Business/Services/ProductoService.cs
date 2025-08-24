using TallerMecanicoApp.Data.Entities;
using TallerMecanicoApp.Data.Repositories;
using TallerMecanicoApp.Business.Interfaces;
using TallerMecanicoApp.Business.Models;

namespace TallerMecanicoApp.Business.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _productoRepository.GetAllAsync();
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _productoRepository.GetByIdAsync(id);
    }

    public async Task<Producto> CreateAsync(Producto producto)
    {
        // Validaciones de negocio
        if (string.IsNullOrWhiteSpace(producto.Nombre))
            throw new ArgumentException("El nombre del producto es requerido");

        if (string.IsNullOrWhiteSpace(producto.Categoria))
            throw new ArgumentException("La categoría del producto es requerida");

        if (producto.PrecioVenta <= 0)
            throw new ArgumentException("El precio de venta debe ser mayor a cero");

        if (producto.CostoCompra < 0)
            throw new ArgumentException("El costo de compra no puede ser negativo");

        // Verificar código de barras duplicado
        if (!string.IsNullOrEmpty(producto.CodigoBarras))
        {
            var productoExistente = await _productoRepository.BuscarPorCodigoBarrasAsync(producto.CodigoBarras);
            if (productoExistente != null)
                throw new InvalidOperationException("Ya existe un producto con ese código de barras");
        }

        producto.FechaRegistro = DateTime.Now;
        await _productoRepository.AddAsync(producto);
        await _productoRepository.SaveChangesAsync();
        return producto;
    }

    public async Task<Producto> UpdateAsync(Producto producto)
    {
        var productoExistente = await _productoRepository.GetByIdAsync(producto.Id);
        if (productoExistente == null)
            throw new InvalidOperationException("Producto no encontrado");

        // Verificar código de barras duplicado
        if (!string.IsNullOrEmpty(producto.CodigoBarras) && 
            producto.CodigoBarras != productoExistente.CodigoBarras)
        {
            var productoConCodigo = await _productoRepository.BuscarPorCodigoBarrasAsync(producto.CodigoBarras);
            if (productoConCodigo != null && productoConCodigo.Id != producto.Id)
                throw new InvalidOperationException("Ya existe un producto con ese código de barras");
        }

        await _productoRepository.UpdateAsync(producto);
        await _productoRepository.SaveChangesAsync();
        return producto;
    }

    public async Task DeleteAsync(int id)
    {
        await _productoRepository.DeleteAsync(id);
        await _productoRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Producto>> GetProductosBajoStockAsync()
    {
        return await _productoRepository.GetProductosBajoStockAsync();
    }

    public async Task<IEnumerable<Producto>> BuscarAsync(BusquedaProducto criterios)
    {
        var productos = await _productoRepository.GetAllAsync();

        // Aplicar filtros
        if (!string.IsNullOrEmpty(criterios.Nombre))
        {
            productos = productos.Where(p => 
                p.Nombre.Contains(criterios.Nombre, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(p.Marca) && p.Marca.Contains(criterios.Nombre, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrEmpty(criterios.Categoria))
        {
            productos = productos.Where(p => p.Categoria.Equals(criterios.Categoria, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(criterios.Marca))
        {
            productos = productos.Where(p => !string.IsNullOrEmpty(p.Marca) && 
                p.Marca.Contains(criterios.Marca, StringComparison.OrdinalIgnoreCase));
        }

        if (criterios.SoloBajoStock == true)
        {
            productos = productos.Where(p => p.BajoStock);
        }

        return productos.OrderBy(p => p.Categoria).ThenBy(p => p.Nombre);
    }

    public async Task<bool> ActualizarStockAsync(int productoId, int nuevaCantidad)
    {
        var producto = await _productoRepository.GetByIdAsync(productoId);
        if (producto == null)
            return false;

        if (nuevaCantidad < 0)
            throw new ArgumentException("La cantidad no puede ser negativa");

        producto.Stock = nuevaCantidad;
        await _productoRepository.UpdateAsync(producto);
        await _productoRepository.SaveChangesAsync();
        return true;
    }
}