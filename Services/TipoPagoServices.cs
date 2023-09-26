using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DesarrolloWeb.Services
{
    public interface ITipoDePagoServices
    {
        public Task<List<tipo_pago>> GetTipo_Pagos();
    }
    public class TipoPagoServicesWhithDapper : ITipoDePagoServices
    {
        private readonly string ConnectionString;
        public TipoPagoServicesWhithDapper (IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<tipo_pago>> GetTipo_Pagos()
        {
            using var conexion = new SqlConnection(ConnectionString);
            conexion.Open();

            List<tipo_pago> tipo_Pagos = (await conexion.
                                            QueryAsync<tipo_pago>("select * from tipo_pago"))
                                            .ToList();
            return tipo_Pagos;
        }

    }
}
