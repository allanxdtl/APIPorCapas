using WEBAPI.Models;

namespace WEBAPI.Services.Interfaces
{

    /// <summary>
    /// Contrato de uso para servicio de Usuarios, con reglas de negocio
    /// </summary>
    public interface IUsuarioService
    {
        Task<IEnumerable<object>> ListarUsuarios();
        Task<bool> IngresarUsuario(Usuario user);
        Task<bool> CrearUsuario(Usuario usuario);
        Task<bool> ActualizarUsuario(Usuario usuario);
        Task<bool> BorrarUsuario(int id);
    }
}
