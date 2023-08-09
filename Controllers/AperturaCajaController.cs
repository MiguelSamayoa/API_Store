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
                var Aperturas = await connection.QueryAsync<AperturaCaja>("EXECUTE sp_VerAperturas", commandType: CommandType.StoredProcedure);
                
                if (Aperturas == null)
                {
                    return NotFound();
                }
                return Ok(Aperturas);

            }
        }

    }
}
