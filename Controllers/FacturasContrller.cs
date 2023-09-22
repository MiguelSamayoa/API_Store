using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesarrolloWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : Controller
    {
        private IFacturasServices facturasServices;

        public FacturasController(IFacturasServices FacturasServices)
        {
            facturasServices = FacturasServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Factura>>> GetFacturas()
        {
            List<Factura> facturas = await facturasServices.GetFacturas();

            if(facturas.Count == 0) return NotFound();
            return Ok(facturas);
        }

        [HttpGet("WithDetalle")]
        public async Task<ActionResult<List<object>>> GetFacturasWithDetalle()
        {
            List<FacturaConDetalle> facturas = await facturasServices.GetFacturasConDetalle();

            if (facturas.Count == 0) return NotFound();
            return Ok(facturas);
        }

        [HttpPost]
        public async Task<ActionResult<FacturaConDetalle>> PostFactura(CreacionFacturaWithDetalleDTO Factura)
        {
            FacturaConDetalle factura = await facturasServices.PostFactura(Factura);
            return factura;
        }

    }
}
