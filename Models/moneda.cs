using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DesarrolloWeb.Models
{
    [Table("moneda")]
    public class moneda
    {
        [Key]
        public int Id_Moneda { get; set; }
        public string nombre_moneda { get; set; }
        public string pais_moneda { get; set; }
        public decimal tipo_cambio { get; set; }

    }


}
