using WEBAPI.Models;

namespace WEBAPI.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<object>> ObtenerClientes();
        Task<bool> CrearCliente(Cliente nuevoCliente);
        Task<bool> ActualizarCliente(Cliente clienteActualizado);
        Task<bool> EliminarCliente(int id);
    }
}
