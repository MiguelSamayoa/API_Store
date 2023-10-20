using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Text;
namespace DesarrolloWeb.Services
{
    public interface IFacturasServices
    {
        public Task<List<Factura>> GetFacturas();
        public Task<List<FacturaConDetalleWithData>> GetFacturasConDetalle();
        public Task<Factura> GetFacturas(int id);
        public Task<FacturaConDetalleWithData> GetFacturasConDetalle(int id);

        public Task<List<FacturaConDetalleWithData>> GetFacturasToday();

        public Task<FacturaConDetalleWithData> PostFactura(CreacionFacturaWithDetalleDTO Factura );
    }

    public class FacturasServicesWhithDapper : IFacturasServices
    {
        private readonly string ConnectionString;

        public FacturasServicesWhithDapper( IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        static string GenerateRandomAlphaNumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                result.Append(chars[index]);
            }

            return result.ToString();
        }

        public async Task<List<Factura>> GetFacturas()
        {
            using var conexion = new SqlConnection( ConnectionString );

            List<Factura> facturas = (await conexion.QueryAsync<Factura>("select * from Factura")).ToList();

            return facturas;
        }

        public async Task<List<FacturaConDetalleWithData>> GetFacturasConDetalle()
        {
            using var conexion = new SqlConnection(ConnectionString);
            List<FacturaConDetalleWithData> facturaConDetalles = new List<FacturaConDetalleWithData>();
            List<Factura> facturas = (await conexion.QueryAsync<Factura>("select * from Factura")).ToList();


            foreach(var factura in facturas)
            {
                List<DetalleWithDataDTO> detalles = (await conexion.QueryAsync<DetalleWithDataDTO>("Sp_VerDetallesByIDwhithProducto",
                    new { id = factura.No_Factura }, commandType: CommandType.StoredProcedure))
                    .ToList();

                if (detalles.Count > 0) facturaConDetalles.Add(new FacturaConDetalleWithData(factura, detalles));
            }
            return facturaConDetalles;
        }

        public async Task<Factura> GetFacturas(int id)
        {
            using var conexion = new SqlConnection(ConnectionString);

            Factura facturas = (await conexion.QueryAsync<Factura>($"select * from Factura where No_Factura = '{id}'")).First();

            return facturas;
        }

        public async Task<FacturaConDetalleWithData> GetFacturasConDetalle(int id)
        {
            using var conexion = new SqlConnection(ConnectionString);
            FacturaConDetalleWithData facturaConDetalles = new FacturaConDetalleWithData();
            Factura factura = (await conexion.QueryAsync<Factura>($"select * from Factura where No_Factura = '{id}'")).First();
            List<DetalleWithDataDTO> detalles = (await conexion.QueryAsync<DetalleWithDataDTO>("Sp_VerDetallesByIDwhithProducto",
                new { id }, commandType: CommandType.StoredProcedure))
                .ToList();

            if (detalles.Count > 0) facturaConDetalles = new FacturaConDetalleWithData(factura, detalles);

            return facturaConDetalles;
        }



        public async Task<FacturaConDetalleWithData> PostFactura(CreacionFacturaWithDetalleDTO Factura)
        {
            using var conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            Decimal i = 0;
            List<Decimal> listaTotales = new List<Decimal>();
            foreach (var item in Factura.Detalles)
            {

                Producto j = (await conexion.QueryAsync<Producto>($"select * from Producto where Id_Producto = {item.Id_Producto}")).FirstOrDefault();

                listaTotales.Add((j.precio_Producto * item.Cantidad));
                i += j.precio_Producto * item.Cantidad;
            }

            var parametros = new
            {
                Cliente = Factura.factura.id_cliente,
                Empleado = Factura.factura.Id_Empleado,
                Serie = GenerateRandomAlphaNumericString(10),
                CostoTotal = i
            };
            Factura facturaInsertada = (await conexion.QueryAsync<Factura>("SpInsertarFactura", parametros, commandType: CommandType.StoredProcedure)).First();

            foreach (var item in Factura.Detalles)
            {

                var p = new
                {
                    orden = facturaInsertada.No_Factura,
                    Cantidad = item.Cantidad,
                    articulo = item.Id_Producto,
                    PrecioTotal = listaTotales[Factura.Detalles.IndexOf(item)]
                };

                //Chinga tu madre xd
                var j = (await conexion.QueryAsync("InsertarDetalleFactura", p, commandType: CommandType.StoredProcedure));

            }

            return await GetFacturasConDetalle(facturaInsertada.No_Factura);
        }





        public async Task<List<FacturaConDetalleWithData>> GetFacturasToday()
        {
            using var conexion = new SqlConnection(ConnectionString);
            List<FacturaConDetalleWithData> facturaConDetalles = new List<FacturaConDetalleWithData>();
            List<Factura> facturas = (await conexion.QueryAsync<Factura>("SP_GetFacturasToday", commandType: CommandType.StoredProcedure)).ToList();

            if (facturas.Count() > 0)
            {
                foreach (var fac in facturas)
                {
                    List<DetalleWithDataDTO> detalles = (await conexion.QueryAsync<DetalleWithDataDTO>("Sp_VerDetallesByIDwhithProducto", new { id = fac.No_Factura }, commandType: CommandType.StoredProcedure))
                    .ToList();

                    facturaConDetalles.Add(new FacturaConDetalleWithData(fac, detalles));
                }
            }
            return facturaConDetalles;
        }
    }
}
