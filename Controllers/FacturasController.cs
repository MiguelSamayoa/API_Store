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

            if (facturas.Count == 0) return NotFound();
            return Ok(facturas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Factura>>> GetFacturas(int id)
        {
            Factura facturas = await facturasServices.GetFacturas(id);

            if (facturas == null) return NotFound();
            return Ok(facturas);
        }

        [HttpGet("WithDetalle")]
        public async Task<ActionResult<List<FacturaConDetalle>>> GetFacturasWithDetalle()
        {
            List<FacturaConDetalle> facturas = await facturasServices.GetFacturasConDetalle();

            if (facturas.Count == 0) return NotFound();
            return Ok(facturas);
        }

        [HttpGet("WithDetalle/{id}")]
        public async Task<ActionResult<List<FacturaConDetalle>>> GetFacturasWithDetalleByID(int id)
        {
            FacturaConDetalle facturas = await facturasServices.GetFacturasConDetalle(id);

            if (facturas == null) return NotFound();
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
