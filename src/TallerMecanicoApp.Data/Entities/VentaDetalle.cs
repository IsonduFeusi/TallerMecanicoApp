using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanicoApp.Data.Entities;

public class VentaDetalle
{
    public int Id { get; set; }
    
    public int Cantidad { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecioUnitario { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
    
    // Foreign Keys
    public int VentaId { get; set; }
    public int ProductoId { get; set; }
    
    // Navegación
    public virtual Venta Venta { get; set; } = null!;
    public virtual Producto Producto { get; set; } = null!;
}