using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucionController : Controller
    {
        private readonly IDevolucion DevolucionServices;

        public DevolucionController(IDevolucion devolucionServices)
        {
            DevolucionServices = devolucionServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Devolucion>>> GetDevolucion()
        {
            List<Devolucion> lista = await DevolucionServices.GetDevolucion();

            if (lista == null) return NotFound("no me sale, soy gay");

            return Ok(lista);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Devolucion>>> GetDevolucionxId(int id)
        {
            List<Devolucion> lista = await DevolucionServices.GetDevolucionxId(id);

            if (lista == null) return NotFound("no me sale, soy gay");

            return Ok(lista);
        }

    }
}
