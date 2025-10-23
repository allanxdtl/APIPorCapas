using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;
        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerProductosAsync());
        }
    }
}
