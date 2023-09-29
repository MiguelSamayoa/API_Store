using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DesarrolloWeb.Services
{
    public interface IMonedaServices {
        public Task<List<moneda>> GetTipoMoneda();
    }
    public class MonedaServicesWhitDapper : IMonedaServices
    {
        private readonly string ConnectionString;

        public MonedaServicesWhitDapper(IConfiguration configuracion)
        {
            ConnectionString = configuracion.GetConnectionString("DefaultConnection");
        }

        public async Task<List<moneda>> GetTipoMoneda()
        {
            using var conexion = new SqlConnection(ConnectionString);

            conexion.Open();

            List<moneda> monedas = (await conexion.
                                            QueryAsync<moneda>("SP_VerNMonedas", commandType: CommandType.StoredProcedure))
                                            .ToList();

            return monedas;

        }

    }
}
