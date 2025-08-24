using System.ComponentModel.DataAnnotations;

namespace TallerMecanicoApp.Data.Entities;

public class Vehiculo
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Marca { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Modelo { get; set; } = string.Empty;
    
    public int Año { get; set; }
    
    [MaxLength(50)]
    public string? TipoMotor { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Placa { get; set; } = string.Empty;
    
    [MaxLength(30)]
    public string? Color { get; set; }
    
    public int? Kilometraje { get; set; }
    
    [MaxLength(100)]
    public string? NumeroChasis { get; set; }
    
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    
    // Foreign Key
    public int ClienteId { get; set; }
    
    // Navegación
    public virtual Cliente Cliente { get; set; } = null!;
    public virtual ICollection<Reparacion> Reparaciones { get; set; } = new List<Reparacion>();
    public virtual ICollection<FotoVehiculo> Fotos { get; set; } = new List<FotoVehiculo>();
}