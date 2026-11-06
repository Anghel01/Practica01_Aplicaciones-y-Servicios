using MiPrimeraApi.Models;

namespace MiPrimeraApi.Repositorio
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> ObtenerUsuarios();
        Usuario? BuscarPorId(int id);
        void AgregarUsuario(Usuario nuevoUsuario);
        bool EliminarPorId(int id);
    }
}
