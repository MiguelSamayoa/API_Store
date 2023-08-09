using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{ 
    [Table("Devolucion")]
    public class Devolucion
    {
        [Key]
        public int Id_devolucion { get; set; }
        public int Id_Detalle { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int Id_Empleado { get; set; }
    }

}
