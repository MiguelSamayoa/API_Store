using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int Id_Cliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public string apellido_cliente { get; set; }
        public string dpi_cliente { get; set; }
        public decimal telefono_cliente { get; set; }
        public string Nit { get; set; }
        public string Correo_Cliente { get; set; }
        public string Direccion { get; set; }
    }


}
