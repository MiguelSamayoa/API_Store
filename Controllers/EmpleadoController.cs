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
    public class EmpleadoController : ControllerBase
    {

        private readonly string connectionString;

        public EmpleadoController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("{Correo}/{password}")]
        public async Task<ActionResult> GetEmpleado( string Correo, string password) {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define el nombre del procedimiento almacenado
                string procedureName = "SP_LoginEmpleado";

                // Crea el objeto anónimo con los parámetros necesarios
                var parameters = new
                {
                    correo = Correo,
                    password = password
                };

                // Ejecuta el procedimiento almacenado y mapea los resultados a un objeto Empleado
                var empleado = await connection.QueryFirstOrDefaultAsync<Empleado>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                if (empleado == null)
                {
                    return NotFound();
                }

                return Ok(empleado);
            }
        }
    }
}
