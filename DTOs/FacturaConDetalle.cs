using DesarrolloWeb.Models;

namespace DesarrolloWeb.DTOs
{
    public class FacturaConDetalle
    {
        public Factura Factura { get; set; }
        public List<DetalleFactura> Detalles { get; set; }
    }
}
