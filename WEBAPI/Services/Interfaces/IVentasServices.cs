using WEBAPI.Models;

namespace WEBAPI.Services.Interfaces
{
    public interface IVentasServices
    {
        Task<IEnumerable<object>> ObtenerTodasLasVentasAsync();
        Task CrearVentaAsync(VentaHeader header, List<VentaDetalle> detalle);
    }
}
