using WEBAPI.Models;
using WEBAPI.Repositories.Interfaces;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Services.Implementations
{
    public class RolService : IRolService
    {
        private readonly IRol _repository;
        public RolService(IRol repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<object>> ObtenerRoles()
        {
            return await _repository.GetRoles();
        }

        public async Task<bool> CrearRol(Role rol)
        {
            return await _repository.CreateRol(rol);
        }

        public async Task<bool> EditarRol(Role rol)
        {
            return await _repository.UpdateRol(rol);
        }

        public async Task<bool> BorrarRol(int id)
        {
            try
            {
                return await _repository.DeleteRol(id);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
