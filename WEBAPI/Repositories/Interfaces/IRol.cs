using WEBAPI.Models;

namespace WEBAPI.Repositories.Interfaces
{
    public interface IRol
    {
        Task<IEnumerable<object>> GetRoles();
        Task<bool> CreateRol(Role rol);
        Task<bool> UpdateRol(Role rol);
        Task<bool> DeleteRol(int id);
    }
}
