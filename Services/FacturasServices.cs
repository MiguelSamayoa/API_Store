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
        public Task<List<FacturaConDetalle>> GetFacturasConDetalle();
        public Task<Factura> GetFacturaByID(int id);
        public Task<FacturaConDetalle> GetFacturasConDetalleByID(int id);

        public Task<FacturaConDetalle> PostFactura(CreacionFacturaWithDetalleDTO Factura );
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

        public async Task<List<FacturaConDetalle>> GetFacturasConDetalle()
        {
            using var conexion = new SqlConnection(ConnectionString);
            List<FacturaConDetalle> facturaConDetalles = new List<FacturaConDetalle>();
            List<Factura> facturas = (await conexion.QueryAsync<Factura>("select * from Factura")).ToList();
            List<DetalleFactura> detalles = (await conexion.QueryAsync<DetalleFactura>("select * from Detalle_Factura")).ToList();

            foreach(var factura in facturas)
            {
                List<DetalleFactura> detalleFactura = detalles.Where( det => det.No_Factura == factura.No_Factura ).ToList();
                
                if(detalleFactura.Count > 0) facturaConDetalles.Add(new FacturaConDetalle(factura, detalleFactura));
            }
            return facturaConDetalles;
        }

        public async Task<Factura> GetFacturaByID(int id)
        {
            using var conexion = new SqlConnection(ConnectionString);

            Factura facturas = (await conexion.QueryAsync<Factura>($"select * from Factura where No_Factura = '{id}'")).First();

            return facturas;
        }

        public async Task<FacturaConDetalle> GetFacturasConDetalleByID(int id)
        {
            using var conexion = new SqlConnection(ConnectionString);
            FacturaConDetalle facturaConDetalles = new FacturaConDetalle();
            Factura factura = (await conexion.QueryAsync<Factura>($"select * from Factura where No_Factura = '{id}'")).First();
            List<DetalleFactura> detalles = (await conexion.QueryAsync<DetalleFactura>("select * from Detalle_Factura")).ToList();

            List<DetalleFactura> detalleFactura = detalles.Where(det => det.No_Factura == factura.No_Factura).ToList();

            if (detalleFactura.Count > 0) facturaConDetalles = new FacturaConDetalle(factura, detalleFactura);

            return facturaConDetalles;
        }

        public async Task<FacturaConDetalle> PostFactura(CreacionFacturaWithDetalleDTO Factura)
        {
            using var conexion = new SqlConnection(ConnectionString);

            Decimal i = 0;
            List<string> listaTotales = new List<string>();
            foreach (var item in Factura.Detalles)
            {

                Producto j = (await conexion.QueryAsync<Producto>($"select * from Producto where Id_Producto = {item.Id_Producto}")).FirstOrDefault();

                listaTotales.Add((j.precio_Producto * item.Cantidad).ToString());
                i += j.precio_Producto * item.Cantidad;
            }

            var parametros = new
            {
                Cliente = Factura.factura.id_cliente,
                Empleado = Factura.factura.Id_Empleado,
                Serie = GenerateRandomAlphaNumericString(10),
                CostoTotal = i
            };
            Factura facturaInsertada = (await conexion.QueryAsync<Factura>("", parametros)).First();

            foreach (var item in Factura.Detalles)
            {

                //var p = new
                //{
                //    orden = idOrden.Id,
                //    Cantidad = item.Cantidad,
                //    articulo = item.articulo,
                //    PrecioTotal = listaTotales[ordenCompleta.Detalle.IndexOf(item)]
                //};

                //Chinga tu madre xd
                var j = (await conexion.QueryAsync("InsertarDetalle", p, commandType: CommandType.StoredProcedure));

            }

            return Ok();
        }
    }
}
