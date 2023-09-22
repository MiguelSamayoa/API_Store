using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int id_Producto { get; set; }
        public int id_Tipo_Producto { get; set; }
        public string nombre_Producto { get; set; }
        public decimal precio_Producto { get; set; }
        public int Stock { get; set; }
        public int id_proveedor { get; set; }
        public string disponibilidad { get; set; }
        public int Id_Empleado { get; set; }

        public string categoria { get; set; }
        public string proveedor { get; set; }

    }
}