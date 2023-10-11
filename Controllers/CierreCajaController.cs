using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Services;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CierreCajaController :ControllerBase
    {
        private readonly ICierreCajaServices cierreCajaServices;

        public CierreCajaController(ICierreCajaServices CierreCajaService)
        {
            cierreCajaServices = CierreCajaService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CierreCaja>>> GetCliente()
        {
            List<CierreCaja> lista = await cierreCajaServices.GetCierreCajas();

            if (lista == null) return NotFound("soy gay xd");

            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult> PostCierreCaja(CierreCajaDTO cierreCaja)
        {
            CierreCaja cierre = await cierreCajaServices.PostCierreCaja(cierreCaja);

            if (cierre == null) return NotFound("Not found, sorry psd, i´m gay");

            return Ok(cierre);
        }


    }
}
