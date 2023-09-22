using System.ComponentModel.DataAnnotations;

namespace DesarrolloWeb.DTOs
{
    public class ProductoOutDTO
    {
        [Key]
        public int id { get; set; }
        public string categoria { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int Stock { get; set; }
        public string proveedor { get; set; }
        public bool disponibilidad { get; set; }
    }
}
