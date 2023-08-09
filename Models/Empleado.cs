using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloWeb.Models
{
    [Table("Empleado")]
    public class Empleado
    {
        [Key]
        public int Id_empleado { get; set; }
        public string Nombre_empleado { get; set; }
        public string apellido_empleado { get; set; }
        public string Puesto_emp { get; set; }
        public string dpi_empleado { get; set; }
        public string alias_empleado { get; set; }
        public string Correo_emp { get; set; }
        public string password_empleado { get; set; }
        public int Telefono_emp { get; set; }
    }

}
