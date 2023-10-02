namespace DesarrolloWeb.DTOs
{
    public class PagoDTO
    {
        public int Id_Tipo_Pago { get; set; }
        public int id_moneda { get; set; }
        public decimal Cantidad_pagar { get; set; }
        public int no_factura { get; set; }
    }
}
