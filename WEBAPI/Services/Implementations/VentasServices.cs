using WEBAPI.Repositories.Interfaces;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Services.Implementations
{
    public class VentasServices : IVentasServices
    {
        private readonly IVentasRepository _repository;
        public VentasServices(IVentasRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<object>> ObtenerTodasLasVentasAsync()
        {
            return await _repository.GetAllVentasAsync();
        }

    }
}
