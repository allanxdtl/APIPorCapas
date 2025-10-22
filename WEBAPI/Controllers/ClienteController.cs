using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEBAPI.DTOs;
using WEBAPI.Models;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public readonly IClienteService _service;
        public readonly IMapper _mapper;

        public ClienteController(IClienteService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerClientes());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDto cliente)
        {
            var clienteNuevo = _mapper.Map<Cliente>(cliente);
            bool result;
            try
            {
                result = await _service.CrearCliente(clienteNuevo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }

            return result ? Ok(new { message = "Cliente creado correctamente" }) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClienteDto cliente)
        {
            Cliente clienteActualizado = _mapper.Map<Cliente>(cliente);

            bool result;
            try
            {
                result = await _service.ActualizarCliente(clienteActualizado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }

            return Ok(new { message = "Cliente actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result;
            try
            {
                result = await _service.EliminarCliente(id);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            return result ? Ok(new { message = "Cliente eliminado correctamente" }) : BadRequest();
        }
    }
}