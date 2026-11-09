using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Models;
using MiPrimeraApi.Repositorio;

namespace MiPrimeraApi.Controllers
{
    [Authorize] // Cualquier usuario autenticado con token puede acceder (Admin o Usuario)
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _repositorio;

        public UsuariosController(IUsuarioRepository repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet] // Admin y Usuario pueden consultar
        public ICollection<Usuario> ObtenerUsuarios()
        {
            return _repositorio.ObtenerUsuarios();
        }

        [HttpGet("{id}")] // Admin y Usuario pueden consultar
        public ActionResult<Usuario> BuscarPorId(int id)
        {
            var usuario = _repositorio.BuscarPorId(id);

            if (usuario == null)
            {
                return NotFound($"Usuario con el ID {id} no encontrado.");
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Solo Admin puede crear
        public ActionResult AgregarUsuario([FromBody] Usuario nuevoUsuario)
        {
            _repositorio.AgregarUsuario(nuevoUsuario);
            return Ok(nuevoUsuario);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Solo Admin puede eliminar
        public ActionResult EliminarPorId(int id)
        {
            bool eliminado = _repositorio.EliminarPorId(id);
            if (!eliminado)
            {
                return NotFound($"No se puede eliminar: Usuario con ID {id} no encontrado.");
            }
            return NoContent();
        }
    }
}
