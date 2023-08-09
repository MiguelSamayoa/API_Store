using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("proveedor")]
    public class Proveedor
    {
        [Key]
        public int Id_proveedor { get; set; }
        public string Nombre_Proveedor { get; set; }
        public string nit_proveedor { get; set; }
        public Decimal telefono_proveedor { get; set; }
        public string direccion_proveedor { get; set; }
        public string correo_proveedor { get; set; }
    }

 
}
