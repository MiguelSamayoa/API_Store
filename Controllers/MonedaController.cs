using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : Controller
    {
        private readonly IMonedaServices serviciomoneda;

        public MonedaController(IMonedaServices Serviciomoneda)//crear el constructor y luego ctrl. "asignar campo"
        {
            serviciomoneda = Serviciomoneda;
        }

        [HttpGet]
        public async Task<ActionResult<List<moneda>>> GetMonedas()
        {
            List<moneda> lista = await serviciomoneda.GetTipoMoneda();

            if (lista == null) return NotFound("no sale chino xd");

            return Ok(lista);
        }
    }
}
