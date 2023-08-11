using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CierreCajaController :ControllerBase
    {
        private readonly string connectionString;

        public CierreCajaController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AperturaCaja>>> GetCliente()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var cierres = connection.Query<AperturaCaja>("sp_VerCierres", commandType: CommandType.StoredProcedure).ToList();

                if (cierres == null || cierres.Count == 0)
                {
                    return NotFound();
                }

                return cierres;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostCierreCaja(CierreCaja cierreCaja)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new
                    {
                        Id_Empleado = cierreCaja.Id_Empleado,
                        Fecha_Hora = cierreCaja.Fecha_Hora,
                        Saldo = cierreCaja.Saldo,
                        Id_apertura = cierreCaja.Id_apertura
                    };

                    await connection.ExecuteAsync("sp_Cierre_Caja", parameters, commandType: CommandType.StoredProcedure);

                    return Ok();
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
