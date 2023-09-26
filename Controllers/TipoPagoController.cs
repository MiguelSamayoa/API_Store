using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPagoController : Controller
    {
        private readonly ITipoDePagoServices tipoDePagoServices;

        public TipoPagoController(ITipoDePagoServices TipoDePagoServices)
        {
            tipoDePagoServices = TipoDePagoServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<tipo_pago>>> GetTipo_Pago()
        {
            List<tipo_pago> lista = await tipoDePagoServices.GetTipo_Pagos();

            if (lista == null) return NotFound("Cinga tu madre x2");

            return Ok(lista);
        }
    }
}
