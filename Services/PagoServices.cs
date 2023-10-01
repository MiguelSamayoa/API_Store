using System.Data;
using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;

namespace DesarrolloWeb.Services
{
    public interface IPagoServices
    {
        public Task<pago> PostPago( PagoDTO DataPago );
    }

    public class PagoServicesWhitDapper : IPagoServices
    {
        private readonly string ConnectionString;
        public PagoServicesWhitDapper( IConfiguration configuracion )
        {
            ConnectionString = configuracion.GetConnectionString("DefaultConnection");
        }
        public async Task<pago> PostPago(PagoDTO DataPago)
        {
            using var conexion = new SqlConnection( ConnectionString );
            conexion.Open();

            pago Pago = (await conexion.QueryAsync<pago>("SP_NuevoPago", DataPago, commandType: CommandType.StoredProcedure)).FirstOrDefault();

            return Pago;
        }
    }
}
