using WEBAPI.Models;

namespace WEBAPI.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<object>> GetClients();
        Task<bool> InsertClient(Cliente cliente);
        Task<bool> UpdateClient(Cliente cliente);
        Task<bool> DeleteClient(int id);
        Task<Cliente?> VerifyPhoneNumber(string phoneNumber);
        Task<Cliente?> VerifyEmail(string email);
    }
}
