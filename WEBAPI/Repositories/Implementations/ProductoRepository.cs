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

        public Task<bool> CreateProductoAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProducto(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Producto?> VerifyBarcode(string codigoBarra)
        {
            throw new NotImplementedException();
        }
    }
}
