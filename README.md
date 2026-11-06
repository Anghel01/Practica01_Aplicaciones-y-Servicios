# Práctica 01 - Aplicaciones y Servicios: MiPrimeraApi

## ¿Qué es el proyecto?

Este proyecto es una API REST desarrollada con ASP.NET Core sobre .NET 8. Su función principal es administrar usuarios mediante operaciones HTTP de consulta, búsqueda, registro y eliminación con persistencia en base de datos.

La API expone endpoints que pueden ser consumidos por aplicaciones web, aplicaciones móviles, clientes de escritorio o herramientas de prueba como Swagger y Postman.

## ¿Qué hace?

La API administra usuarios con la siguiente estructura:

- `IdUsuario`: Identificador del usuario (clave primaria autoincremental).
- `NombreUsuario`: Nombre del usuario.
- `CorreoElectronicousuario`: Correo electrónico.
- `Contrasenausuario`: Contraseña.
- `Activo`: Estado activo o inactivo.

### Endpoints disponibles (`api/Usuarios`):

| Método | Ruta | Función | Códigos de respuesta |
|---|---|---|---|
| `GET` | `/api/Usuarios` | Obtiene todos los usuarios registrados. | `200 OK` |
| `GET` | `/api/Usuarios/{id}` | Busca un usuario por su identificador. | `200 OK`, `404 Not Found` |
| `POST` | `/api/Usuarios` | Registra un nuevo usuario en la base de datos. | `200 OK` |
| `DELETE` | `/api/Usuarios/{id}` | Elimina un usuario por su identificador. | `204 NoContent`, `404 Not Found` |

## ¿Cómo funciona operativamente? (Principios SOLID)

Cuando la aplicación inicia, ASP.NET Core configura los controladores, Swagger, Entity Framework Core y la conexión con SQL Server.

El flujo operativo sigue el principio de inversión de dependencias:

1. La solicitud HTTP llega a `UsuariosController`.
2. El controlador recibe una abstracción (`IUsuarioRepository`) mediante inyección de dependencias (`AddScoped`), desacoplándolo de la implementación concreta.
3. El controlador delega la operación al repositorio `UsuarioRepository`.
4. `UsuarioRepository` utiliza `ApplicationDbContext` para comunicarse con SQL Server.
5. Entity Framework Core traduce las operaciones a consultas SQL sobre la tabla `Usuarios`.
6. El resultado regresa al repositorio y posteriormente al controlador.
7. El controlador devuelve la respuesta HTTP correspondiente al cliente.

## Organización de los componentes

- **`Controllers/UsuariosController.cs`**: Expone los endpoints HTTP y maneja las respuestas (`200 OK`, `204 NoContent`, `404 Not Found`).
- **`Repositorio/IUsuarioRepository.cs`**: Interfaz que define el contrato de operaciones de usuarios.
- **`Repositorio/UsuarioRepository.cs`**: Implementación del repositorio usando Entity Framework Core y consultas optimizadas (`AsNoTracking`).
- **`Data/ApplicationDbContext.cs`**: Contexto de Entity Framework Core que mapea la entidad `Usuario` con la tabla `Usuarios`.
- **`Models/Usuario.cs`**: Clase modelo de la entidad de usuario.
- **`Program.cs`**: Registro de servicios, inyección de dependencias y pipeline HTTP.
- **`Scripts/InicializarBaseDeDatos.sql`**: Script SQL para crear la base de datos `AplicacionesP01Db`, la tabla `Usuarios` y registros iniciales.

## Conexión con SQL Server

Configurada en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AplicacionesP01Db;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## Inicialización de la Base de Datos

Para inicializar la base de datos local:
1. Abrir SQL Server Management Studio (SSMS) o la terminal de SQL Server.
2. Conectarse a `(localdb)\MSSQLLocalDB`.
3. Ejecutar el script ubicado en `MiPrimeraApi/Scripts/InicializarBaseDeDatos.sql`.

## Tecnologías utilizadas

- .NET 8 (C#)
- ASP.NET Core Web API
- Entity Framework Core 8 (SQL Server)
- SQL Server LocalDB
- Swagger / OpenAPI
- Inyección de dependencias y Patrón Repositorio
