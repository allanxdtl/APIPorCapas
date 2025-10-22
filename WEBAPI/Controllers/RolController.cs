using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.DTOs;
using WEBAPI.Models;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Controllers
{
    /// <summary>
    /// Controlador encargado de manejar las operaciones relacionadas con los roles del sistema.
    /// </summary>
    /// <remarks>
    /// Este controlador expone endpoints para **consultar**, **crear**, **editar** y **eliminar** roles dentro del sistema.
    /// 
    /// Ejemplo de objeto Role:
    /// ```json
    /// {
    ///   "id": 1,
    ///   "descripcion": "Administrador"
    /// }
    /// ```
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<RolController> _logger;

        /// <summary>
        /// Constructor del controlador con inyección de dependencia del servicio de roles.
        /// </summary>
        /// <param name="service">Instancia del servicio de roles.</param>
        /// <param name="mapper">Instancia del mapeador</param>
        /// <param name="logger">Instancia del logger</param>
        public RolController(IRolService service, IMapper mapper, ILogger<RolController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        // ---------------------------------------------------------------------------------------
        // GET: api/Rol
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// Obtiene la lista completa de roles registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve todos los roles disponibles.  
        /// 
        /// **Ejemplo de respuesta:**
        /// ```json
        /// [
        ///   { "id": 1, "descripcion": "Administrador" },
        ///   { "id": 2, "descripcion": "Cajero" }
        /// ]
        /// ```
        /// </remarks>
        /// <response code="200">Lista de roles obtenida correctamente.</response>
        /// <response code="500">Error interno al obtener los datos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerRoles());
        }

        // ---------------------------------------------------------------------------------------
        // POST: api/Rol
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// Crea un nuevo rol en el sistema.
        /// </summary>
        /// <remarks>
        /// Inserta un nuevo rol en la base de datos.  
        /// 
        /// **Ejemplo de solicitud:**
        /// ```json
        /// {
        ///   "descripcion": "Supervisor"
        /// }
        /// ```
        /// </remarks>
        /// <param name="rol">Objeto <see cref="RolDto"/> con la descripción del nuevo rol.</param>
        /// <response code="200">Rol creado exitosamente.</response>
        /// <response code="400">No se pudo crear el rol.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(RolDto rol)
        {

            var rolModel = _mapper.Map<Role>(rol);

            bool result = await _service.CrearRol(rolModel);

            return result ? Ok(new { message = "Rol creado exitosamente" }) :
                            BadRequest(new { message = "El rol no ha podido ser creado" });
        }

        // ---------------------------------------------------------------------------------------
        // PUT: api/Rol
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un rol existente en el sistema.
        /// </summary>
        /// <remarks>
        /// Permite modificar la descripción de un rol ya existente.  
        /// 
        /// **Ejemplo de solicitud:**
        /// ```json
        /// {
        ///   "id": 2,
        ///   "descripcion": "Cajero principal"
        /// }
        /// ```
        /// </remarks>
        /// <param name="rol">Objeto <see cref="RolDto"/> con los datos actualizados.</param>
        /// <response code="200">Rol editado exitosamente.</response>
        /// <response code="400">Error al completar la petición.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(RolDto rol)
        {
            var rolModel = _mapper.Map<Role>(rol);

            bool result = await _service.EditarRol(rolModel);

            return result ? Ok(new { message = "Rol editado exitosamente" }) : BadRequest(new { message = "Error al completar la peticion" });
        }

        // ---------------------------------------------------------------------------------------
        // DELETE: api/Rol/{id}
        // ---------------------------------------------------------------------------------------
        /// <summary>
        /// Elimina un rol existente por su identificador.
        /// </summary>
        /// <remarks>
        /// Este endpoint elimina un rol de la base de datos usando su ID.  
        /// 
        /// **Ejemplo de solicitud:**
        /// ```
        /// DELETE /api/Rol/1
        /// ```
        /// </remarks>
        /// <param name="id">Identificador del rol a eliminar.</param>
        /// <response code="200">Rol eliminado exitosamente.</response>
        /// <response code="400">Error al completar la petición.</response>
        /// <response code="500">Error en el servidor</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _service.BorrarRol(id);

                return result
                    ? Ok(new { message = "Rol eliminado exitosamente" })
                    : BadRequest(new { message = "El rol no existe o ya fue eliminado" });
            }
            catch (InvalidOperationException ex)
            {
                // Error por integridad referencial
                _logger.LogError("{error}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("{error}", ex.Message);
                // Cualquier otro error inesperado
                return StatusCode(500, new { message = "Ocurrió un error inesperado" });
            }
        }
    }
}
