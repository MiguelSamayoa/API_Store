using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("Cierre_Caja")]
    public class CierreCaja
    {
        [Key]
        public int Id_cierre_Caja { get; set; }
        public int Id_Empleado { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public decimal Saldo { get; set; }
        public int Id_apertura { get; set; }
    }
}
