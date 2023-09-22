using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;

namespace DesarrolloWeb.Services
{
    public interface ITipo_ProductoServices
    {
        public Task<List<TipoProducto>> GetAll();
        public Task<TipoProducto> GetTipoById(int id);
    }
    public class TipoProductoServicesWithDapper : ITipo_ProductoServices
    {
        private string connectionString;
        public TipoProductoServicesWithDapper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<TipoProducto>> GetAll()
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                List<TipoProducto> tipoProductos = (await conexion.QueryAsync<TipoProducto>("SELECT * FROM Tipo_Producto")).ToList();

                if(tipoProductos != null) return tipoProductos;
                return null;
            }
        }

        public async Task<TipoProducto> GetTipoById(int id)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                TipoProducto tipoProductos = (await conexion.QueryAsync<TipoProducto>($"SELECT * FROM Tipo_Producto WHERE Id_Tipo_Producto = {id}")).FirstOrDefault();

                if (tipoProductos != null) return tipoProductos;
                return null;
            }
        }

    }
}
