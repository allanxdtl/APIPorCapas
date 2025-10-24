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
    public class VentasController : ControllerBase
    {
        private readonly IVentasServices _service;
        private readonly IMapper _mapper;

        public VentasController(IVentasServices service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerTodasLasVentasAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenta([FromBody] VentaDto venta)
        {
            VentaHeader header = _mapper.Map<VentaHeader>(venta);

            await _service.CrearVentaAsync(header, header.Detalles.ToList());

            return Ok("Venta registrada");
        }
    }
}
