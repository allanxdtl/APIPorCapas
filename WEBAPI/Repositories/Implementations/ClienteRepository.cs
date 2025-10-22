using Microsoft.EntityFrameworkCore;
using WEBAPI.Context;
using WEBAPI.Models;
using WEBAPI.Repositories.Interfaces;

namespace WEBAPI.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ResidenciasContext _context;
        public ClienteRepository(ResidenciasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetClients()
        {
            return await _context.Clientes
                .AsNoTracking()
                .Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Apellido,
                    c.Email,
                    c.Telefono
                }).ToListAsync();
        }

        public async Task<bool> InsertClient(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateClient(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClient(int id)
        {
            Cliente? cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;
            try
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("No se puede eliminar el cliente porque está asociado a uno o más registros de ventas.", ex);
            }
            return true;
        }

        public async Task<Cliente?> VerifyPhoneNumber(string phoneNumber)
        {
            return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Telefono == phoneNumber);
        }

        public async Task<Cliente?> VerifyEmail(string email)
        {
            return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
