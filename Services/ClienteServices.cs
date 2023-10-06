using AutoMapper;
using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DesarrolloWeb.Services
{
    public interface IClienteServices
    {
        public Task<List<PersonaGenericoOut>> GetCliente();
        public Task<PersonaGenericoOut> GetClienteID(int Id);
        public Task<PersonaGenericoOut> PostCliente(PersonaGenericoOut cliente);
    }


    public class ClienteServicesWithDapper : IClienteServices
    {
        private readonly string connectionString;
        private readonly IMapper mapper;

        public ClienteServicesWithDapper(IConfiguration configuration, IMapper mapper)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.mapper = mapper;
        }



        public async Task<List<PersonaGenericoOut>> GetCliente()
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var clientes = (await connection.QueryAsync<Cliente>("SP_VerTodosClientes",
                commandType: CommandType.StoredProcedure)).ToList();


            List<PersonaGenericoOut> p = mapper.Map<List<PersonaGenericoOut>>(clientes);
            return p;
        }

        public async Task<PersonaGenericoOut> GetClienteID(int Id)
        {
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var parameters = new
            {
                id_empleado = Id
            };

            var clientes = (await connection.QueryAsync<Cliente>($"sp_BuscarClientePorID", parameters,
                commandType: CommandType.StoredProcedure)).FirstOrDefault();

            PersonaGenericoOut p = mapper.Map<PersonaGenericoOut>(clientes);

            return p;
        }
    

        public async Task<PersonaGenericoOut> PostCliente(PersonaGenericoOut cliente)
        {

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            var Cliente = new {
                Nombre_Cliente = cliente.Nombre,
                apellido_cliente = "",
                dpi_cliente = cliente.dpi,
                telefono_cliente = cliente.Telefono,
                Nit= cliente.Nit,
                Correo_Cliente = cliente.Correo,
                Direccion = cliente.Direccion
            };

            var lista = (await connection.QueryAsync<Cliente>("SP_InsertarCliente", Cliente, commandType: CommandType.StoredProcedure)).FirstOrDefault();



            PersonaGenericoOut p = mapper.Map<PersonaGenericoOut>(lista);
            return p;
        }
    }
}
