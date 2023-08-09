using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

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

        [HttpGet]
        public async Task<ActionResult> GetEmpleado( string correo, string contraseña) {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define el nombre del procedimiento almacenado
                string procedureName = "SP_LoginEmpleado";

                // Crea el objeto anónimo con los parámetros necesarios
                var parameters = new
                {
                    correo = correo,
                    password = contraseña
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
