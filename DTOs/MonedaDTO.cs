using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DesarrolloWeb.DTOs
{
    public class MonedaDTO
    {
        
            [Key]
            public int Id_Moneda { get; set; }
            public string nombre_moneda { get; set; }
                  
    }
}
