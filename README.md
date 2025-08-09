# TallerMecánicoApp - Sistema de Gestión para Taller Mecánico y Tienda de Llantas

## Descripción

**TallerMecánicoApp** es una aplicación de gestión completa desarrollada en .NET 8 para talleres mecánicos que también funcionan como tienda de llantas, aros y accesorios automotrices. El sistema está completamente en español y proporciona una interfaz intuitiva para la gestión integral del negocio.

## Características Principales

### ✅ Implementado
- **Base de datos SQLite** con Entity Framework Core
- **Arquitectura por capas** (UI, Business, Data, Common)  
- **Gestión de Clientes** - Registro completo con datos de contacto
- **Gestión de Vehículos** - Registro por placa con datos completos del vehículo
- **Gestión de Inventario** - Control de stock de productos por categorías
- **Sistema de búsqueda** - Búsqueda por múltiples criterios
- **Alertas de stock bajo** - Notificaciones automáticas
- **Panel de control** - Resumen estadístico del sistema
- **Validaciones de negocio** - Reglas de integridad de datos
- **Patrón Repository** - Acceso estructurado a datos

### 🚧 Funcionalidades Base Implementadas
- **Categorías de productos**: Llantas, Aros, Aceites, Filtros, Accesorios
- **Entidades principales**: Cliente, Vehículo, Producto, Proveedor, Reparación, Venta
- **Búsquedas avanzadas** con filtros múltiples
- **Cálculos automáticos** de márgenes de ganancia
- **Datos semilla** para testing inicial

## Arquitectura del Sistema

```
TallerMecanicoApp/
├── src/
│   ├── TallerMecanicoApp.UI/              # Interfaz de usuario (Console Demo)
│   ├── TallerMecanicoApp.Business/         # Lógica de negocio
│   ├── TallerMecanicoApp.Data/            # Acceso a datos (Entity Framework)
│   └── TallerMecanicoApp.Common/          # Clases compartidas
└── TallerMecanicoApp.sln                  # Solución principal
```

### Tecnologías Utilizadas
- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM para acceso a datos
- **SQLite** - Base de datos embebida
- **Patrón Repository** - Abstracción de acceso a datos
- **Inyección de Dependencias** - Microsoft.Extensions.DependencyInjection
- **iTextSharp** - Generación de PDFs (preparado para uso futuro)

## Entidades de Base de Datos

### Cliente
- Información personal y de contacto
- Relación con múltiples vehículos
- Historial de ventas

### Vehículo  
- Datos completos del vehículo (marca, modelo, año, placa, etc.)
- Relación con cliente propietario
- Historial de reparaciones
- Soporte para fotos (estructura preparada)

### Producto
- Categorización por tipo (Llantas, Aros, Aceites, etc.)
- Control de stock con alertas
- Precios de venta y costo de compra
- Cálculo automático de márgenes

### Reparación
- Registro de servicios realizados
- Costos de piezas y mano de obra
- Estados de seguimiento

### Venta y VentaDetalle
- Registro de transacciones
- Detalles de productos vendidos
- Diferentes métodos de pago

## Instalación y Ejecución

### Prerrequisitos
- .NET 8 SDK
- SQLite (incluido)

### Pasos de Instalación

1. **Clonar el repositorio**
```bash
git clone https://github.com/IsonduFeusi/TallerMecanicoApp.git
cd TallerMecanicoApp
```

2. **Restaurar paquetes NuGet**
```bash
dotnet restore
```

3. **Compilar la solución**
```bash
dotnet build
```

4. **Ejecutar la aplicación**
```bash
cd src/TallerMecanicoApp.UI
dotnet run
```

### Primera Ejecución
- La base de datos SQLite se crea automáticamente
- Se insertan datos de ejemplo (4 productos, 1 proveedor)
- La base de datos se guarda como `taller_mecanico.db`

## Uso del Sistema

