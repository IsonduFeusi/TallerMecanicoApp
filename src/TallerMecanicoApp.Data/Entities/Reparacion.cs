using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanicoApp.Data.Entities;

public class Reparacion
{
    public int Id { get; set; }
    
    public DateTime Fecha { get; set; } = DateTime.Now;
    
    [Required]
    [MaxLength(100)]
    public string TipoServicio { get; set; } = string.Empty; // Mecánico, Eléctrico, Mantenimiento
    
    [Required]
    public string Descripcion { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal CostoPiezas { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal CostoManoObra { get; set; }
    
    [MaxLength(20)]
    public string Estado { get; set; } = "Pendiente"; // Pendiente, En Proceso, Completado
    
    // Foreign Key
    public int VehiculoId { get; set; }
    
    // Navegación
    public virtual Vehiculo Vehiculo { get; set; } = null!;
    
    // Propiedad calculada
    [NotMapped]
    public decimal CostoTotal => CostoPiezas + CostoManoObra;
}