using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Interfaces;
using MiPrimeraApi.Models;

namespace MiPrimeraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly IProveedorService _service;

    public ProveedoresController(IProveedorService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Proveedor>>> ObtenerTodos()
    {
        var proveedores = await _service.ObtenerTodosAsync();
        return Ok(proveedores);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Proveedor>> ObtenerPorId(int id)
    {
        var proveedor = await _service.BuscarPorIdAsync(id);
        if (proveedor == null) return NotFound();
        return Ok(proveedor);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Proveedor>> Crear([FromBody] Proveedor proveedor)
    {
        var creado = await _service.CrearProveedorAsync(proveedor);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }
}
