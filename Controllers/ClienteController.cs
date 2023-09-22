using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;
using AutoMapper;
using DesarrolloWeb.DTOs;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController:ControllerBase
    {
        private readonly string connectionString;
        private readonly IMapper mapper;

        public ClienteController(IConfiguration configuration, IMapper mapper)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaGenericoOut>>> GetCliente()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var clientes = (await connection.QueryAsync<Cliente>("SP_VerTodosClientes", 
                    commandType: CommandType.StoredProcedure)).ToList();

                if (clientes == null || clientes.Count == 0)
                {
                    return NotFound();
                }

                List<PersonaGenericoOut> p =  mapper.Map<List<PersonaGenericoOut>>(clientes);
                return Ok(p);
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

                var clientes = (await connection.QueryAsync<Cliente>($"sp_BuscarClientePorID", parameters,
                    commandType: CommandType.StoredProcedure)).ToList();

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

                    var lista = (await connection.QueryAsync<Cliente>("SP_InsertarCliente", parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();

  
                    if (lista != null)
                    {
                        return lista;
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
