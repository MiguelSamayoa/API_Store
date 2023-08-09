using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("tipo_pago")]
    public class tipo_pago
    {
        [Key]
        public int Id_Tipo_Pago { get; set; }
        public string Descripcion { get; set; }
    }

}
