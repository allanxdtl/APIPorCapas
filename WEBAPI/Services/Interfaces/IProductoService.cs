using WEBAPI.DTOs;
using WEBAPI.Models;

namespace WEBAPI.Services.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerProductosAsync();
        Task<bool> CrearProductoAsync(Producto producto);
        Task<bool> ActualizarProductoAsync(Producto producto);
        Task<bool> EliminarProductoAsync(string barcode);
    }
}
