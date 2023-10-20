using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DesarrolloWeb.Services
{
    public interface ICierreCajaServices {
        public Task<List<CierreCaja>> GetCierreCajas();
        public void PostCierreCaja(CierreCajaDTO CierreCaja);
    }
    public class CierreCajaServicesWithDapper : ICierreCajaServices
    {
        private readonly string ConnectionString;
        public CierreCajaServicesWithDapper(IConfiguration configuracion)
        {
            ConnectionString = configuracion.GetConnectionString("DefaultConnection");
        }


        public async Task<List<CierreCaja>> GetCierreCajas()
        {
            using var conexion = new SqlConnection(ConnectionString);
            conexion.Open();

            List<CierreCaja> cierre = (await conexion.
                                            QueryAsync<CierreCaja>("sp_VerCierres", commandType: CommandType.StoredProcedure))
                                            .ToList();

            return cierre;
        }


        public async void PostCierreCaja(CierreCajaDTO CierreCaja)
        {
            using var conexion = new SqlConnection(ConnectionString);
            conexion.Open();

            CierreCaja cierre = (await conexion.QueryAsync<CierreCaja>("sp_Cierre_Caja", CierreCaja, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        }
    }
} 
