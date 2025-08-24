using TallerMecanicoApp.Data.Entities;
using TallerMecanicoApp.Data.Repositories;
using TallerMecanicoApp.Business.Interfaces;
using TallerMecanicoApp.Business.Models;

namespace TallerMecanicoApp.Business.Services;

public class VehiculoService : IVehiculoService
{
    private readonly IVehiculoRepository _vehiculoRepository;
    private readonly IClienteRepository _clienteRepository;

    public VehiculoService(IVehiculoRepository vehiculoRepository, IClienteRepository clienteRepository)
    {
        _vehiculoRepository = vehiculoRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<Vehiculo>> GetAllAsync()
    {
        return await _vehiculoRepository.GetAllAsync();
    }

    public async Task<Vehiculo?> GetByIdAsync(int id)
    {
        return await _vehiculoRepository.GetVehiculoCompletoAsync(id);
    }

    public async Task<Vehiculo> CreateAsync(Vehiculo vehiculo)
    {
        // Validaciones de negocio
        if (string.IsNullOrWhiteSpace(vehiculo.Marca))
            throw new ArgumentException("La marca del vehículo es requerida");

        if (string.IsNullOrWhiteSpace(vehiculo.Modelo))
            throw new ArgumentException("El modelo del vehículo es requerido");

        if (string.IsNullOrWhiteSpace(vehiculo.Placa))
            throw new ArgumentException("La placa del vehículo es requerida");

        // Verificar que el cliente existe
        var cliente = await _clienteRepository.GetByIdAsync(vehiculo.ClienteId);
        if (cliente == null)
            throw new InvalidOperationException("El cliente especificado no existe");

        // Verificar que la placa no esté duplicada
        var vehiculoExistente = await _vehiculoRepository.BuscarPorPlacaAsync(vehiculo.Placa);
        if (vehiculoExistente != null)
            throw new InvalidOperationException("Ya existe un vehículo con esa placa");

        vehiculo.FechaRegistro = DateTime.Now;
        await _vehiculoRepository.AddAsync(vehiculo);
        await _vehiculoRepository.SaveChangesAsync();
        return vehiculo;
    }

    public async Task<Vehiculo> UpdateAsync(Vehiculo vehiculo)
    {
        var vehiculoExistente = await _vehiculoRepository.GetByIdAsync(vehiculo.Id);
        if (vehiculoExistente == null)
            throw new InvalidOperationException("Vehículo no encontrado");

        // Verificar placa duplicada
        if (vehiculo.Placa != vehiculoExistente.Placa)
        {
            var vehiculoConPlaca = await _vehiculoRepository.BuscarPorPlacaAsync(vehiculo.Placa);
            if (vehiculoConPlaca != null && vehiculoConPlaca.Id != vehiculo.Id)
                throw new InvalidOperationException("Ya existe un vehículo con esa placa");
        }

        await _vehiculoRepository.UpdateAsync(vehiculo);
        await _vehiculoRepository.SaveChangesAsync();
        return vehiculo;
    }

    public async Task DeleteAsync(int id)
    {
        await _vehiculoRepository.DeleteAsync(id);
        await _vehiculoRepository.SaveChangesAsync();
    }

    public async Task<Vehiculo?> BuscarPorPlacaAsync(string placa)
    {
        if (string.IsNullOrWhiteSpace(placa))
            return null;

        return await _vehiculoRepository.BuscarPorPlacaAsync(placa.ToUpper());
    }

    public async Task<IEnumerable<Vehiculo>> BuscarAsync(BusquedaVehiculo criterios)
    {
        var vehiculos = await _vehiculoRepository.GetAllAsync();
        
        // Aplicar filtros
        if (!string.IsNullOrEmpty(criterios.Placa))
        {
            vehiculos = vehiculos.Where(v => v.Placa.Contains(criterios.Placa.ToUpper()));
        }

        if (!string.IsNullOrEmpty(criterios.Marca))
        {
            vehiculos = vehiculos.Where(v => v.Marca.Contains(criterios.Marca, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(criterios.Modelo))
        {
            vehiculos = vehiculos.Where(v => v.Modelo.Contains(criterios.Modelo, StringComparison.OrdinalIgnoreCase));
        }

        if (criterios.Año.HasValue)
        {
            vehiculos = vehiculos.Where(v => v.Año == criterios.Año.Value);
        }

        if (!string.IsNullOrEmpty(criterios.NombreCliente))
        {
            vehiculos = vehiculos.Where(v => v.Cliente.Nombre.Contains(criterios.NombreCliente, StringComparison.OrdinalIgnoreCase));
        }

        return vehiculos;
    }
}