using WEBAPI.Models;
using WEBAPI.Repositories.Interfaces;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<object>> ObtenerClientes()
        {
            return _repository.GetClients();
        }

        public async Task<bool> CrearCliente(Cliente nuevoCliente)
        {
            if (nuevoCliente.Telefono == null || nuevoCliente.Email == null)
                throw new ArgumentException("El número de teléfono y el correo no pueden ser nulos.");

            Cliente? telefonoExiste = await _repository.VerifyPhoneNumber(nuevoCliente.Telefono);
            Cliente? emailExiste = await _repository.VerifyEmail(nuevoCliente.Email);

            if (telefonoExiste != null)
                throw new InvalidOperationException("El número de teléfono ya está registrado.");

            if (emailExiste != null)
                throw new InvalidOperationException("El email ya está registrado.");

            return await _repository.InsertClient(nuevoCliente);
        }

        public async Task<bool> ActualizarCliente(Cliente clienteActualizado)
        {
            if (clienteActualizado.Telefono == null || clienteActualizado.Email == null)
                throw new ArgumentException("El número de teléfono y el correo no pueden ser nulo.");

            Cliente? telefonoExistente = await _repository.VerifyPhoneNumber(clienteActualizado.Telefono);
            Cliente? emailExistente = await _repository.VerifyEmail(clienteActualizado.Email);

            if (telefonoExistente != null && emailExistente != null && telefonoExistente.Id != clienteActualizado.Id)
                throw new InvalidOperationException("El número de teléfono ya está registrado por otro cliente.");

            return await _repository.UpdateClient(clienteActualizado);
        }

        public async Task<bool> EliminarCliente(int id)
        {
            try
            {
                return await _repository.DeleteClient(id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }
    }
}
