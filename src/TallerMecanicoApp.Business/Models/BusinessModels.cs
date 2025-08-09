namespace TallerMecanicoApp.Business.Models;

public class BusquedaVehiculo
{
    public string? Placa { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int? Año { get; set; }
    public string? NombreCliente { get; set; }
}

public class BusquedaProducto
{
    public string? Nombre { get; set; }
    public string? Categoria { get; set; }
    public string? Marca { get; set; }
    public bool? SoloBajoStock { get; set; }
}

public class ResumenVentas
{
    public decimal VentasHoy { get; set; }
    public decimal VentasSemana { get; set; }
    public decimal VentasMes { get; set; }
    public int ProductosBajoStock { get; set; }
    public int ClientesRegistrados { get; set; }
    public int VehiculosRegistrados { get; set; }
}

public class ProductoMasVendido
{
    public int ProductoId { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public int CantidadVendida { get; set; }
    public decimal TotalVentas { get; set; }
}