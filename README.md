# Práctica 01 - Aplicaciones y Servicios: MiPrimeraApi

## ¿Qué es el proyecto?

Este proyecto es una API REST desarrollada con ASP.NET Core sobre .NET 8. Su función principal es administrar **Usuarios** y **Productos** mediante operaciones HTTP con persistencia en SQL Server (Entity Framework Core), seguridad mediante tokens **JWT (JSON Web Tokens)** y control de acceso basado en **Roles (RBAC - Role-Based Access Control)**.

La API expone endpoints que pueden ser consumidos por aplicaciones web, aplicaciones móviles, clientes de escritorio o herramientas de prueba como Swagger y Postman.

## Modelos de Datos

### 1. `Usuario`
- `IdUsuario`: Identificador del usuario (clave primaria autoincremental).
- `NombreUsuario`: Nombre del usuario.
- `CorreoElectronicousuario`: Correo electrónico.
- `Contrasenausuario`: Contraseña.
- `Activo`: Estado activo o inactivo.

### 2. `Producto`
- `Id`: Identificador del producto (clave primaria autoincremental).
- `Nombre`: Nombre del producto (`nvarchar(100)`).
- `Precio`: Precio del producto (`decimal(18,2)`).
- `Activo`: Estado activo o inactivo (`bit`).

## Roles del Sistema y Permisos

1. **`Admin`**: Control total. Puede consultar (`GET`), registrar (`POST`), actualizar (`PUT`) y eliminar (`DELETE`).
2. **`Usuario`**: Solo lectura. Puede consultar (`GET`), pero cualquier intento de crear, editar o borrar responderá con `403 Forbidden`.

### Matriz de Endpoints y Permisos:

| Recurso | Método | Ruta | Autorización | Rol Requerido | Función | Códigos de respuesta |
|---|---|---|---|---|---|---|
| **Auth** | `POST` | `/api/Auth/login` | Anónima (`[AllowAnonymous]`) | Ninguno | Autentica y genera el token con su Rol. | `200 OK`, `401` |
| **Usuarios** | `GET` | `/api/Usuarios` | Requiere Token | `Admin` o `Usuario` | Obtiene todos los usuarios. | `200 OK`, `401` |
| **Usuarios** | `GET` | `/api/Usuarios/{id}` | Requiere Token | `Admin` o `Usuario` | Busca usuario por ID. | `200 OK`, `401`, `404` |
| **Usuarios** | `POST` | `/api/Usuarios` | Requiere Rol | **Solo `Admin`** | Registra un nuevo usuario. | `200 OK`, `401`, `403` |
| **Usuarios** | `DELETE` | `/api/Usuarios/{id}` | Requiere Rol | **Solo `Admin`** | Elimina un usuario por ID. | `204 NoContent`, `401`, `403`, `404` |
| **Productos** | `GET` | `/api/Productos` | Requiere Token | `Admin` o `Usuario` | Obtiene todos los productos. | `200 OK`, `401` |
| **Productos** | `GET` | `/api/Productos/{id}` | Requiere Token | `Admin` o `Usuario` | Busca producto por ID. | `200 OK`, `401`, `404` |
| **Productos** | `POST` | `/api/Productos` | Requiere Rol | **Solo `Admin`** | Crea un nuevo producto. | `201 Created`, `401`, `403` |
| **Productos** | `PUT` | `/api/Productos/{id}` | Requiere Rol | **Solo `Admin`** | Actualiza un producto existente. | `204 NoContent`, `401`, `403`, `404` |
| **Productos** | `DELETE` | `/api/Productos/{id}` | Requiere Rol | **Solo `Admin`** | Elimina un producto por ID. | `204 NoContent`, `401`, `403`, `404` |

## Usuarios de Prueba (Simulación de Roles)

Para pruebas locales en `/api/Auth/login`:
- **Administrador**: `usuario: admin`, `password: 1234` &rarr; Rol: `Admin`
- **Cliente**: `usuario: cliente`, `password: 1234` &rarr; Rol: `Usuario`

## Arquitectura y Migraciones (Code First)

1. **Entity Framework Core**:
   - `ApplicationDbContext.cs` mapea las entidades `Usuarios` y `Productos`.
   - Migraciones creadas con `dotnet ef migrations add` y aplicadas con `dotnet ef database update`.
2. **Patrón Repositorio y SOLID**:
   - Acceso a datos desacoplado en `UsuarioRepository` y `ProductoRepository`.
   - Inyección de dependencias `AddScoped` en `Program.cs`.
3. **Seguridad**:
   - Middleware de autenticación JWT y autorización por roles.
   - Swagger UI interactivo con botón `Authorize` (Bearer token).
