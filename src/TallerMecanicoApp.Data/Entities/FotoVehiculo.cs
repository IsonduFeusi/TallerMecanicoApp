using System.ComponentModel.DataAnnotations;

namespace TallerMecanicoApp.Data.Entities;

public class FotoVehiculo
{
    public int Id { get; set; }
    
    [Required]
    public string RutaArchivo { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? Descripcion { get; set; }
    
    [MaxLength(20)]
    public string Tipo { get; set; } = "General"; // General, Antes, Después
    
    public DateTime FechaSubida { get; set; } = DateTime.Now;
    
    // Foreign Key
    public int VehiculoId { get; set; }
    
    // Navegación
    public virtual Vehiculo Vehiculo { get; set; } = null!;
}