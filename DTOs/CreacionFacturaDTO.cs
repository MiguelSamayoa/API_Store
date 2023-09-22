using System.ComponentModel.DataAnnotations;

namespace DesarrolloWeb.DTOs
{
    public class CreacionFacturaWithDetalleDTO
    {
        public CreacionFacturaDTO factura { get; set; }
        public List<CreacionDetalleDTO> Detalles { get; set; }
    }

    public class CreacionFacturaDTO
    {
        public int id_cliente { get; set; }
        public int Id_Empleado { get; set; }
    }

    public class CreacionDetalleDTO
    {
        public int No_Factura { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
