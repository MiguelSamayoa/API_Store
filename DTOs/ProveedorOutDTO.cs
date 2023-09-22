using System.ComponentModel.DataAnnotations;

namespace DesarrolloWeb.DTOs
{
    public class ProveedorOutDTO
    {
        [Key]
        public int id { get; set; }
        public string Nombre { get; set; }
        public string nit { get; set; }
        public Decimal telefono { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
    }
}
