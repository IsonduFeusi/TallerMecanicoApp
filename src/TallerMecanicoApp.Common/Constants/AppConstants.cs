namespace TallerMecanicoApp.Common.Constants;

public static class CategoriasProducto
{
    public const string Llantas = "Llantas";
    public const string Aros = "Aros";
    public const string Aceites = "Aceites";
    public const string Filtros = "Filtros";
    public const string Accesorios = "Accesorios";
    
    public static readonly string[] Todas = { Llantas, Aros, Aceites, Filtros, Accesorios };
}

public static class TiposServicio
{
    public const string Mecanico = "Mecánico";
    public const string Electrico = "Eléctrico";
    public const string Mantenimiento = "Mantenimiento";
    public const string Modificacion = "Modificación";
    
    public static readonly string[] Todos = { Mecanico, Electrico, Mantenimiento, Modificacion };
}

public static class EstadosReparacion
{
    public const string Pendiente = "Pendiente";
    public const string EnProceso = "En Proceso";
    public const string Completado = "Completado";
    
    public static readonly string[] Todos = { Pendiente, EnProceso, Completado };
}

public static class TiposPago
{
    public const string Efectivo = "Efectivo";
    public const string Tarjeta = "Tarjeta";
    public const string Transferencia = "Transferencia";
    
    public static readonly string[] Todos = { Efectivo, Tarjeta, Transferencia };
}