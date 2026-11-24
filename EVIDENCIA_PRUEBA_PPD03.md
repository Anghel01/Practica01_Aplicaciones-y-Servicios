# Evidencia de Prueba – PPD03 (Arquitectura en Capas, Interfaces y Persistencia)

## 1. Compilación del proyecto
Comando ejecutado:
```powershell
dotnet build "C:\Users\angel\source\repos\MiPrimeraApi\MiPrimeraApi\MiPrimeraApi.csproj"
```
Resultado:
- `Compilación correcta. 0 Advertencia(s) 0 Errores`

## 2. Prueba en vivo del endpoint `POST /api/Proveedores`
Autenticación previa con `POST http://localhost:5055/api/Auth/login` (`Usuario: "admin"`) para obtener el JWT Bearer Token con rol `Admin`.

Petición enviada a `POST http://localhost:5055/api/Proveedores`:
```json
{
  "nombre": "Distribuidora La Costa",
  "telefono": "0991234567",
  "activo": true
}
```

Respuesta HTTP observada:
- **Status Code:** `201` (`Created`)
- **Body:**
```json
{"id":2,"nombre":"Distribuidora La Costa","telefono":"0991234567","activo":true}
```

## 3. Verificación en SQL Server (`AplicacionesP01Db` -> `dbo.Proveedores`)
Consulta ejecutada:
```sql
SELECT * FROM dbo.Proveedores;
```
Resultado observado en `(localdb)\MSSQLLocalDB`:
```text
Id          Nombre                                                                                               Telefono             Activo
----------- ---------------------------------------------------------------------------------------------------- -------------------- ------
          1 Distribuidora La Costa                                                                               0991234567                1
          2 Distribuidora La Costa                                                                               0991234567                1

(2 rows affected)
```
