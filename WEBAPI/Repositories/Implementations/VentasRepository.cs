using Microsoft.EntityFrameworkCore;
using WEBAPI.Context;
using WEBAPI.Models;
using WEBAPI.Repositories.Interfaces;

namespace WEBAPI.Repositories.Implementations
{
    public class VentasRepository : IVentasRepository
    {
        private readonly ResidenciasContext _context;

        public VentasRepository(ResidenciasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllVentasAsync()
        {
            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Select(v => new
                {
                    v.Id,
                    v.Fecha,
                    v.Total,
                    ClienteNombre = v.Cliente.Nombre,
                    UsuarioNombre = v.Usuario.Nombre
                }).ToListAsync();
        }

        public async Task CreateVenta(VentaHeader header, List<VentaDetalle> detalle)
        {
            await _context.Ventas.AddAsync(header);
            await _context.SaveChangesAsync();
            foreach (var item in detalle)
            {
                item.IdVentaHeader = header.Id;
            }
            await _context.VentaDetalles.AddRangeAsync(detalle);
            await _context.SaveChangesAsync();
        }
    }
}
