using TallerMecanicoApp.Data.Entities;
using TallerMecanicoApp.Data.Repositories;
using TallerMecanicoApp.Business.Interfaces;
using TallerMecanicoApp.Business.Models;

namespace TallerMecanicoApp.Business.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _clienteRepository.GetAllAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _clienteRepository.GetByIdAsync(id);
    }

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        // Validaciones de negocio
        if (string.IsNullOrWhiteSpace(cliente.Nombre))
            throw new ArgumentException("El nombre del cliente es requerido");

        // Verificar si ya existe un cliente con el mismo teléfono
        if (!string.IsNullOrEmpty(cliente.Telefono))
        {
            var clienteExistente = await _clienteRepository.BuscarPorTelefonoAsync(cliente.Telefono);
            if (clienteExistente != null)
                throw new InvalidOperationException("Ya existe un cliente con ese número de teléfono");
        }

        cliente.FechaRegistro = DateTime.Now;
        await _clienteRepository.AddAsync(cliente);
        await _clienteRepository.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente)
    {
        var clienteExistente = await _clienteRepository.GetByIdAsync(cliente.Id);
        if (clienteExistente == null)
            throw new InvalidOperationException("Cliente no encontrado");

        // Verificar teléfono duplicado
        if (!string.IsNullOrEmpty(cliente.Telefono) && cliente.Telefono != clienteExistente.Telefono)
        {
            var clienteConTelefono = await _clienteRepository.BuscarPorTelefonoAsync(cliente.Telefono);
            if (clienteConTelefono != null && clienteConTelefono.Id != cliente.Id)
                throw new InvalidOperationException("Ya existe un cliente con ese número de teléfono");
        }

        await _clienteRepository.UpdateAsync(cliente);
        await _clienteRepository.SaveChangesAsync();
        return cliente;
    }

    public async Task DeleteAsync(int id)
    {
        await _clienteRepository.DeleteAsync(id);
        await _clienteRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Cliente>> BuscarPorNombreAsync(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return new List<Cliente>();

        return await _clienteRepository.BuscarPorNombreAsync(nombre);
    }

    public async Task<Cliente?> BuscarPorTelefonoAsync(string telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
            return null;

        return await _clienteRepository.BuscarPorTelefonoAsync(telefono);
    }
}