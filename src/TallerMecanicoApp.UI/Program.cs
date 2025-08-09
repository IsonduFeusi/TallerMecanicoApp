using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TallerMecanicoApp.Data.Context;
using TallerMecanicoApp.Data.Repositories;
using TallerMecanicoApp.Business.Services;
using TallerMecanicoApp.Business.Interfaces;
using TallerMecanicoApp.Data.Entities;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("=== TALLER MECÁNICO Y TIENDA DE LLANTAS ===");
Console.WriteLine("Sistema de Gestión - Versión Demo");
Console.WriteLine();

// Configuración de servicios
var services = new ServiceCollection();
ConfigureServices(services);

using var serviceProvider = services.BuildServiceProvider();

try
{
    // Inicializar base de datos
    await InicializarBaseDatos(serviceProvider);
    
    // Ejecutar menú principal
    await EjecutarMenuPrincipal(serviceProvider);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

static void ConfigureServices(IServiceCollection services)
{
    // Configurar Entity Framework con SQLite
    services.AddDbContext<TallerMecanicoContext>(options =>
        options.UseSqlite("Data Source=taller_mecanico.db"));
    
    // Configurar repositorios
    services.AddScoped<IClienteRepository, ClienteRepository>();
    services.AddScoped<IVehiculoRepository, VehiculoRepository>();
    services.AddScoped<IProductoRepository, ProductoRepository>();
    
    // Configurar servicios de negocio
    services.AddScoped<IClienteService, ClienteService>();
    services.AddScoped<IVehiculoService, VehiculoService>();
    services.AddScoped<IProductoService, ProductoService>();
}

static async Task InicializarBaseDatos(ServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TallerMecanicoContext>();
    
    // Crear la base de datos y aplicar migraciones
    await context.Database.EnsureCreatedAsync();
    
    Console.WriteLine("✓ Base de datos inicializada correctamente");
    Console.WriteLine($"✓ Productos iniciales: {await context.Productos.CountAsync()}");
    Console.WriteLine($"✓ Proveedores iniciales: {await context.Proveedores.CountAsync()}");
    Console.WriteLine();
}

static async Task EjecutarMenuPrincipal(ServiceProvider serviceProvider)
{
    var continuar = true;
    
    while (continuar)
    {
        MostrarMenuPrincipal();
        var opcion = Console.ReadLine();
        
        switch (opcion)
        {
            case "1":
                await GestionarClientes(serviceProvider);
                break;
            case "2":
                await GestionarVehiculos(serviceProvider);
                break;
            case "3":
                await GestionarInventario(serviceProvider);
                break;
            case "4":
                await MostrarResumen(serviceProvider);
                break;
            case "5":
                continuar = false;
                break;
            default:
                Console.WriteLine("Opción no válida. Presione Enter para continuar...");
                Console.ReadLine();
                break;
        }
        
        Console.Clear();
    }
}

static void MostrarMenuPrincipal()
{
    Console.WriteLine("=== MENÚ PRINCIPAL ===");
    Console.WriteLine("1. Gestión de Clientes");
    Console.WriteLine("2. Gestión de Vehículos");
    Console.WriteLine("3. Gestión de Inventario");
    Console.WriteLine("4. Resumen del Sistema");
    Console.WriteLine("5. Salir");
    Console.WriteLine();
    Console.Write("Seleccione una opción: ");
}

static async Task GestionarClientes(ServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var clienteService = scope.ServiceProvider.GetRequiredService<IClienteService>();
    
    Console.Clear();
    Console.WriteLine("=== GESTIÓN DE CLIENTES ===");
    Console.WriteLine("1. Listar todos los clientes");
    Console.WriteLine("2. Crear nuevo cliente");
    Console.WriteLine("3. Buscar cliente por nombre");
    Console.WriteLine("4. Volver al menú principal");
    Console.Write("Seleccione una opción: ");
    
    var opcion = Console.ReadLine();
    
    switch (opcion)
    {
        case "1":
            var clientes = await clienteService.GetAllAsync();
            Console.WriteLine("\n--- LISTA DE CLIENTES ---");
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.Id} | Nombre: {cliente.Nombre} | Teléfono: {cliente.Telefono ?? "N/A"} | Vehículos: {cliente.Vehiculos.Count}");
            }
            break;
            
        case "2":
            Console.Write("Nombre del cliente: ");
            var nombre = Console.ReadLine();
            Console.Write("Teléfono (opcional): ");
            var telefono = Console.ReadLine();
            Console.Write("Correo (opcional): ");
            var correo = Console.ReadLine();
            
            if (!string.IsNullOrEmpty(nombre))
            {
                try
                {
                    var nuevoCliente = new Cliente
                    {
                        Nombre = nombre,
                        Telefono = string.IsNullOrEmpty(telefono) ? null : telefono,
                        Correo = string.IsNullOrEmpty(correo) ? null : correo
                    };
                    
                    await clienteService.CreateAsync(nuevoCliente);
                    Console.WriteLine("✓ Cliente creado exitosamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            break;
            
        case "3":
            Console.Write("Nombre a buscar: ");
            var nombreBuscar = Console.ReadLine();
            if (!string.IsNullOrEmpty(nombreBuscar))
            {
                var clientesEncontrados = await clienteService.BuscarPorNombreAsync(nombreBuscar);
                Console.WriteLine($"\n--- RESULTADOS ({clientesEncontrados.Count()}) ---");
                foreach (var cliente in clientesEncontrados)
                {
                    Console.WriteLine($"ID: {cliente.Id} | Nombre: {cliente.Nombre} | Teléfono: {cliente.Telefono ?? "N/A"}");
                }
            }
            break;
    }
    
    if (opcion != "4")
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }
}

static async Task GestionarVehiculos(ServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var vehiculoService = scope.ServiceProvider.GetRequiredService<IVehiculoService>();
    var clienteService = scope.ServiceProvider.GetRequiredService<IClienteService>();
    
    Console.Clear();
    Console.WriteLine("=== GESTIÓN DE VEHÍCULOS ===");
    Console.WriteLine("1. Listar todos los vehículos");
    Console.WriteLine("2. Crear nuevo vehículo");
    Console.WriteLine("3. Buscar por placa");
    Console.WriteLine("4. Volver al menú principal");
    Console.Write("Seleccione una opción: ");
    
    var opcion = Console.ReadLine();
    
    switch (opcion)
    {
        case "1":
            var vehiculos = await vehiculoService.GetAllAsync();
            Console.WriteLine("\n--- LISTA DE VEHÍCULOS ---");
            foreach (var vehiculo in vehiculos)
            {
                Console.WriteLine($"Placa: {vehiculo.Placa} | {vehiculo.Marca} {vehiculo.Modelo} ({vehiculo.Año}) | Cliente: {vehiculo.Cliente.Nombre}");
            }
            break;
            
        case "2":
            // Primero mostrar clientes disponibles
            var clientesDisponibles = await clienteService.GetAllAsync();
            if (!clientesDisponibles.Any())
            {
                Console.WriteLine("No hay clientes registrados. Registre un cliente primero.");
                break;
            }
            
            Console.WriteLine("\n--- CLIENTES DISPONIBLES ---");
            foreach (var cliente in clientesDisponibles)
            {
                Console.WriteLine($"ID: {cliente.Id} | Nombre: {cliente.Nombre}");
            }
            
            Console.Write("\nID del cliente: ");
            if (int.TryParse(Console.ReadLine(), out int clienteId))
            {
                Console.Write("Marca: ");
                var marca = Console.ReadLine();
                Console.Write("Modelo: ");
                var modelo = Console.ReadLine();
                Console.Write("Año: ");
                var añoStr = Console.ReadLine();
                Console.Write("Placa: ");
                var placa = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(marca) && !string.IsNullOrEmpty(modelo) && 
                    int.TryParse(añoStr, out int año) && !string.IsNullOrEmpty(placa))
                {
                    try
                    {
                        var nuevoVehiculo = new Vehiculo
                        {
                            ClienteId = clienteId,
                            Marca = marca,
                            Modelo = modelo,
                            Año = año,
                            Placa = placa.ToUpper()
                        };
                        
                        await vehiculoService.CreateAsync(nuevoVehiculo);
                        Console.WriteLine("✓ Vehículo registrado exitosamente");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            break;
            
        case "3":
            Console.Write("Placa a buscar: ");
            var placaBuscar = Console.ReadLine();
            if (!string.IsNullOrEmpty(placaBuscar))
            {
                var vehiculoEncontrado = await vehiculoService.BuscarPorPlacaAsync(placaBuscar);
                if (vehiculoEncontrado != null)
                {
                    Console.WriteLine($"\n--- VEHÍCULO ENCONTRADO ---");
                    Console.WriteLine($"Placa: {vehiculoEncontrado.Placa}");
                    Console.WriteLine($"Vehículo: {vehiculoEncontrado.Marca} {vehiculoEncontrado.Modelo} ({vehiculoEncontrado.Año})");
                    Console.WriteLine($"Cliente: {vehiculoEncontrado.Cliente.Nombre}");
                    Console.WriteLine($"Teléfono Cliente: {vehiculoEncontrado.Cliente.Telefono ?? "N/A"}");
                    Console.WriteLine($"Reparaciones: {vehiculoEncontrado.Reparaciones.Count}");
                }
                else
                {
                    Console.WriteLine("Vehículo no encontrado");
                }
            }
            break;
    }
    
    if (opcion != "4")
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }
}

static async Task GestionarInventario(ServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var productoService = scope.ServiceProvider.GetRequiredService<IProductoService>();
    
    Console.Clear();
    Console.WriteLine("=== GESTIÓN DE INVENTARIO ===");
    Console.WriteLine("1. Listar todos los productos");
    Console.WriteLine("2. Productos con bajo stock");
    Console.WriteLine("3. Buscar producto");
    Console.WriteLine("4. Actualizar stock");
    Console.WriteLine("5. Volver al menú principal");
    Console.Write("Seleccione una opción: ");
    
    var opcion = Console.ReadLine();
    
    switch (opcion)
    {
        case "1":
            var productos = await productoService.GetAllAsync();
            Console.WriteLine("\n--- INVENTARIO COMPLETO ---");
            foreach (var producto in productos)
            {
                var indicadorStock = producto.BajoStock ? "⚠️ " : "";
                Console.WriteLine($"{indicadorStock}ID: {producto.Id} | {producto.Nombre} | Categoría: {producto.Categoria} | Stock: {producto.Stock} | Precio: ${producto.PrecioVenta:F2}");
            }
            break;
            
        case "2":
            var productosBajoStock = await productoService.GetProductosBajoStockAsync();
            Console.WriteLine("\n--- PRODUCTOS CON BAJO STOCK ---");
            if (productosBajoStock.Any())
            {
                foreach (var producto in productosBajoStock)
                {
                    Console.WriteLine($"⚠️  {producto.Nombre} | Stock: {producto.Stock} | Mínimo: {producto.StockMinimo}");
                }
            }
            else
            {
                Console.WriteLine("✓ Todos los productos tienen stock suficiente");
            }
            break;
            
        case "3":
            Console.Write("Nombre del producto a buscar: ");
            var nombreBuscar = Console.ReadLine();
            if (!string.IsNullOrEmpty(nombreBuscar))
            {
                var criterios = new TallerMecanicoApp.Business.Models.BusquedaProducto { Nombre = nombreBuscar };
                var productosEncontrados = await productoService.BuscarAsync(criterios);
                Console.WriteLine($"\n--- RESULTADOS ({productosEncontrados.Count()}) ---");
                foreach (var producto in productosEncontrados)
                {
                    Console.WriteLine($"ID: {producto.Id} | {producto.Nombre} | Stock: {producto.Stock} | Precio: ${producto.PrecioVenta:F2}");
                }
            }
            break;
            
        case "4":
            Console.Write("ID del producto: ");
            if (int.TryParse(Console.ReadLine(), out int productoId))
            {
                var producto = await productoService.GetByIdAsync(productoId);
                if (producto != null)
                {
                    Console.WriteLine($"Producto: {producto.Nombre} | Stock actual: {producto.Stock}");
                    Console.Write("Nuevo stock: ");
                    if (int.TryParse(Console.ReadLine(), out int nuevoStock))
                    {
                        try
                        {
                            await productoService.ActualizarStockAsync(productoId, nuevoStock);
                            Console.WriteLine("✓ Stock actualizado exitosamente");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Producto no encontrado");
                }
            }
            break;
    }
    
    if (opcion != "5")
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }
}

static async Task MostrarResumen(ServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TallerMecanicoContext>();
    
    Console.Clear();
    Console.WriteLine("=== RESUMEN DEL SISTEMA ===");
    
    var totalClientes = await context.Clientes.CountAsync();
    var totalVehiculos = await context.Vehiculos.CountAsync();
    var totalProductos = await context.Productos.CountAsync();
    var productosBajoStock = await context.Productos.CountAsync(p => p.Stock <= p.StockMinimo);
    var totalVentas = await context.Ventas.CountAsync();
    
    Console.WriteLine($"📊 Clientes registrados: {totalClientes}");
    Console.WriteLine($"🚗 Vehículos registrados: {totalVehiculos}");
    Console.WriteLine($"📦 Productos en inventario: {totalProductos}");
    Console.WriteLine($"⚠️  Productos con bajo stock: {productosBajoStock}");
    Console.WriteLine($"💰 Ventas registradas: {totalVentas}");
    
    if (productosBajoStock > 0)
    {
        Console.WriteLine("\n--- PRODUCTOS QUE REQUIEREN RESTOCK ---");
        var productos = await context.Productos.Where(p => p.Stock <= p.StockMinimo).ToListAsync();
        foreach (var producto in productos)
        {
            Console.WriteLine($"• {producto.Nombre} (Stock: {producto.Stock}, Mínimo: {producto.StockMinimo})");
        }
    }
    
    Console.WriteLine("\n✅ Sistema funcionando correctamente");
    Console.WriteLine("\nPresione Enter para continuar...");
    Console.ReadLine();
}
