using DesarrolloWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace DesarrolloWeb.DTOs
{
    public class CreacionFacturaWithDetalleDTO
    {
        public CreacionFacturaDTO factura { get; set; }
        public List<CreacionDetalleDTO> Detalles { get; set; }

        public CreacionFacturaWithDetalleDTO(CreacionFacturaDTO factura = null, List<CreacionDetalleDTO> Detalles = null)
        {
            this.factura = factura;
            this.Detalles = Detalles;
        }
    }

    public class CreacionFacturaDTO
    {
        public int id_cliente { get; set; }
        public int Id_Empleado { get; set; }
        public Boolean Descuento { get; set; }

        public CreacionFacturaDTO() { }

        public CreacionFacturaDTO( int id_cliente, int Id_Empleado )
        {
            this.id_cliente = id_cliente;
            this.Id_Empleado = Id_Empleado;
        }

        public CreacionFacturaDTO(int id_cliente, int Id_Empleado, Boolean Descuento = false)
        {
            this.id_cliente = id_cliente;
            this.Id_Empleado = Id_Empleado;
            this.Descuento = Descuento;
        }
    }

    public class CreacionDetalleDTO
    {
        public int Id_Detalle { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
