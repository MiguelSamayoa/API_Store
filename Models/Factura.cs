using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("Factura")]
    public class Factura
    {
        [Key]
        public int No_Factura { get; set; }
        public int id_cliente { get; set; }
        public string serie { get; set; }
        public int Id_Empleado { get; set; }
        public DateTime Fecha { get; set; }
    }

}
