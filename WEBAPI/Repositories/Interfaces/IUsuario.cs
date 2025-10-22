using WEBAPI.Models;

namespace WEBAPI.Repositories.Interfaces
{
    /// <summary>
    /// Contrato de uso para manejar usuarios en la API
    /// </summary>
    public interface IUsuario
    {
        Task<bool> LogUser(Usuario user);
        Task<IEnumerable<object>> ListUsers();
        Task<bool> InsertUser(Usuario user);
        Task<bool> UpdateUser(Usuario user);
        Task<bool> DeleteUser(int id);
        Task<Usuario?> VerifyUsername(string username);
        Task<bool> VerifyRol(int id);
    }
}
