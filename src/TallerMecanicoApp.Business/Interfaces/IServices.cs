using TallerMecanicoApp.Data.Entities;
using TallerMecanicoApp.Business.Models;

namespace TallerMecanicoApp.Business.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente);
    Task DeleteAsync(int id);
    Task<IEnumerable<Cliente>> BuscarPorNombreAsync(string nombre);
    Task<Cliente?> BuscarPorTelefonoAsync(string telefono);
}

public interface IVehiculoService
{
    Task<IEnumerable<Vehiculo>> GetAllAsync();
    Task<Vehiculo?> GetByIdAsync(int id);
    Task<Vehiculo> CreateAsync(Vehiculo vehiculo);
    Task<Vehiculo> UpdateAsync(Vehiculo vehiculo);
    Task DeleteAsync(int id);
    Task<Vehiculo?> BuscarPorPlacaAsync(string placa);
    Task<IEnumerable<Vehiculo>> BuscarAsync(BusquedaVehiculo criterios);
}

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task<Producto> CreateAsync(Producto producto);
    Task<Producto> UpdateAsync(Producto producto);
    Task DeleteAsync(int id);
    Task<IEnumerable<Producto>> GetProductosBajoStockAsync();
    Task<IEnumerable<Producto>> BuscarAsync(BusquedaProducto criterios);
    Task<bool> ActualizarStockAsync(int productoId, int nuevaCantidad);
}

public interface IDashboardService
{
    Task<ResumenVentas> GetResumenVentasAsync();
    Task<IEnumerable<ProductoMasVendido>> GetProductosMasVendidosAsync(int cantidad = 5);
}