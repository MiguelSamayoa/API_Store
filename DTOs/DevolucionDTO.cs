namespace DesarrolloWeb.DTOs
{
    public class DevolucionDTO
    {
        public int Id_Detalle { get; set; }
        public int Cantidad { get; set; }
        public int Id_Empleado { get; set; }

        public DevolucionDTO(int Id_Detalle, int Cantidad, int Id_Empleado)
        {
            this.Id_Detalle = Id_Detalle;
            this.Cantidad = Cantidad;
            this.Id_Empleado = Id_Empleado;
        }

        public DevolucionDTO() { }

    }
}
