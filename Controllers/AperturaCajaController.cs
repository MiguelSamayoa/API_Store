using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DesarrolloWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AperturaCajaController:ControllerBase
	{
		private readonly string connectionString;

		public AperturaCajaController(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		[HttpGet]
		public async Task<ActionResult<List<AperturaCaja>>> GetAperturas()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var Aperturas = await connection.QueryAsync<AperturaCaja>("sp_VerAperturas", commandType: CommandType.StoredProcedure);
				
				if (Aperturas == null)
				{
					return NotFound();
				}
				return Ok(Aperturas);

			}
		}

		[HttpPost]
		public async Task<ActionResult<AperturaCaja>> PostAperturaCaja(AperturaCaja aperturaCaja)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					
					var parameters = new
					{
						Id_Empleado = aperturaCaja.Id_Empleado,
						Fecha_Hora = aperturaCaja.Fecha_Hora,
						Saldo = aperturaCaja.Saldo
					};

					await connection.ExecuteAsync("sp_AperturaCaja", parameters, commandType: CommandType.StoredProcedure);

					var lista = (await connection.QueryAsync<AperturaCaja>("sp_VerUltimaAperturaCaja", commandType: CommandType.StoredProcedure)).ToList();
					if (lista.Count > 0)
					{
						return lista[0];
					}
					else
					{
						return NotFound();
					}
				}
			}
			catch (Exception ex)
			{
				// Manejo de errores
				return StatusCode(500, ex.Message);
			}
		}


	}
}
