using System.ComponentModel.DataAnnotations;

namespace TallerMecanicoApp.Data.Entities;

public class Proveedor
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? Contacto { get; set; }
    
    [MaxLength(20)]
    public string? Telefono { get; set; }
    
    [MaxLength(100)]
    public string? Correo { get; set; }
    
    [MaxLength(200)]
    public string? Direccion { get; set; }
    
    public int TiempoEntregaDias { get; set; } = 7;
    
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}