using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Context;
using WEBAPI.DTOs;
using WEBAPI.Models;
using WEBAPI.Repositories.Interfaces;

namespace WEBAPI.Repositories.Implementations
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ResidenciasContext _context;
        public ProductoRepository(ResidenciasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDto>> GetAllProductosAsync()
        {
            return await _context.Productos
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion,
                    Codigo = p.Codigo,
                    Precio = p.Precio
                })
                .ToListAsync();
        }

        public async Task<bool> CreateProductoAsync(Producto producto)
        {
            try
            {
                await _context.Productos.AddAsync(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProductoAsync(Producto producto)
        {
            try
            {
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (SqlException)
            {
                throw new Exception("No se ha podido establecer la conexion con la base de datos");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteProductoAsync(Producto producto)
        {
            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("No es posible eliminar el producto, tiene ventas relacionadas");
            }
        }

        public async Task<Producto?> VerifyBarcodeAsync(string codigoBarra)
        {
            try
            {
                return await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Codigo == codigoBarra);
            }
            catch (SqlException)
            {
                throw new Exception("No ha sido posible conectarse a la base de datos");
            }
        }
    }
}
