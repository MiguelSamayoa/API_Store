using DesarrolloWeb.DTOs;
using System;
using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.OpenApi.Any;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucionController : Controller
    {
        private readonly IDevolucion DevolucionServices;
        private readonly IFacturasServices facturasServices;

        public DevolucionController(IDevolucion devolucionServices, IFacturasServices facturasServices)
        {
            DevolucionServices = devolucionServices;
            this.facturasServices = facturasServices;
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

            if (lista == null) return NotFound("Casi, echele ganas a la próxima si sale bro");

            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult> PostDevolucion(CreacionFacturaWithDetalleDTO factura)
        {

            List<Devolucion> lista = new List<Devolucion>();
            foreach( var det in factura.Detalles)
            {
                DevolucionDTO devolucion = new DevolucionDTO( det.Id_Detalle, det.Cantidad, factura.factura.Id_Empleado );
                Devolucion Devoluciones = await DevolucionServices.PostDevolucion(devolucion);
                lista.Add(Devoluciones);
            }


            if (lista == null) return NotFound("Not found, como yo que no encuentro su amor :(");


            var f = await facturasServices.PostFactura(factura);
            return Ok(new { Devoluciones = lista, factura = f });
        }


    }
}
