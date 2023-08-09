using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models

{
    [Table("Detalle_Factura")]
    public class Detalle_Factura
    {
        [Key]
        public int Id_Detalle { get; set; }
        public int No_Factura { get; set; }
        public int Id_Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
 
}
