using Microsoft.EntityFrameworkCore;
using TallerMecanicoApp.Data.Entities;

namespace TallerMecanicoApp.Data.Context;

public class TallerMecanicoContext : DbContext
{
    public TallerMecanicoContext(DbContextOptions<TallerMecanicoContext> options) : base(options)
    {
    }
    
    // DbSets
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Reparacion> Reparaciones { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<VentaDetalle> VentaDetalles { get; set; }
    public DbSet<FotoVehiculo> FotoVehiculos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configuración de relaciones
        modelBuilder.Entity<Vehiculo>()
            .HasOne(v => v.Cliente)
            .WithMany(c => c.Vehiculos)
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
            
        modelBuilder.Entity<Reparacion>()
            .HasOne(r => r.Vehiculo)
            .WithMany(v => v.Reparaciones)
            .HasForeignKey(r => r.VehiculoId)
            .OnDelete(DeleteBehavior.Restrict);
            
        modelBuilder.Entity<FotoVehiculo>()
            .HasOne(f => f.Vehiculo)
            .WithMany(v => v.Fotos)
            .HasForeignKey(f => f.VehiculoId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Cliente)
            .WithMany(c => c.Ventas)
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.SetNull);
            
        modelBuilder.Entity<VentaDetalle>()
            .HasOne(vd => vd.Venta)
            .WithMany(v => v.Detalles)
            .HasForeignKey(vd => vd.VentaId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<VentaDetalle>()
            .HasOne(vd => vd.Producto)
            .WithMany(p => p.VentaDetalles)
            .HasForeignKey(vd => vd.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configuración de índices
        modelBuilder.Entity<Vehiculo>()
            .HasIndex(v => v.Placa)
            .IsUnique();
            
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Telefono);
            
        modelBuilder.Entity<Producto>()
            .HasIndex(p => p.CodigoBarras)
            .IsUnique()
            .HasFilter("[CodigoBarras] IS NOT NULL");
        
        // Datos iniciales (seed data)
        SeedData(modelBuilder);
    }
    
    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Categorías de productos predefinidas
        modelBuilder.Entity<Producto>().HasData(
            new Producto { Id = 1, Nombre = "Llanta 185/65R15", Categoria = "Llantas", Stock = 20, StockMinimo = 5, PrecioVenta = 120.00m, CostoCompra = 80.00m },
            new Producto { Id = 2, Nombre = "Aro 15 pulgadas", Categoria = "Aros", Stock = 15, StockMinimo = 3, PrecioVenta = 200.00m, CostoCompra = 140.00m },
            new Producto { Id = 3, Nombre = "Aceite Motor 5W-30", Categoria = "Aceites", Stock = 50, StockMinimo = 10, PrecioVenta = 35.00m, CostoCompra = 25.00m },
            new Producto { Id = 4, Nombre = "Filtro de Aire", Categoria = "Filtros", Stock = 30, StockMinimo = 8, PrecioVenta = 15.00m, CostoCompra = 10.00m }
        );
        
        // Proveedor de ejemplo
        modelBuilder.Entity<Proveedor>().HasData(
            new Proveedor { Id = 1, Nombre = "Distribuidora Auto Partes", Contacto = "Juan Pérez", Telefono = "555-1234", Correo = "ventas@autopartes.com" }
        );
    }
}