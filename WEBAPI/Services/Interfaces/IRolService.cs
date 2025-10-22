using WEBAPI.Models;

namespace WEBAPI.Services.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<object>> ObtenerRoles();
        Task<bool> CrearRol(Role rol);
        Task<bool> EditarRol(Role rol);
        Task<bool> BorrarRol(int id);
    }
}