### Menú Principal
1. **Gestión de Clientes** - Crear, buscar y listar clientes
2. **Gestión de Vehículos** - Registrar vehículos asociados a clientes  
3. **Gestión de Inventario** - Control de productos y stock
4. **Resumen del Sistema** - Dashboard con estadísticas
5. **Salir** - Cerrar aplicación

### Funcionalidades por Módulo

#### Clientes
- ✅ Crear nuevos clientes con validación
- ✅ Buscar por nombre o teléfono
- ✅ Listar todos los clientes
- ✅ Prevención de teléfonos duplicados

#### Vehículos  
- ✅ Registrar vehículos con datos completos
- ✅ Buscar por placa (único)
- ✅ Asociación automática con cliente
- ✅ Validación de placas duplicadas

#### Inventario
- ✅ Visualización por categorías  
- ✅ Alertas de stock bajo
- ✅ Búsqueda por nombre/marca
- ✅ Actualización de stock
- ✅ Cálculo de márgenes de ganancia

## Estructura de Datos

### Productos Iniciales
- **Llanta 185/65R15** - Stock: 20, Precio: $120.00
- **Aro 15 pulgadas** - Stock: 15, Precio: $200.00  
- **Aceite Motor 5W-30** - Stock: 50, Precio: $35.00
- **Filtro de Aire** - Stock: 30, Precio: $15.00

### Ejemplo de Uso
```
Cliente: Juan Pérez García (555-1234)
Vehículo: Toyota Corolla 2020 (Placa: ABC-123)
```

## Futuras Implementaciones

### Próximas Funcionalidades
- [ ] **Windows Forms UI** - Interfaz gráfica completa
- [ ] **Sistema de ventas** - Registro de transacciones
- [ ] **Generación de PDFs** - Facturas y reportes
- [ ] **Exportación a Excel** - Reportes de datos
- [ ] **Fotos de vehículos** - Subida y gestión de imágenes
- [ ] **Dashboard avanzado** - Gráficos y análisis
- [ ] **Gestión de proveedores** - CRUD completo
- [ ] **Sistema de respaldos** - Automatización de backups
- [ ] **Red local** - Acceso multi-usuario

### Mejoras Técnicas
- [ ] **Migraciones EF** - Versionado de base de datos  
- [ ] **Logging** - Sistema de auditoría
- [ ] **Validaciones avanzadas** - Reglas de negocio complejas
- [ ] **Tests unitarios** - Cobertura de código
- [ ] **Documentación API** - Swagger/OpenAPI

## Desarrollo

### Comandos Útiles

```bash
# Limpiar y reconstruir
dotnet clean && dotnet build

# Ejecutar desde directorio raíz
dotnet run --project src/TallerMecanicoApp.UI

# Crear migraciones (futuro)
dotnet ef migrations add InitialCreate --project src/TallerMecanicoApp.Data

# Actualizar base de datos (futuro)
dotnet ef database update --project src/TallerMecanicoApp.Data
```

### Estructura del Código

#### Capa de Datos (TallerMecanicoApp.Data)
- **Entities/** - Modelos de base de datos
- **Context/** - DbContext de Entity Framework  
- **Repositories/** - Implementación del patrón Repository

#### Capa de Negocio (TallerMecanicoApp.Business)
- **Services/** - Servicios con lógica de negocio
- **Models/** - DTOs y modelos de transferencia
- **Interfaces/** - Contratos de servicios

#### Capa de Interfaz (TallerMecanicoApp.UI)
- **Program.cs** - Aplicación console demo
- (Futuro: Formularios Windows Forms)

#### Capa Común (TallerMecanicoApp.Common)
- **Constants/** - Constantes del sistema
- **Helpers/** - Utilidades
- **Extensions/** - Métodos de extensión

## Contribución

Este proyecto está diseñado para ser una base sólida y escalable. Las contribuciones son bienvenidas siguiendo las mejores prácticas de desarrollo .NET.

## Licencia

Proyecto desarrollado para uso educativo y comercial.

---

**Estado del Proyecto**: ✅ Base funcional implementada  
**Última actualización**: Agosto 2024  
**Versión**: 1.0.0-beta