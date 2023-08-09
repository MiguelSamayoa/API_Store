using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("Tipo_Producto")]
    public class TipoProducto
    {
        [Key]
        public int Id_Tipo_Producto { get; set; }
        public string Tipo_Producto { get; set; }

    }

}
