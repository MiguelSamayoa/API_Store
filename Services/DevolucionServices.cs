
using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace DesarrolloWeb.Services
{
    public interface IDevolucion {
        public Task<List<Devolucion>> GetDevolucion();
        public Task<List<Devolucion>> GetDevolucionxId(int id);
        public Task<Devolucion> PostDevolucion(DevolucionDTO devolucion);
    }
    
    public class DevolucionServicesWithDapper : IDevolucion
    {
        private readonly string ConnectionString;
        public DevolucionServicesWithDapper(IConfiguration configuracion)
        {
            ConnectionString = configuracion.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Devolucion>> GetDevolucion()
        {
            using var conexion = new SqlConnection(ConnectionString);

            conexion.Open();

            List<Devolucion> Devoluciones = (await conexion.
                                           QueryAsync<Devolucion>("sp_verDevoluciones", commandType: CommandType.StoredProcedure))
                                           .ToList();

            return Devoluciones;
        }



        public async Task<List<Devolucion>> GetDevolucionxId(int id)
        {
            using var conexion = new SqlConnection(ConnectionString);

            conexion.Open();

            List<Devolucion> Devoluciones = (await conexion.
                                           QueryAsync<Devolucion>("sp_verDevolucionesXEmpleado", new { id }, commandType: CommandType.StoredProcedure))
                                           .ToList();

            return Devoluciones;
        }


        public async Task<Devolucion> PostDevolucion(DevolucionDTO devolucion)
        {
            using var conexion = new SqlConnection(ConnectionString);
            conexion.Open();

            Devolucion devoluciones = (await conexion.QueryAsync<Devolucion>("InsertarDevolucion", devolucion, commandType: CommandType.StoredProcedure)).FirstOrDefault();

            return devoluciones;

        }

        
    }
}
