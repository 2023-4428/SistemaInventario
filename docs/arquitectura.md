# Arquitectura y decisiones de diseño

Este documento relaciona cada requisito de la guía del Proyecto 1 con el lugar exacto del código donde se cumple.
Los diagramas están en [diagrama-clases.md](diagrama-clases.md) y [diagrama-er.md](diagrama-er.md).

## 1. Arquitectura por capas

| Proyecto | Tipo | Contenido | Referencia a |
|---|---|---|---|
| `SistemaInventario.Entidades` | Class Library | Modelos de dominio (`EntidadBase`, `Categoria`, `FotoCategoria`, `Suplidor`, `Producto`, `Factura`, `FacturaDetalle`) y registros de resultado de los reportes | — |
| `SistemaInventario.Comun` *(proyecto adicional)* | Class Library | `ILogger`/`Logger` (archivo `errores.log`) y las excepciones compartidas | — |
| `SistemaInventario.AccesoDatos` | Class Library | Repositorios genéricos, Singleton de conexión (ADO.NET con `Microsoft.Data.SqlClient`) | Entidades, Comun |
| `SistemaInventario.LogicaNegocio` | Class Library | Servicios, validaciones, consultas LINQ y raíz de composición | AccesoDatos, Entidades, Comun |
| `SistemaInventario.Presentacion` | Windows Forms App | Pantallas, diálogos y controladores de listado y papelera | **LogicaNegocio**, Entidades, Comun |
| `SistemaInventario.Pruebas` *(adicional)* | xUnit | Pruebas unitarias y de arquitectura | todas |

**La capa de Presentación no referencia a Acceso a Datos**: su `.csproj` no contiene esa referencia y ninguna clase de la UI usa el espacio de
nombres `SistemaInventario.AccesoDatos`. Para que la UI pueda arrancar sin conocer los repositorios, la *raíz de composición*
(`LogicaNegocio/Composicion/ContenedorServicios.cs`) vive en la capa de negocio: arma los repositorios, validadores y servicios y solo expone
las interfaces de servicio. Esta regla la vigilan pruebas automáticas (`ArquitecturaPruebas.cs`).

> El proyecto `Comun` se agregó porque el `Logger` y las excepciones deben usarse desde todas las capas, incluida la de datos, que no puede depender de la de negocio.
> La guía exige *mínimo* 4 proyectos, así que un quinto no la contradice.

## 2. Patrones de diseño obligatorios

| Requisito | Dónde está |
|---|---|
| **Repository** con interfaz genérica `IRepositorio<T>` | `AccesoDatos/Interfaces/IRepositorio.cs` (contrato) y `AccesoDatos/Repositorios/RepositorioBase.cs` (implementación genérica del CRUD y del borrado lógico). Los repositorios concretos (`CategoriaRepositorio`, `SuplidorRepositorio`, `ProductoRepositorio`, `FacturaRepositorio`) solo aportan su SQL, el mapeo de filas y, si tienen tablas dependientes (fotos, líneas de factura), el gancho `EliminarDependientesEn`. |
| **Singleton** para la conexión | `AccesoDatos/ConexionBD.cs`: constructor privado, instancia única perezosa y segura entre hilos (`Lazy<T>`), cadena de conexión centralizada. Devuelve conexiones de vida corta que ADO.NET reutiliza con su *pool*; compartir una única conexión física daría errores con lectores abiertos y con varios hilos. |
| **Inyección de dependencias por constructor** entre capas | Todos los repositorios, servicios, validadores y pantallas reciben sus dependencias por constructor (por interfaz). El grafo se arma en `ContenedorServicios`; las pruebas lo reemplazan con repositorios en memoria. |

Patrones adicionales: *Template Method* (`RepositorioBase<T>`, `ServicioBase<T>`: los pasos que varían son métodos virtuales como `Normalizar`, `ValidarReglasDeNegocio`, `AntesDeEliminar`, `AntesDeRestaurar` y `AntesDeEliminarDefinitivo`), *Null Object* (`LoggerNulo`), *Composition Root* (`ContenedorServicios`). En la interfaz, la lógica común de los listados y de la Papelera no se hereda: se reutiliza por composición con `ControladorListado<T>` y `ControladorPapelera<T>`.

## 3. Genéricos

