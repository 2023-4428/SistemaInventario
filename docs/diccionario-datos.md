# Diccionario de datos

Basado en `database/schema.sql` (la fuente de verdad del esquema). Incluye las seis tablas: `Categoria`, `Suplidor`, `Producto`, `FotoCategoria`, `Factura` y `FacturaDetalle`.

## dbo.Categoria

| Columna | Tipo | ¿Nulo? | Clave | Descripción |
|---|---|---|---|---|
| `Id` | `INT IDENTITY` | No | PK | Identificador único autogenerado. |
| `Nombre` | `NVARCHAR(100)` | No | UNIQUE | Nombre (obligatorio, único en Categoria). |
| `Descripcion` | `NVARCHAR(500)` | No |  | Descripción de la categoría. |
| `Activo` | `BIT` | No |  | Borrado lógico: 1 = activo, 0 = eliminado (aparece en la Papelera). Valor por defecto: `1`. |
| `FechaCreacion` | `DATETIME2(0)` | No |  | Fecha y hora de creación del registro. Valor por defecto: `SYSDATETIME()`. |
| `FechaModificacion` | `DATETIME2(0)` | No |  | Fecha y hora de la última modificación, eliminación o restauración. Valor por defecto: `SYSDATETIME()`. |

**Restricciones**

- `UQ_Categoria_Nombre` — UNIQUE (Nombre)
- `CK_Categoria_Nombre` — CHECK (LEN(LTRIM(RTRIM(Nombre))) >= 3)
- `CK_Categoria_Descripcion` — CHECK (LEN(LTRIM(RTRIM(Descripcion))) >= 1)

## dbo.Suplidor

| Columna | Tipo | ¿Nulo? | Clave | Descripción |
|---|---|---|---|---|
| `Id` | `INT IDENTITY` | No | PK | Identificador único autogenerado. |
| `NombreEmpresa` | `NVARCHAR(150)` | No | UNIQUE | Nombre de la empresa (obligatorio, único). |
| `NombreContacto` | `NVARCHAR(100)` | No |  | Persona de contacto. |
| `Telefono` | `NVARCHAR(12)` | No |  | Teléfono dominicano 809/829/849-XXX-XXXX. |
| `Correo` | `NVARCHAR(150)` | No |  | Correo electrónico. |
| `SitioWeb` | `NVARCHAR(250)` | Sí |  | Sitio web opcional (http/https). |
| `Activo` | `BIT` | No |  | Borrado lógico: 1 = activo, 0 = eliminado (aparece en la Papelera). Valor por defecto: `1`. |
| `FechaCreacion` | `DATETIME2(0)` | No |  | Fecha y hora de creación del registro. Valor por defecto: `SYSDATETIME()`. |
| `FechaModificacion` | `DATETIME2(0)` | No |  | Fecha y hora de la última modificación, eliminación o restauración. Valor por defecto: `SYSDATETIME()`. |

**Restricciones**

- `UQ_Suplidor_NombreEmpresa` — UNIQUE (NombreEmpresa)
- `CK_Suplidor_Telefono` — CHECK (Telefono LIKE N'8[024]9-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]')
- `CK_Suplidor_Correo` — CHECK (Correo LIKE N'_%@_%._%')
- `CK_Suplidor_SitioWeb` — CHECK (SitioWeb IS NULL OR SitioWeb LIKE N'http://%' OR SitioWeb LIKE N'https://%')

## dbo.Producto

| Columna | Tipo | ¿Nulo? | Clave | Descripción |
|---|---|---|---|---|
| `Id` | `INT IDENTITY` | No | PK | Identificador único autogenerado. |
| `Nombre` | `NVARCHAR(150)` | No |  | Nombre del producto (obligatorio; único junto con categoría y suplidor). |
| `PrecioUnitario` | `DECIMAL(18, 2)` | No |  | Precio mayor a 0 con 2 decimales. |
| `CategoriaId` | `INT` | No | FK → Categoria.Id | Categoría asociada. |
| `SuplidorId` | `INT` | No | FK → Suplidor.Id | Suplidor asociado. |
| `Activo` | `BIT` | No |  | Borrado lógico: 1 = activo, 0 = eliminado (aparece en la Papelera). Valor por defecto: `1`. |
| `FechaCreacion` | `DATETIME2(0)` | No |  | Fecha y hora de creación del registro. Valor por defecto: `SYSDATETIME()`. |
| `FechaModificacion` | `DATETIME2(0)` | No |  | Fecha y hora de la última modificación, eliminación o restauración. Valor por defecto: `SYSDATETIME()`. |

**Restricciones**

- `FK_Producto_Categoria` — FOREIGN KEY (CategoriaId) REFERENCES Categoria(Id)
- `FK_Producto_Suplidor` — FOREIGN KEY (SuplidorId) REFERENCES Suplidor(Id)
- `CK_Producto_Precio` — CHECK (PrecioUnitario > 0)
- `CK_Producto_Nombre` — CHECK (LEN(LTRIM(RTRIM(Nombre))) >= 2)
- `UQ_Producto_Nombre_Categoria_Suplidor` — índice UNIQUE (Nombre, CategoriaId, SuplidorId): no hay dos productos iguales, ni siquiera entre los de la Papelera

