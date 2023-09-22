using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;

namespace DesarrolloWeb.Services
{
    public interface IEmpleadoService
    {
        public Task<Empleado> AutenticarEmpleado(EmpleadoLogin empleado);
    }

    // ---------------------------------------------------------------------------------
    public class EmpleadoServicesWithDapper : IEmpleadoService
    {
        private string connectionString;
        public EmpleadoServicesWithDapper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public Task<bool> ActualizarEmpleado(Empleado usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Empleado> AutenticarEmpleado(EmpleadoLogin empleado)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var parameters = new { 
                    empleado.Correo, 
                    empleado.Password };

                var usuarios = await connection.QueryAsync<Empleado>("SP_LoginEmpleado", 
                                    parameters,
                                    commandType: System.Data.CommandType.StoredProcedure);

                if (usuarios != null) return usuarios.FirstOrDefault();
                return null;
            };
            
        }
    }
}