- `IRepositorio<T>` y `RepositorioBase<T>` — CRUD genérico (requisito).
- `IServicio<T>` y `ServicioBase<T>` — flujo común de validar, guardar, eliminar y restaurar.
- `IValidador<T>` — contrato genérico de validación.
- `ExtensionesBusqueda.Buscar<T>(...)` — **método genérico propio** de búsqueda parcial reutilizado por todos los listados.
- `RepositorioBase<T>.Ejecutar<TResultado>(...)` — punto único de acceso a la base de datos con `try/catch`.
- Presentación: `ControladorListado<T>` y `ControladorPapelera<T>` (ambos con la restricción `where T : EntidadBase`, usados por todas las pantallas) y `ListaOrdenable<T>` (lista que permite ordenar columnas).

## 4. Consultas LINQ (mínimo 5 no triviales)

| # | Consulta | Dónde | Operadores principales | Pantalla |
|---|---|---|---|---|
| 1 | Productos agrupados por categoría con conteo | `ReporteServicio.ProductosAgrupadosPorCategoria` | `join … into` (group join), `let`, `Count`, `orderby` | Reportes |
| 2 | Top 5 suplidores con más productos | `ReporteServicio.TopSuplidoresPorProductos` | `GroupJoin`, `OrderByDescending`, `ThenBy`, `Take`, `Select` con índice | Reportes |
| 3 | Productos en un rango de precios filtrados por categoría | `ReporteServicio.ProductosPorRangoDePrecio` | `Where` acumulativo, `OrderBy`, `ThenBy` | Reportes |
| 4 | Búsqueda parcial ignorando mayúsculas (y acentos) | `ExtensionesBusqueda.Buscar<T>` | `Where`, `All`, `Any`, `Select` | Todos los listados |
| 5 | Promedio de precios por categoría | `ReporteServicio.PromedioPreciosPorCategoria` | `GroupBy` con clave compuesta, `Average`, `Min`, `Max` | Reportes |
| 6 | Producto más caro de cada categoría | `ReporteServicio.ProductoMasCaroPorCategoria` | `GroupBy`, `OrderByDescending`, `First` | Reportes |
| 7 | Categorías sin productos | `ReporteServicio.CategoriasSinProductos` | `GroupJoin`, `Where` (equivale a LEFT JOIN … IS NULL) | Reportes |
| 8 | Filtro combinado de productos | `ProductoServicio.Filtrar` | `Where` encadenados + búsqueda | Productos |

## 5. Validaciones (capa de Lógica de Negocio) y su despliegue en la UI

| Campo | Regla | Implementación |
|---|---|---|
| Email | formato válido (regex) | `PatronesValidacion.Correo` → `ValidadorSuplidor` |
| Teléfono | `^(809\|829\|849)-\d{3}-\d{4}$` | `PatronesValidacion.Telefono` → `ValidadorSuplidor` |
| Sitio web | URL http/https válida, solo si se proporciona | `PatronesValidacion.EsUrlValida` → `ValidadorSuplidor` |
| Precio | mayor que 0 y máximo 2 decimales (tope 99 999 999.99) | `ValidadorProducto` |
| Foto | `.jpg`, `.jpeg`, `.png`; máximo 2 MB; además se comprueba la firma real del archivo | `ValidadorFoto` |
| Nombres | no vacíos y dentro de la longitud permitida (categoría 3–100, descripción ≤ 500) | `ValidadorCategoria`, `ValidadorSuplidor`, `ValidadorProducto` |
| Cliente de la factura | nombre de 2 a 150 caracteres; cédula (11 dígitos) o RNC (9 dígitos) opcional, se aceptan guiones | `ValidadorFactura`, `PatronesValidacion.EsCedulaORncValido` |
| Líneas de factura | al menos una y máximo 50; cantidad de 1 a 9999; sin productos repetidos; los productos deben existir y estar activos | `ValidadorFactura`, `FacturaServicio` |
| Unicidad | nombre de categoría y de empresa únicos; producto único por nombre + categoría + suplidor; todo incluso frente a la Papelera | `CategoriaServicio`, `SuplidorServicio`, `ProductoServicio` (+ índices `UNIQUE` en la base de datos) |

Los errores viajan como `ValidacionException` con la lista (campo, mensaje) y la interfaz los muestra junto a cada control con `ErrorProvider`
(`UiHelper.MostrarErrores`). La base de datos repite las reglas básicas con restricciones `CHECK` como segunda línea de defensa.

## 6. Manejo de excepciones y registro

1. Toda llamada a la base de datos pasa por `RepositorioBase<T>.Ejecutar` (o `EjecutarEnTransaccion` cuando hay varias tablas, como al guardar una factura con sus líneas o al eliminar definitivamente), que captura `SqlException`, escribe el detalle en el log y la convierte en
   `ErrorBaseDatosException` con un mensaje comprensible (servidor caído, base de datos inexistente, tiempo agotado, duplicado, integridad referencial…).
