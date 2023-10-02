using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Client;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController:Controller
    {
        private readonly IPagoServices pagoServices;

        public PagoController( IPagoServices pagoServices)
        {
            this.pagoServices = pagoServices;
        }

        [HttpPost]
        public async Task<ActionResult<pago>> PostPago( PagoDTO pago )
        {
            pago Pago = await pagoServices.PostPago( pago );

            if (Pago == null) return NotFound();

            return Ok(Pago);
        }
    }
}
