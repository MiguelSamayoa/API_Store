namespace DesarrolloWeb.DTOs
{
    public class DetalleWithDataDTO
    {
        public int Id_Detalle { get; set; }

        public string Producto { get; set; }
        public int Id_Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
