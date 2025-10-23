using WEBAPI.DTOs;
using WEBAPI.Repositories.Interfaces;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosAsync()
        {
            return await _repository.GetAllProductosAsync();
        }
    }
}
