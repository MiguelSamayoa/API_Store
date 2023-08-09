using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("pago")]
    public class pago
    {
        [Key]
        public int id_pago { get; set; }
        public int Id_Tipo_Pago { get; set; }
        public int id_moneda { get; set; }
        public decimal Cantidad_pagar { get; set; }
        public int no_factura { get; set; }
    }

}
