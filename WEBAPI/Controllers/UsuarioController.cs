using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;
using WEBAPI.Services;

namespace WEBAPI.Controllers
{
    /// <summary>
    /// Controlador que gestiona las operaciones relacionadas con los usuarios del sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        /// <summary>
        /// Inicializa una nueva instancia del controlador de usuarios.
        /// </summary>
        /// <param name="service">Interfaz del servicio de usuarios inyectado mediante dependencia.</param>
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        // ---------------------------------------------------------------------------------------
        // GET: api/Usuario
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// 🔍 Obtiene la lista completa de usuarios registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve todos los usuarios activos o registrados en el sistema.
        /// 
        /// **Ejemplo de respuesta:**
        /// ```json
        /// [
        ///   {
        ///     "id": 1,
        ///     "nombre": "Bryan",
        ///     "apellido": "Valdez",
        ///     "username": "allanxdtl",
        ///     "rol":"Administrador"
        ///   }
        /// ]
        /// ```
        /// </remarks>
        /// <response code="200">Devuelve la lista completa de usuarios.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <returns>Lista de objetos <see cref="Usuario"/> en formato JSON.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ListarUsuarios());
        }

        // ---------------------------------------------------------------------------------------
        // POST: api/Usuario/Login
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// 🔐 Verifica si las credenciales del usuario son correctas.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite validar el inicio de sesión de un usuario mediante su nombre y contraseña.
        /// 
        /// **Ejemplo de solicitud:**
        /// ```json
        /// {
        ///   "username": "bvaldez",
        ///   "password": "12345"
        /// }
        /// ```
        /// 
        /// **Respuestas posibles:**
        /// - ✅ **200 OK** — Acceso autorizado.  
        /// - ❌ **401 Unauthorized** — Credenciales incorrectas.  
        /// - ⚠️ **400 Bad Request** — Solicitud mal formada.  
        /// </remarks>
        /// <param name="usuario">Objeto con las credenciales del usuario (`Username`, `Password`).</param>
        /// <response code="200">Acceso autorizado.</response>
        /// <response code="401">Credenciales incorrectas.</response>
        /// <response code="400">Solicitud mal formada o con datos inválidos.</response>
        /// <returns>Mensaje indicando si el inicio de sesión fue exitoso o no.</returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = await _service.IngresarUsuario(usuario);
            return result ? Ok(new { message = "Acceso autorizado" }) :
                            Unauthorized(new { message = "Credenciales incorrectas" });
        }

        // ---------------------------------------------------------------------------------------
        // POST: api/Usuario
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// ➕ Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint inserta un nuevo registro de usuario si el nombre de usuario no existe.
        /// 
        /// **Ejemplo de solicitud:**
        /// ```json
        /// {
        ///   "nombre": "Allan",
        ///   "apellido": "Valdez",
        ///   "username": "allanxdtl",
        ///   "password": "12345"
        /// }
        /// ```
        /// 
        /// **Respuestas posibles:**
        /// - ✅ **200 OK** — Usuario insertado correctamente.  
        /// - ⚠️ **400 Bad Request** — El usuario ya existe.  
        /// </remarks>
        /// <param name="usuario">Objeto de tipo <see cref="Usuario"/> con los datos del nuevo usuario.</param>
        /// <response code="200">Usuario insertado correctamente.</response>
        /// <response code="400">No se puede crear el usuario, ya existe.</response>
        /// <returns>Mensaje indicando si la inserción fue exitosa.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            bool result = await _service.CrearUsuario(usuario);
            if (result)
                return Ok(new { message = "Usuario insertado correctamente" });
            return BadRequest(new { message = "No se puede crear el usuario, ya existe el usuario" });
        }

        // ---------------------------------------------------------------------------------------
        // DELETE: api/Usuario/{id}
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// 🗑️ Elimina un usuario del sistema por su identificador.
        /// </summary>
        /// <remarks>
        /// Elimina un usuario existente según su ID.  
        /// 
        /// **Ejemplo de solicitud:**
        /// ```
        /// DELETE /api/Usuario/1
        /// ```
        /// 
        /// **Respuestas posibles:**
        /// - ✅ **200 OK** — Usuario eliminado.  
        /// - ❌ **404 Not Found** — No se encontró el usuario solicitado.  
        /// </remarks>
        /// <param name="id">Identificador numérico del usuario.</param>
        /// <response code="200">Usuario eliminado.</response>
        /// <response code="404">El usuario no existe.</response>
        /// <returns>Mensaje indicando el resultado de la eliminación.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _service.BorrarUsuario(id);
            if (!result)
                return NotFound(new { message = "El usuario solicitado no existe" });
            return Ok(new { message = "Usuario eliminado" });
        }

        // ---------------------------------------------------------------------------------------
        // PUT: api/Usuario
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// ✏️ Actualiza la información de un usuario existente.
        /// </summary>
        /// <remarks>
        /// Este endpoint modifica los datos de un usuario registrado.
        /// 
        /// **Ejemplo de solicitud:**
        /// ```json
        /// {
        ///   "id": 3,
        ///   "nombre": "Allan Bryan",
        ///   "apellido": "Valdez",
        ///   "username": "allanxdtl",
        ///   "password": "nuevaClave123"
        /// }
        /// ```
        /// 
        /// **Respuestas posibles:**
        /// - ✅ **200 OK** — Usuario actualizado correctamente.  
        /// - ⚠️ **409 Conflict** — No se pudo actualizar el usuario.  
        /// </remarks>
        /// <param name="usuario">Objeto con los datos actualizados del usuario.</param>
        /// <response code="200">Usuario actualizado correctamente.</response>
        /// <response code="409">No se pudo actualizar el usuario.</response>
        /// <returns>Mensaje indicando si la actualización fue exitosa.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromBody] Usuario usuario)
        {
            bool result = await _service.ActualizarUsuario(usuario);
            return result ? Ok(new { message = "Usuario actualizado" }) :
                            Conflict(new { message = "No se pudo actualizar el usuario" });
        }
    }
}
