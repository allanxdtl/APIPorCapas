using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuestPDF.Fluent;
using WEBAPI.Context;
using WEBAPI.Documents;
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
            await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Ventas.AddAsync(header);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                VentaHeader? hearderReal = await _context.Ventas
                    .Include(v => v.Cliente)
                    .Include(v => v.Usuario)
                    .Include(v => v.Detalles)
                        .ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(v => v.Id == header.Id);

                if (hearderReal == null)
                    return;

                var pdfDoc = new VentaPDFDocument(hearderReal);
                var pdfBytes = pdfDoc.GeneratePdf();

                var pdfDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
                if (!Directory.Exists(pdfDir))
                    Directory.CreateDirectory(pdfDir);

                var pdfPath = Path.Combine(pdfDir, $"Venta_{header.Id}.pdf");
                await File.WriteAllBytesAsync(pdfPath, pdfBytes);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
