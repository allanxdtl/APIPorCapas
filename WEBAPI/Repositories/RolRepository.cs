using Microsoft.EntityFrameworkCore;
using WEBAPI.Context;
using WEBAPI.DTOs;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class RolRepository : IRol
    {
        private readonly ResidenciasContext _context;
        public RolRepository(ResidenciasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolDto>> GetRoles()
        {
            return await _context.Roles.Select(x => new RolDto { Id =  x.Id, Descripcion = x.Descripcion }).ToListAsync();
        }

        public async Task<bool> CreateRol(Role rol)
        {
            await _context.Roles.AddAsync(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRol(Role rol)
        {
            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRol(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);
                if (rol == null)
                    return false;

                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    throw new InvalidOperationException("No se puede eliminar el rol porque está asignado a uno o más usuarios.");
                }

                throw;
            }
        }
    }
}
