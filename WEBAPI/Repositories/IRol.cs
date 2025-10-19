using System.Collections;
using WEBAPI.DTOs;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public interface IRol
    {
        Task<IEnumerable<RolDto>> GetRoles();
        Task<bool> CreateRol(Role rol);
        Task<bool> UpdateRol(Role rol);
        Task<bool> DeleteRol(int id);
    }
}
