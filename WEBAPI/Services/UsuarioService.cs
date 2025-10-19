using WEBAPI.Utils;
using WEBAPI.Models;
using WEBAPI.Repositories;

namespace WEBAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        //Inyeccion de dependencia de Repositorio de Usuario
        private readonly IUsuario _repository;
        public UsuarioService(IUsuario repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<object>> ListarUsuarios()
        {
            return await _repository.ListUsers();
        }

        public async Task<bool> IngresarUsuario(Usuario user)
        {
            return await _repository.LogUser(user);
        }

        public async Task<bool> CrearUsuario(Usuario user)
        {
            user.Password = PasswordHasher.Hash(user.Password);

            var usuarioExistente = await _repository.VerifyUsername(user.Username);
            bool rolExistente = await _repository.VerifyRol(user.IdRol);

            if (usuarioExistente == null && rolExistente)
                return await _repository.InsertUser(user);
            return false;
        }

        public async Task<bool> BorrarUsuario(int id)
        {
            return await _repository.DeleteUser(id);
        }

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            usuario.Password = PasswordHasher.Hash(usuario.Password);

            var usuarioExistente = await _repository.VerifyUsername(usuario.Username);
            bool rolExistente = await _repository.VerifyRol(usuario.IdRol);

            if (!rolExistente)
                return false;

            if (usuarioExistente != null && usuarioExistente.Id != usuario.Id)
                return false;

            bool result = await _repository.UpdateUser(usuario);
            return result;
        }
    }
}