2. `Logger` (en `Comun`) escribe cada error en `errores.log` junto al ejecutable, con fecha, nivel, mensaje y traza; nunca lanza excepciones (si no puede escribir, usa una ruta alterna).
3. La interfaz usa `UiHelper.Ejecutar`, que traduce `ValidacionException`, `ReglaNegocioException`, `EntidadNoEncontradaException` y `ErrorBaseDatosException` en mensajes amables.
4. `Program.cs` registra además cualquier excepción no controlada (hilo de interfaz y dominio de aplicación).

## 7. Borrado lógico, Papelera y eliminación definitiva

- Las tablas `Categoria`, `FotoCategoria`, `Suplidor`, `Producto` y `Factura` tienen `Activo BIT`; los listados solo consultan `Activo = 1`.
  `FacturaDetalle` (las líneas) no tiene `Activo`: pertenece a su factura y se borra con ella.
- **Eliminar** cambia `Activo` a 0 y actualiza `FechaModificacion`. En una factura equivale a anularla. Nada se borra físicamente.
- No se puede eliminar una categoría ni un suplidor con productos **activos** (`AntesDeEliminar` en `CategoriaServicio` / `SuplidorServicio`).
- **Papelera** (`UcPapelera`): una pestaña por tipo (categorías, suplidores, productos y facturas) con búsqueda y los botones *Restaurar* y *Eliminar definitivamente*.
- Restaurar un producto exige que su categoría y su suplidor estén activos; si no, el mensaje indica qué restaurar primero (`ProductoServicio.AntesDeRestaurar`).
- **Eliminar definitivamente** (`ServicioBase<T>.EliminarDefinitivo`) borra la fila de la base de datos y solo se permite si el registro ya está en la Papelera.
  Cada servicio añade su regla en `AntesDeEliminarDefinitivo`: una categoría o un suplidor no pueden tener productos (activos o en la Papelera) y un producto no puede aparecer en líneas de factura, para conservar el historial de ventas.
  Las fotos de una categoría y las líneas de una factura se borran junto a su registro principal, dentro de una transacción.
- Un registro eliminado no se puede modificar hasta restaurarlo.

## Facturación

- `Factura` (cliente, cédula/RNC opcional, subtotal, ITBIS y total) tiene de 1 a 50 líneas `FacturaDetalle` (producto, cantidad, precio y subtotal de la línea).
- El ITBIS es el 18 % del subtotal, redondeado a 2 decimales (`Factura.RecalcularTotales`); el número (`F-00000001`) lo calcula la base de datos a partir del `Id`.
- Al crear una factura, `FacturaServicio` toma el nombre y el precio del producto vigente y recalcula los totales; no confía en lo que envíe la pantalla.
  La línea conserva el precio con el que se facturó aunque el producto cambie después.
- La base de datos repite las reglas con restricciones `CHECK` (totales, cantidad, precio y subtotal de línea) y una restricción `UNIQUE` para que un producto no se repita dentro de la misma factura.

## 8. Convenciones de código

- PascalCase para clases, métodos y propiedades; camelCase para variables locales, parámetros y campos privados; prefijo `I` en interfaces; un archivo por clase;
  documentación XML (`///`) en los miembros públicos. El archivo `.editorconfig` define las reglas de nombres y se comprobó con los analizadores de Roslyn (`IDE1006`) sin violaciones.
- Los proyectos de biblioteca generan su documentación XML y compilan sin advertencias.

## 9. Pruebas

El proyecto `SistemaInventario.Pruebas` (xUnit) usa repositorios en memoria para probar validadores, servicios, reglas de negocio, reportes LINQ, el `Logger`
y las reglas de arquitectura. Se ejecuta con `dotnet test src/SistemaInventario.sln` o desde el Explorador de pruebas de Visual Studio.

## 10. Decisiones de diseño

- **ADO.NET puro** (`SqlConnection`/`SqlCommand`) en lugar de un ORM: la guía pide `SqlException`, conexión por Singleton y repositorios propios.
- **Fotos dentro de la base de datos** (`VARBINARY(MAX)`): la aplicación y sus datos de prueba son portátiles, sin rutas de archivo que se rompan al cambiar de equipo.
- **Filtrado en memoria**: los listados cargan los registros activos una vez y filtran con LINQ mientras el usuario escribe (sin consultar la base de datos en cada tecla).
- **Interfaz con el diseñador de Visual Studio**: cada formulario y pantalla tiene su `.Designer.cs`, así que se edita visualmente. Solo los botones de navegación y las columnas de Reportes se crean en código porque dependen de los datos. La lógica repetida de los listados vive en `ControladorListado<T>` y `ControladorPapelera<T>`.
