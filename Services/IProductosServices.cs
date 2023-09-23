using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DesarrolloWeb.Services
{
    public interface IProductosServices
    {
        public Task<List<Producto>> getProductos();

        public Task<Producto> GetProducto(int Id_Producto);

        public Task<Producto> setProducto(Producto producto);

        public Task<List<Producto>> getProductosWithData();
    }

    //*---------------------------------------------------

    public class ProductoServicioWhithDapper : IProductosServices
    {
        private string connectionString;

        public ProductoServicioWhithDapper( IConfiguration configuration )
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Producto>> getProductos()
        {
            using(SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                var products = (await conexion.QueryAsync<Producto>("SP_VerProducto", 
                                                    commandType: CommandType.StoredProcedure)).ToList();

                return products;
            }            
        }

        public async Task<Producto> GetProducto(int Id_Producto)
        {
            using(SqlConnection conexion = new SqlConnection( connectionString ) )
            {
                conexion.Open();

                Producto producto = (await conexion.QueryAsync<Producto>("sp_BuscarProductoPorId",
                                                    new { Id_Producto },
                                                    commandType: CommandType.StoredProcedure))
                                                    .FirstOrDefault();

                return producto;
            }
        }

        public async Task<Producto> setProducto( Producto producto )
        {
            using ( var connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                var parametros = new
                {
                    producto.id_Tipo_Producto,
                    producto.nombre_Producto,
                    producto.precio_Producto,
                    producto.Stock,
                    producto.id_proveedor,
                    producto.disponibilidad,
                    producto.Id_Empleado
                };
                Producto ProductoIngresado = (await connection.QueryAsync<Producto>( "sp_IngresoProducto", 
                                                            parametros, 
                                                            commandType: CommandType.StoredProcedure )).FirstOrDefault();

                return ProductoIngresado;
            }
        }

        public async Task<List<Producto>> getProductosWithData()
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                var products = (await conexion.QueryAsync<Producto>("Sp_VerProductosWithProveedor",
                                                    commandType: CommandType.StoredProcedure)).ToList();

                return products;
            }
        }
    }
}
