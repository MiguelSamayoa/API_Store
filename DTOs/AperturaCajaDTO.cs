namespace DesarrolloWeb.DTOs
{
    public class AperturaCajaDTO
    {
        public int Id_Empleado { get; set; }
        public decimal Saldo { get; set; }

        public AperturaCajaDTO( int Empleado, decimal Saldo)
        {
            Id_Empleado = Empleado;
            this.Saldo= Saldo;
        }
        public AperturaCajaDTO(){}
    }
}
