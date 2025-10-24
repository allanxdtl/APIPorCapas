using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.DTOs;
using WEBAPI.Models;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(IProductoService service, IMapper mapper, ILogger<ProductoController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Obteniene lista de productos del sistema
        /// </summary>
        /// <returns>Lista de productos</returns>
        /// <response code="200">Lista de productos devuelta correctamente</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerProductosAsync());
        }

        /// <summary>
        /// Crea un nuevo producto en el sistema.
        /// </summary>
        /// <param name="producto">
        /// Objeto <see cref="ProductoDto"/> que contiene la información del producto a registrar.
        /// Debe incluir los campos requeridos como código de barras, descripción, precio y categoría.
        /// </param>
        /// <returns>
        /// Devuelve una respuesta HTTP con el resultado de la operación.
        /// </returns>
        /// <response code="200">El producto fue insertado correctamente en la base de datos.</response>
        /// <response code="400">Los datos enviados son inválidos o ocurrió un error durante la inserción.</response>
        /// <response code="409">Ya existe un producto con el mismo código de barras en el sistema.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] ProductoDto producto)
        {
            try
            {
                Producto productoInsert = _mapper.Map<Producto>(producto);
                bool result = await _service.CrearProductoAsync(productoInsert);

                return result
                    ? Ok(new { message = "Producto insertado correctamente" })
                    : BadRequest(new { message = "Ocurrió un error al insertar el producto." });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un producto del sistema
        /// </summary>
        /// <param name="producto">Objeto de tipo Producto</param>
        /// <returns>Respuesta HTTP</returns>
        /// <response code="200">Producto actualizado correctamente</response>
        /// <response code="409">El codigo del producto ya existe en el sistema o el precio es menor o igual a 0</response>
        /// <response code="500">Error general en el servidor</response>
        [HttpPut]
        public async Task<IActionResult> Put(ProductoDto producto)
        {
            try
            {
                Producto productoUpdate = _mapper.Map<Producto>(producto);
                bool result = await _service.ActualizarProductoAsync(productoUpdate);
                return result ?
                    Ok(new { message = "Producto actualizado correctamente" })
                        :
                    Conflict(new { message = "El codigo esta repetido o el precio es menor a 0" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        /// <summary>
        /// Elimina un producto del sistema
        /// </summary>
        /// <param name="barcode">Codigo de barras</param>
        /// <returns>Codigo HTTP como respuesta de la api</returns>
        /// <response code="200">Producto eliminado correctamente</response>
        /// <response code="409">No se pudo eliminar el producto</response>
        /// <response code="404">Producto no encontrado</response>
        /// <response code="500">Error en el servidor</response>
        [HttpDelete("{barcode}")]
        public async Task<IActionResult> Delete(string barcode)
        {
            try
            {
                bool response = await _service.EliminarProductoAsync(barcode);
                return response ? Ok(new { message = "El producto fue eliminado correctamente" })
                    : Conflict("No se ha podido eliminar");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "No ha sido posible conectase a la base de datos, favor de contactar al administrador");
            }
        }
    }
}
