# Práctica 01 - Aplicaciones y Servicios: MiPrimeraApi

## ¿Qué es el proyecto?

Este proyecto es una API REST desarrollada con ASP.NET Core sobre .NET 8. Su función principal es administrar usuarios mediante operaciones HTTP de consulta, búsqueda, registro y eliminación con persistencia en SQL Server, seguridad mediante tokens **JWT (JSON Web Tokens)** y control de acceso basado en **Roles (RBAC - Role-Based Access Control)**.

La API expone endpoints que pueden ser consumidos por aplicaciones web, aplicaciones móviles, clientes de escritorio o herramientas de prueba como Swagger y Postman.

## Estructura del Modelo `Usuario`

- `IdUsuario`: Identificador del usuario (clave primaria autoincremental).
- `NombreUsuario`: Nombre del usuario.
- `CorreoElectronicousuario`: Correo electrónico.
- `Contrasenausuario`: Contraseña.
- `Activo`: Estado activo o inactivo.

## Roles del Sistema y Permisos

El sistema implementa dos roles principales emitidos dinámicamente en el login:

1. **`Admin`**: Control total. Puede consultar (`GET`), registrar nuevos usuarios (`POST`) y eliminar usuarios (`DELETE`).
2. **`Usuario`**: Perfil de solo lectura. Puede consultar (`GET`), pero cualquier intento de crear (`POST`) o eliminar (`DELETE`) responderá con `403 Forbidden`.

### Matriz de Endpoints y Permisos:

| Método | Ruta | Autorización | Rol Requerido | Función | Códigos de respuesta |
|---|---|---|---|---|---|
| `POST` | `/api/Auth/login` | Anónima (`[AllowAnonymous]`) | Ninguno | Autentica y genera el token con su Rol. | `200 OK`, `401 Unauthorized` |
| `GET` | `/api/Usuarios` | Requiere Token (`[Authorize]`) | `Admin` o `Usuario` | Obtiene todos los usuarios registrados. | `200 OK`, `401 Unauthorized` |
| `GET` | `/api/Usuarios/{id}` | Requiere Token (`[Authorize]`) | `Admin` o `Usuario` | Busca un usuario por su identificador. | `200 OK`, `401`, `404 Not Found` |
| `POST` | `/api/Usuarios` | Requiere Rol (`[Authorize(Roles = "Admin")]`) | **Solo `Admin`** | Registra un nuevo usuario en la base de datos. | `200 OK`, `401`, `403 Forbidden` |
| `DELETE` | `/api/Usuarios/{id}` | Requiere Rol (`[Authorize(Roles = "Admin")]`) | **Solo `Admin`** | Elimina un usuario por su identificador. | `204 NoContent`, `401`, `403 Forbidden`, `404` |

## Usuarios de Prueba (Simulación de Roles)

Para pruebas locales en `/api/Auth/login`:
- **Administrador**:
  - `usuario`: `admin`
  - `password`: `1234`
  - Rol asignado: `Admin`
- **Cliente**:
  - `usuario`: `cliente`
  - `password`: `1234`
  - Rol asignado: `Usuario`

## ¿Cómo funciona operativamente? (Principios SOLID)

1. La solicitud HTTP pasa por los middlewares de autenticación (`UseAuthentication`) y autorización (`UseAuthorization`).
2. Se evalúa el token JWT: valida firma, expiración y claims de rol (`ClaimTypes.Role`).
3. El controlador [UsuariosController.cs](file:///C:/Users/angel/source/repos/MiPrimeraApi/MiPrimeraApi/Controllers/UsuariosController.cs) recibe una abstracción ([IUsuarioRepository.cs](file:///C:/Users/angel/source/repos/MiPrimeraApi/MiPrimeraApi/Repositorio/IUsuarioRepository.cs)) mediante inyección de dependencias (`AddScoped`), desacoplándolo de la implementación concreta.
4. El repositorio [UsuarioRepository.cs](file:///C:/Users/angel/source/repos/MiPrimeraApi/MiPrimeraApi/Repositorio/UsuarioRepository.cs) ejecuta las consultas a SQL Server mediante [ApplicationDbContext.cs](file:///C:/Users/angel/source/repos/MiPrimeraApi/MiPrimeraApi/Data/ApplicationDbContext.cs) utilizando Entity Framework Core.

## Pruebas de los Endpoints

Se pueden ejecutar desde:
1. **Swagger UI**: Disponible en `/swagger`, con soporte para el botón **Authorize** (pegando el token JWT).
2. **Archivo HTTP**: Utilizando [MiPrimeraApi.http](file:///C:/Users/angel/source/repos/MiPrimeraApi/MiPrimeraApi/MiPrimeraApi.http) directamente en Visual Studio.
