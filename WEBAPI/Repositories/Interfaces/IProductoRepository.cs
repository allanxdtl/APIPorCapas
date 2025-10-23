using WEBAPI.DTOs;
using WEBAPI.Models;

namespace WEBAPI.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoDto>> GetAllProductosAsync();
        Task<bool> CreateProductoAsync(Producto producto);
        Task<bool> UpdateProducto(Producto producto);
        Task<bool> DeleteProducto(int id);
        Task<Producto?> VerifyBarcode(string codigoBarra);
    }
}
