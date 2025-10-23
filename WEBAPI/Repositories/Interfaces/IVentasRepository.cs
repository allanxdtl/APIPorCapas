using WEBAPI.Models;

namespace WEBAPI.Repositories.Interfaces
{
    public interface IVentasRepository
    {
        Task<IEnumerable<object>> GetAllVentasAsync();
        Task CreateVenta(VentaHeader header, List<VentaDetalle> detalle);
    }
}
