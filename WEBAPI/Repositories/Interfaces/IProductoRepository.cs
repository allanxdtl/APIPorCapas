using WEBAPI.DTOs;
using WEBAPI.Models;

namespace WEBAPI.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoDto>> GetAllProductosAsync();
        Task<bool> CreateProductoAsync(Producto producto);
        Task<bool> UpdateProductoAsync(Producto producto);
        Task<bool> DeleteProductoAsync(Producto producto);
        Task<Producto?> VerifyBarcodeAsync(string codigoBarra);
    }
}
