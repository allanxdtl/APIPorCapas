using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WEBAPI.Context;
using WEBAPI.Models;
using WEBAPI.Utils;

namespace WEBAPI.Repositories
{

    /// <summary>
    /// Implementacion de interfaz IUsuario
    /// </summary>
    public class UsuarioRepository : IUsuario
    {
        //Inyeccion de dependencia de base de datos
        private readonly ResidenciasContext _context;
        public UsuarioRepository(ResidenciasContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ingreso al sistema con un usuario
        /// </summary>
        /// <param name="user">Objeto de tipo Usuario, campos username y password</param>
        /// <returns>true si las credenciales son correctas, false si no coincide</returns>
        public async Task<bool> LogUser(Usuario user)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == user.Username);

            if (usuario == null)
                return false;

            bool login = PasswordHasher.Verify(user.Password, usuario.Password);

            if (!login)
                return false;

            return true;
        }

        /// <summary>
        /// Regresa lista de usuarios
        /// </summary>
        /// <returns>Lista de usuarios del sistema, con id, nombre y apellido</returns>
        public async Task<IEnumerable<object>> ListUsers()
        {
            return await _context.Usuarios.Include(e => e.IdRolNavigation).Select(u => new { u.Id, u.Nombre, u.Apellido, u.Username, rol = u.IdRolNavigation.Descripcion }).ToListAsync();
        }

        /// <summary>
        /// Inserta un usuario en el sistema
        /// </summary>
        /// <param name="user">Objeto de tipo Usuario</param>
        /// <returns>true si la insercion fue correcta, false si no</returns>
        public async Task<bool> InsertUser(Usuario user)
        {
            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Actualiza un usuario en el sistema
        /// </summary>
        /// <param name="user">Objeto de tipo Usuario</param>
        /// <returns>True si fue actualizado correctamente, false si no</returns>
        public async Task<bool> UpdateUser(Usuario user)
        {
            _context.Usuarios.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Elimina un usuario del sistema
        /// </summary>
        /// <param name="id">Id del Usuario a eliminar</param>
        /// <returns>True si la eliminacion fue exitosa, false si no</returns>
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);

            if (user == null)
                return false;
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Verifica si el nombre de usuario existe en el sistema
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <returns>El objeto del usuario si existe el username, null si no existe</returns>
        public async Task<Usuario?> VerifyUsername(string username)
        {
            var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
            if (usuario == null)
                return null;
            return usuario;
        }

        public async Task<bool> VerifyRol(int rol)
        {
            var rolExistente = await _context.Roles.FirstOrDefaultAsync(r => r.Id == rol);

            if (rolExistente == null)
                return false;
            return true;
        }
    }
}
