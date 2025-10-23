namespace WEBAPI.Services.Interfaces
{
    public interface IVentasServices
    {
        Task<IEnumerable<object>> ObtenerTodasLasVentasAsync();
    }
}
