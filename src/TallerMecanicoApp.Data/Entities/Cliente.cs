using System.ComponentModel.DataAnnotations;

namespace TallerMecanicoApp.Data.Entities;

public class Cliente
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? Telefono { get; set; }
    
    [MaxLength(100)]
    public string? Correo { get; set; }
    
    [MaxLength(200)]
    public string? Direccion { get; set; }
    
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    
    // Navegación
    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
}