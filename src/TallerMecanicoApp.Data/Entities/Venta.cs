using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanicoApp.Data.Entities;

public class Venta
{
    public int Id { get; set; }
    
    public DateTime Fecha { get; set; } = DateTime.Now;
    
    [MaxLength(20)]
    public string NumeroFactura { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Impuesto { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    
    [MaxLength(20)]
    public string TipoPago { get; set; } = "Efectivo"; // Efectivo, Tarjeta, Transferencia
    
    [MaxLength(20)]
    public string Estado { get; set; } = "Completada"; // Cotización, Completada, Cancelada
    
    // Foreign Key
    public int? ClienteId { get; set; }
    
    // Navegación
    public virtual Cliente? Cliente { get; set; }
    public virtual ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
}