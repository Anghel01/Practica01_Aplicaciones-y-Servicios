# Evidencia de Prueba: Examen Práctico Corto ITID803_02

## 1. Guión de Prueba Ejecutado

### Paso 1: Autenticación en `/api/Auth/login`
- **Request**:
  ```json
  POST /api/Auth/login
  {
    "usuario": "admin",
    "password": "1234"
  }
  ```
- **Response**: `200 OK` (Token JWT emitido con rol `Admin`).

---

### Paso 2: Creación de Proveedor en `POST /api/Proveedores`
- **Headers**:
  ```http
  Authorization: Bearer <TOKEN_ADMIN>
  Content-Type: application/json
  ```
- **Request Body**:
  ```json
  {
    "nombre": "Distribuidora La Costa",
    "telefono": "0991234567",
    "activo": true
  }
  ```
- **Response**: `201 Created`
  ```json
  {
    "id": 1,
    "nombre": "Distribuidora La Costa",
    "telefono": "0991234567",
    "activo": true
  }
  ```

---

### Paso 3: Persistencia en SQL Server LocalDB (`AplicacionesP01Db.dbo.Proveedores`)

Consulta ejecutada:
```sql
SELECT Id, Nombre, Telefono, Activo FROM dbo.Proveedores;
```

**Resultado obtenido**:
```text
Id          Nombre                                                               Telefono             Activo
----------- -------------------------------------------------------------------- -------------------- ------
1           Distribuidora La Costa                                               0991234567           1

(1 rows affected)
```

---

## Conclusión
- La tabla `Proveedores` fue creada automáticamente vía migraciones de Entity Framework Core (`AgregarProveedores`).
- La columna `Nombre` se configuró como `nvarchar(100) NOT NULL`.
- La columna `Telefono` se configuró como `nvarchar(20) NULL`.
- El endpoint `POST /api/Proveedores` está debidamente protegido con `[Authorize(Roles = "Admin")]`.
- El ID autoincremental asignado por SQL Server fue `1`.
