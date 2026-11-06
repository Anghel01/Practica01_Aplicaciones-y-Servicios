# Práctica 01 - Aplicaciones y Servicios: MiPrimeraApi

## ¿Qué es el proyecto?

Este proyecto es una API REST desarrollada con ASP.NET Core sobre .NET 8. Su función principal es administrar usuarios mediante operaciones HTTP de consulta, búsqueda, registro y eliminación con persistencia en SQL Server y seguridad mediante autenticación con tokens **JWT (JSON Web Tokens)**.

La API expone endpoints que pueden ser consumidos por aplicaciones web, aplicaciones móviles, clientes de escritorio o herramientas de prueba como Swagger y Postman.

## ¿Qué hace?

La API administra usuarios con la siguiente estructura:

- `IdUsuario`: Identificador del usuario (clave primaria autoincremental).
- `NombreUsuario`: Nombre del usuario.
- `CorreoElectronicousuario`: Correo electrónico.
- `Contrasenausuario`: Contraseña.
- `Activo`: Estado activo o inactivo.

### Endpoints disponibles:

| Método | Ruta | Autorización | Función | Códigos de respuesta |
|---|---|---|---|---|
| `POST` | `/api/Auth/login` | Anónima (`[AllowAnonymous]`) | Autentica al usuario y emite un token JWT. | `200 OK`, `401 Unauthorized` |
| `GET` | `/api/Usuarios` | Requiere Token (`[Authorize]`) | Obtiene todos los usuarios registrados. | `200 OK`, `401 Unauthorized` |
| `GET` | `/api/Usuarios/{id}` | Requiere Token (`[Authorize]`) | Busca un usuario por su identificador. | `200 OK`, `401`, `404 Not Found` |
| `POST` | `/api/Usuarios` | Requiere Token (`[Authorize]`) | Registra un nuevo usuario en la base de datos. | `200 OK`, `401 Unauthorized` |
| `DELETE` | `/api/Usuarios/{id}` | Requiere Rol Admin (`[Authorize(Roles = "Admin")]`) | Elimina un usuario por su identificador. | `204 NoContent`, `401`, `403`, `404` |

## Autenticación con JWT (JSON Web Tokens)

1. El cliente envía sus credenciales al endpoint de login (`POST /api/Auth/login`).
2. Si las credenciales son válidas, la API genera un token JWT firmado con algoritmo HMAC-SHA256 usando la clave secreta configurada en `appsettings.json`.
3. El token incluye claims como el usuario y el rol (`Admin`).
4. En las solicitudes posteriores, el cliente envía el encabezado `Authorization: Bearer <token>`.
5. El middleware de autenticación valida la firma, vigencia, emisor y audiencia.

## ¿Cómo funciona operativamente? (Principios SOLID)

Cuando la aplicación inicia, ASP.NET Core configura los controladores, Swagger con soporte para JWT Bearer, Entity Framework Core y la conexión con SQL Server.

El flujo operativo sigue el principio de inversión de dependencias:

1. La solicitud HTTP llega a los middlewares de autenticación (`UseAuthentication`) y autorización (`UseAuthorization`).
2. La solicitud es procesada por `UsuariosController`.
3. El controlador recibe una abstracción (`IUsuarioRepository`) mediante inyección de dependencias (`AddScoped`), desacoplándolo de la implementación concreta.
4. El controlador delega la operación al repositorio `UsuarioRepository`.
5. `UsuarioRepository` utiliza `ApplicationDbContext` para comunicarse con SQL Server.
6. Entity Framework Core traduce las operaciones a consultas SQL sobre la tabla `Usuarios`.
7. El resultado regresa al repositorio y posteriormente al controlador.
8. El controlador devuelve la respuesta HTTP correspondiente al cliente.

## Organización de los componentes

- **`Controllers/AuthController.cs`**: Controlador para la generación y emisión de tokens JWT.
- **`Controllers/UsuariosController.cs`**: Expone los endpoints protegidos con `[Authorize]` y `[Authorize(Roles = "Admin")]`.
- **`Repositorio/IUsuarioRepository.cs`**: Interfaz que define el contrato de operaciones de usuarios.
- **`Repositorio/UsuarioRepository.cs`**: Implementación del repositorio usando Entity Framework Core y consultas optimizadas (`AsNoTracking`).
- **`Data/ApplicationDbContext.cs`**: Contexto de Entity Framework Core que mapea la entidad `Usuario` con la tabla `Usuarios`.
- **`Models/Usuario.cs`**: Clase modelo de la entidad de usuario.
- **`Program.cs`**: Registro de servicios, configuración de JWT Bearer, Swagger interactivo y pipeline HTTP.
- **`Scripts/InicializarBaseDeDatos.sql`**: Script SQL para crear la base de datos `AplicacionesP01Db`, la tabla `Usuarios` y registros iniciales.

## Conexión y Configuración (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AplicacionesP01Db;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "esta-es-una-clave-secreta-de-mas-de-32-caracteres-123",
    "Issuer": "CafeteriaBackend",
    "Audience": "CafeteriaClientes"
  }
}
```

## Pruebas de los Endpoints

Se pueden ejecutar desde:
1. **Swagger UI**: Disponible en `/swagger`, con soporte para el botón **Authorize** (pegando el token JWT).
2. **Archivo HTTP**: Utilizando `MiPrimeraApi/MiPrimeraApi.http` directamente en Visual Studio.
