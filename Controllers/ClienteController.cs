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
    public class ClienteController:ControllerBase
    {
        private readonly string connectionString;

        public ClienteController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var clientes = connection.Query<Cliente>("SP_VerTodosClientes", commandType: CommandType.StoredProcedure).ToList();

                if (clientes == null || clientes.Count == 0)
                {
                    return NotFound();
                }

                return clientes;
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienteID(int Id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    id_empleado = Id
                };

                var clientes = connection.Query<Cliente>($"sp_BuscarClientePorID", parameters, commandType: CommandType.StoredProcedure).ToList();

                if (clientes == null || clientes.Count == 0)
                {
                    return NotFound();
                }

                return clientes;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new
                    {
                        Nombre_Cliente = cliente.Nombre_Cliente,
                        apellido_cliente = cliente.apellido_cliente,
                        dpi_cliente = cliente.dpi_cliente,
                        Nit = cliente.Nit,
                        telefono_cliente = cliente.telefono_cliente,
                        Correo_Cliente = cliente.Correo_Cliente,
                        Direccion = cliente.Direccion
                    };

                    await connection.ExecuteAsync("SP_InsertarCliente", parameters, commandType: CommandType.StoredProcedure);

                    var lista = (await connection.QueryAsync<Cliente>("GetLastCliente", commandType: CommandType.StoredProcedure)).ToList();
                    if (lista.Count > 0)
                    {
                        return lista[0];
                    }
                    else
                    {
                        return NotFound("Elemento No Encontrdo");
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
