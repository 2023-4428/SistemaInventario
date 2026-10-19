Sistema de Inventario — Categorías, Suplidores y Productos

Proyecto 1 · ISW-124 Programación Avanzada · Individual


Requisitos previos
Necesita	Detalle
Windows 10 u 11	La aplicación usa Windows Forms.
SDK de .NET 10	Para compilar. Con Visual Studio, instale la carga de trabajo Desarrollo de escritorio de .NET. (Para ejecutar solo el .exe basta el .NET 10 Desktop Runtime.)
SQL Server o SQL Server Express	Por defecto se espera la instancia localhost\SQLEXPRESS con autenticación de Windows.
SQL Server Management Studio (opcional)	Para ejecutar los scripts; también sirve sqlcmd.


Instalación paso a paso
1. Obtener el código
bash
git clone (https://github.com/2023-4428/SistemaInventario.git)
cd SistemaInventario
2. Crear la base de datos

Ejecute los dos scripts de la carpeta database en este orden:

schema.sql — crea la base de datos SistemaInventario 
datos.sql — inserta los datos de prueba: 6 categorías, 6 suplidores y 24 productos activos (mínimo exigido: 5, 5 y 20), más algunos registros ya eliminados para probar la Papelera.

Con SSMS: Archivo → Abrir → Archivo…, conéctese a localhost\SQLEXPRESS y pulse Ejecutar (F5) en cada script. Con la línea de comandos:

bash
sqlcmd -S localhost\SQLEXPRESS -E -f 65001 -i database\schema.sql
sqlcmd -S localhost\SQLEXPRESS -E -f 65001 -i database\datos.sql


3. Configurar la conexión

Abra src/SistemaInventario.Presentacion/appsettings.json. Por defecto contiene:

json
{
  "ConnectionStrings": {
    "SistemaInventario": "Server=localhost\\SQLEXPRESS;Database=SistemaInventario;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

Si su servidor es otro, cambie la parte Server=:

Su SQL Server	Valor de Server=
SQL Server Express (por defecto)	localhost\\SQLEXPRESS
Instancia predeterminada	localhost o .
LocalDB de Visual Studio	(localdb)\\MSSQLLocalDB
Con usuario y contraseña	reemplace Trusted_Connection=True; por User Id=su_usuario;Password=su_clave;

En JSON la barra invertida se escribe doble (\\).

4. Ejecutar desde Visual Studio
Abra src/SistemaInventario.sln.
Clic derecho en SistemaInventario.Presentacion → Establecer como proyecto de inicio.
Pulse F5. Si la base de datos no está disponible, la aplicación se lo indica con un mensaje claro.

Desde la terminal: dotnet run --project src/SistemaInventario.Presentacion.

¿Usa otra versión de .NET? Cambie la propiedad MarcoDotNet (por ejemplo, a net8.0) en src/Directory.Build.props; es el único lugar donde se define.

5. Ejecutable compilado 

abralo, ya existe


Queda en build/ (build\SistemaInventario.exe). Debe existir la base de datos y build\appsettings.json debe apuntar a su servidor. Con publicar-release.bat autocontenido la carpeta incluye el runtime de .NET y se ejecuta en cualquier Windows sin instalar nada.


Guía de uso

La barra lateral permite moverse entre Categorías, Suplidores, Productos, Papelera y Reportes (LINQ).

Categorías

Escriba en el buscador y la lista se filtra al instante. Nuevo abre el diálogo de edición, donde se administran las fotos (hasta 5).

<img width="1919" height="1005" alt="image" src="https://github.com/user-attachments/assets/e8f309b5-7a8e-419f-8c56-3843a3da6ef3" />


<img width="810" height="826" alt="image" src="https://github.com/user-attachments/assets/b32b6408-69a2-41d0-8fb6-b46020a7e347" />



Suplidores y productos

<img width="1919" height="1005" alt="image" src="https://github.com/user-attachments/assets/e3733f5d-f2b4-484c-85e9-3f7fce67ec34" />


<img width="1919" height="1005" alt="image" src="https://github.com/user-attachments/assets/41d53816-f7e2-46dd-a815-cde989f291ee" />


Eliminar y Papelera

Eliminar envía el registro a la Papelera (nada se borra físicamente). Una categoría o suplidor con productos activos no se puede eliminar.


<img width="622" height="269" alt="image" src="https://github.com/user-attachments/assets/9e54f49a-9de0-482b-93e5-fe78f53ca878" />



En la Papelera se ven los registros eliminados y se restauran. Un producto solo se restaura si su categoría y su suplidor están activos (en los datos de prueba, el Balón de Fútbol No. 5 exige restaurar primero la categoría Deportes).

<img width="1919" height="431" alt="image" src="https://github.com/user-attachments/assets/d38e193c-69e1-43af-b7c8-3646cb4c8362" />


Reportes (LINQ)

Cada reporte muestra, bajo el título, las operaciones LINQ que usa.

<img width="1919" height="541" alt="image" src="https://github.com/user-attachments/assets/7c95385f-08ea-4a25-8309-e9e5e09b8b51" />


Registro de errores

Cualquier error de base de datos o excepción inesperada se guarda en errores.log junto al ejecutable. El botón Ver registro de errores de la barra lateral lo abre.

<img width="1919" height="988" alt="image" src="https://github.com/user-attachments/assets/29255b42-540d-43df-a878-7e4605f274a8" />



Las pantallas internas (Uc*: Categorías, Suplidores, Productos, Papelera y Reportes) y los botones de navegación de la barra lateral se arman en código; los colores y fuentes compartidos están en Utilidades/Tema.cs.

Arquitectura



Proyecto	Responsabilidad
SistemaInventario.Entidades	Modelos de dominio.
SistemaInventario.AccesoDatos	Repositorios (IRepositorio<T> genérico), Singleton de conexión (ADO.NET).
SistemaInventario.LogicaNegocio	Servicios, validaciones, consultas LINQ y raíz de composición.
SistemaInventario.Presentacion	Formularios de Windows Forms (ventana principal y diálogos de edición con diseñador visual).
SistemaInventario.Comun	Logger (errores.log) y excepciones compartidas.
SistemaInventario.Pruebas	Pruebas unitarias y de arquitectura.

Documentación detallada: en la carpeta [docs]

Estructura del repositorio
text
/SistemaInventario
├── README.md                      instrucciones de instalación + capturas
├── publicar-release.bat           genera el ejecutable Release en /build
├── /src                           código fuente de la solución
│   ├── SistemaInventario.sln
│   ├── SistemaInventario.Entidades/
│   ├── SistemaInventario.Comun/
│   ├── SistemaInventario.AccesoDatos/
│   ├── SistemaInventario.LogicaNegocio/
│   ├── SistemaInventario.Presentacion/
│   └── SistemaInventario.Pruebas/
├── /database                      schema.sql · datos.sql · reset.sql
├── /docs                          diagramas de clases (UML) y ER, diccionario de datos, arquitectura, guion del video
└── /build                         ejecutable compilado en Release




Autor

Jean Carlos Ramirez Paredes [2023-4428]
