using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("Apertura_Caja")]
    public class AperturaCaja
    {
        [Key]
        public int Id_apertura { get; set; }
        public int Id_Empleado { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public decimal Saldo { get; set; }
    }
}
