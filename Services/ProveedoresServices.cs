using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;

namespace DesarrolloWeb.Services
{
    public interface IProveedoresServices
    {
        public Task<List<Proveedor>> GetProveedoresAsync();
    }
    //------------------------------------------------------------------------------------------------------------

    public class ProveedoresServicesWithDapper : IProveedoresServices
    {
        private string connectionString;
        public ProveedoresServicesWithDapper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Proveedor>> GetProveedoresAsync()
        {
            using(var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                List<Proveedor> proveedores = ( await conexion.QueryAsync<Proveedor>(" SELECT * FROM proveedor ")).ToList();

                if (proveedores.Count != 0) return proveedores;

                return proveedores;
            }
        }
    }
}