## dbo.FotoCategoria

| Columna | Tipo | ¿Nulo? | Clave | Descripción |
|---|---|---|---|---|
| `Id` | `INT IDENTITY` | No | PK | Identificador único autogenerado. |
| `CategoriaId` | `INT` | No | FK → Categoria.Id | Categoría asociada. |
| `NombreArchivo` | `NVARCHAR(260)` | No |  | Nombre original del archivo de imagen. |
| `Contenido` | `VARBINARY(MAX)` | No |  | Bytes de la imagen (máx. 2 MB). |
| `Activo` | `BIT` | No |  | Borrado lógico: 1 = activo, 0 = eliminado (aparece en la Papelera). Valor por defecto: `1`. |
| `FechaCreacion` | `DATETIME2(0)` | No |  | Fecha y hora de creación del registro. Valor por defecto: `SYSDATETIME()`. |
| `FechaModificacion` | `DATETIME2(0)` | No |  | Fecha y hora de la última modificación, eliminación o restauración. Valor por defecto: `SYSDATETIME()`. |

**Restricciones**

- `FK_FotoCategoria_Categoria` — FOREIGN KEY (CategoriaId) REFERENCES Categoria(Id)
- `CK_FotoCategoria_Tamano` — CHECK (DATALENGTH(Contenido) > 0 AND DATALENGTH(Contenido) <= 2097152)
- `CK_FotoCategoria_Extension` — CHECK (LOWER(NombreArchivo) LIKE N'%.jpg' OR LOWER(NombreArchivo) LIKE N'%.jpeg' OR LOWER(NombreArchivo) LIKE N'%.png')

## dbo.Factura

| Columna | Tipo | ¿Nulo? | Clave | Descripción |
|---|---|---|---|---|
| `Id` | `INT IDENTITY` | No | PK | Identificador único autogenerado. |
| `Numero` | `NVARCHAR` (calculada, persistida) | No | UNIQUE | Número de factura con formato `F-00000001`, generado a partir del `Id`. |
| `ClienteNombre` | `NVARCHAR(150)` | No |  | Nombre del cliente. |
| `ClienteDocumento` | `NVARCHAR(15)` | Sí |  | Cédula (11 dígitos) o RNC (9 dígitos) del cliente, opcional. |
| `Subtotal` | `DECIMAL(18, 2)` | No |  | Suma de los subtotales de las líneas. |
| `Itbis` | `DECIMAL(18, 2)` | No |  | ITBIS (18 % del subtotal). |
| `Total` | `DECIMAL(18, 2)` | No |  | Subtotal + ITBIS. |
| `Activo` | `BIT` | No |  | Borrado lógico: 1 = vigente, 0 = anulada (aparece en la Papelera). Valor por defecto: `1`. |
| `FechaCreacion` | `DATETIME2(0)` | No |  | Fecha y hora de creación del registro. Valor por defecto: `SYSDATETIME()`. |
| `FechaModificacion` | `DATETIME2(0)` | No |  | Fecha y hora de la última modificación, anulación o restauración. Valor por defecto: `SYSDATETIME()`. |

**Restricciones**

- `UQ_Factura_Numero` — UNIQUE (Numero)
- `CK_Factura_Cliente` — CHECK (LEN(LTRIM(RTRIM(ClienteNombre))) >= 2)
- `CK_Factura_Totales` — CHECK (Subtotal >= 0 AND Itbis >= 0 AND Total = Subtotal + Itbis)

## dbo.FacturaDetalle

| Columna | Tipo | ¿Nulo? | Clave | Descripción |
|---|---|---|---|---|
| `Id` | `INT IDENTITY` | No | PK | Identificador único autogenerado. |
| `FacturaId` | `INT` | No | FK → Factura.Id | Factura a la que pertenece la línea. |
| `ProductoId` | `INT` | No | FK → Producto.Id | Producto facturado. |
| `Cantidad` | `INT` | No |  | Unidades facturadas (1 a 9999). |
| `PrecioUnitario` | `DECIMAL(18, 2)` | No |  | Precio con el que se facturó el producto. |
| `Subtotal` | `DECIMAL(18, 2)` | No |  | Cantidad × precio unitario. |

Esta tabla no tiene `Activo` ni fechas: las líneas se borran junto con su factura cuando esta se elimina definitivamente.

**Restricciones**

- `FK_FacturaDetalle_Factura` — FOREIGN KEY (FacturaId) REFERENCES Factura(Id)
- `FK_FacturaDetalle_Producto` — FOREIGN KEY (ProductoId) REFERENCES Producto(Id)
- `UQ_FacturaDetalle_Factura_Producto` — UNIQUE (FacturaId, ProductoId)
- `CK_FacturaDetalle_Cantidad` — CHECK (Cantidad BETWEEN 1 AND 9999)
- `CK_FacturaDetalle_Precio` — CHECK (PrecioUnitario > 0)
- `CK_FacturaDetalle_Subtotal` — CHECK (Subtotal = Cantidad * PrecioUnitario)
