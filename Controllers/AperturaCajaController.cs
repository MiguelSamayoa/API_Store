using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using DesarrolloWeb.Services;
using DesarrolloWeb.DTOs;

namespace DesarrolloWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AperturaCajaController:ControllerBase
	{
        private readonly IAperturaCajaServices aperturaServices;

        public AperturaCajaController( IAperturaCajaServices ApeerturaServices )
		{
            aperturaServices = ApeerturaServices;
        }

		[HttpGet]
		public async Task<ActionResult<List<AperturaCaja>>> GetAperturas()
		{
			List<AperturaCaja> lista = await aperturaServices.GetAperturaCaja();

			if(lista == null) return NotFound("Cinga tu madre xd");

			return Ok(lista);
        }

		[HttpPost]
		public async Task<ActionResult<AperturaCaja>> PostAperturaCaja(AperturaCajaDTO aperturaCaja)
		{
			AperturaCaja apertura = await aperturaServices.PostApertura(aperturaCaja);

			if(apertura == null) return NotFound("Chinga tu madre");

			return Ok(apertura);
        }
	}
}
