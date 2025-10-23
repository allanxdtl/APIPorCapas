using WEBAPI.DTOs;

namespace WEBAPI.Services.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerProductosAsync();
    }
}
