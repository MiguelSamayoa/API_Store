using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DesarrolloWeb.Services
{
    public interface IAperturaCajaServices
    {
        public Task<List<AperturaCaja>> GetAperturaCaja();
        public Task<AperturaCaja> PostApertura( AperturaCajaDTO Apertura );
    }


    public class AperturaCajaServicesWithDappper : IAperturaCajaServices
    {
        private readonly string ConnectionString;
        public AperturaCajaServicesWithDappper( IConfiguration configuracion )
        {
            ConnectionString = configuracion.GetConnectionString("DefaultConnection");
        }


        public async Task<List<AperturaCaja>> GetAperturaCaja()
        {
            using var conexion = new SqlConnection(ConnectionString);

            conexion.Open();

            List<AperturaCaja> aperturas = (await conexion.
                                            QueryAsync<AperturaCaja>("sp_VerAperturas", commandType: CommandType.StoredProcedure))
                                            .ToList();

            return aperturas;
        }


        public async Task<AperturaCaja> PostApertura(AperturaCajaDTO Apertura)
        {
            using var conexion = new SqlConnection(ConnectionString);
            conexion.Open();

            AperturaCaja apertura = (await conexion.QueryAsync<AperturaCaja>("sp_AperturaCaja", Apertura, commandType: CommandType.StoredProcedure)).FirstOrDefault();

            return apertura;
        }
    }


    public class AperturaCajaServiceswithEntity : IAperturaCajaServices
    {
        public Task<List<AperturaCaja>> GetAperturaCaja()
        {
            throw new NotImplementedException();
        }

        public Task<AperturaCaja> PostApertura(AperturaCajaDTO Apertura)
        {
            throw new NotImplementedException();
        }
    }
}
