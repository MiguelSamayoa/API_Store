using DesarrolloWeb.Models;

namespace DesarrolloWeb.DTOs
{
    public class FacturaConDetalle
    {
        public Factura Factura { get; set; }
        public List<DetalleFactura> Detalles { get; set; }

        public FacturaConDetalle(){}
        public FacturaConDetalle(Factura factura, List<DetalleFactura> Detalles)
        {
            Factura = factura;
            this.Detalles = Detalles;
        }
    }    
    public class FacturaConDetalleWithData
    {
        public Factura Factura { get; set; }
        public List<DetalleWithDataDTO> Detalles { get; set; }

        public FacturaConDetalleWithData(){}
        public FacturaConDetalleWithData(Factura factura, List<DetalleWithDataDTO> Detalles)
        {
            Factura = factura;
            this.Detalles = Detalles;
        }
    }
}
