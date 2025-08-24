using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanicoApp.Data.Entities;

public class Producto
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string? Marca { get; set; }
    
    [MaxLength(50)]
    public string? Modelo { get; set; }
    
    [MaxLength(50)]
    public string? Medidas { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Categoria { get; set; } = string.Empty; // Llantas, Aros, Aceites, Filtros, Accesorios
    
    public int Stock { get; set; }
    
    public int StockMinimo { get; set; } = 5;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecioVenta { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal CostoCompra { get; set; }
    
    [MaxLength(100)]
    public string? Proveedor { get; set; }
    
    [MaxLength(50)]
    public string? CodigoBarras { get; set; }
    
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    
    // Navegación
    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
    
    // Propiedad calculada
    [NotMapped]
    public decimal MargenGanancia => PrecioVenta > 0 ? ((PrecioVenta - CostoCompra) / PrecioVenta) * 100 : 0;
    
    [NotMapped]
    public bool BajoStock => Stock <= StockMinimo;
}