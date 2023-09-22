using DesarrolloWeb.Models;

namespace DesarrolloWeb.DTOs
{
    public class FacturaConDetalle
    {
        public Factura Factura { get; set; }
        public List<DetalleFactura> Detalles { get; set; }

        public FacturaConDetalle(){}

        public FacturaConDetalle( Factura factura, List<DetalleFactura> Detalles) {
            this.Factura = factura;
            this.Detalles = Detalles;
        }

    }
}
