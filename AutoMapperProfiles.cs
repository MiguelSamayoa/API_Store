using AutoMapper;
using Dapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.Data.SqlClient;


namespace DesarrolloWeb
{
    public class AutoMapperProfiles: Profile
    {
        private readonly ITipo_ProductoServices tipoServices;

        public AutoMapperProfiles( ITipo_ProductoServices tipoServices)
        {
            CreateMap<Cliente, PersonaGenericoOut>()
                .ForMember(cliente => cliente.ID, op => op.MapFrom(PersonaID))
                .ForMember(cliente => cliente.Nombre, op => op.MapFrom(PersonaNombre))
                .ForMember(cliente => cliente.dpi, op => op.MapFrom(PersonaDpi))
                .ForMember(cliente => cliente.Correo, op => op.MapFrom(PersonaCorreo));
            CreateMap<Producto, ProductoOutDTO>()
                .ForMember(p => p.id, op => op.MapFrom(ProductoId))
                .ForMember(p => p.nombre, op => op.MapFrom(ProductoNombre))
                .ForMember(p => p.precio, op => op.MapFrom(ProductoPrecio))
                .ForMember(p => p.disponibilidad, op => op.MapFrom(ProducDisponibilidad));

            CreateMap<Proveedor, ProveedorOutDTO>()
                .ForMember(pro => pro.id, op => op.MapFrom(ProveedorId))
                .ForMember(pro => pro.Nombre, op => op.MapFrom(ProveedorNombre))
                .ForMember(pro => pro.nit, op => op.MapFrom(ProveedorNit))
                .ForMember(pro => pro.telefono, op => op.MapFrom(ProveedorTelefono))
                .ForMember(pro => pro.direccion, op => op.MapFrom(ProveedorDireccion))
                .ForMember(pro => pro.correo, op => op.MapFrom(ProveedorCorreo));

        }

        private int PersonaID(Cliente cliente, PersonaGenericoOut persona) { return cliente.Id_Cliente; }

        private string PersonaNombre(Cliente cliente, PersonaGenericoOut persona) { return $"{cliente.Nombre_Cliente} {cliente.apellido_cliente}"; }

        private string PersonaDpi(Cliente cliente, PersonaGenericoOut persona) { return cliente.dpi_cliente; }

        private string PersonaCorreo(Cliente cliente, PersonaGenericoOut persona) { return cliente.Correo_Cliente; }

        //------------------------------------------------------------------------------

        private int ProductoId(Producto Producto, ProductoOutDTO ProductoDTO) { return Producto.id_Producto; }
 
        private string ProductoNombre(Producto Producto, ProductoOutDTO ProductoDTO) { return Producto.nombre_Producto; }

        private decimal ProductoPrecio(Producto Producto, ProductoOutDTO ProductoDTO) { return Producto.precio_Producto; }

        private bool ProducDisponibilidad(Producto Producto, ProductoOutDTO ProductoDTO)
        {
            if (Producto.disponibilidad == "Disponible") return true;
            return false;
        }

        //------------------------------------------------------------------------------
        private int ProveedorId(Proveedor Producto, ProveedorOutDTO ProductoDTO) { return Producto.Id_proveedor; }

        private string ProveedorNombre(Proveedor Producto, ProveedorOutDTO ProductoDTO) { return Producto.Nombre_Proveedor; }

        private string ProveedorNit(Proveedor Producto, ProveedorOutDTO ProductoDTO) { return Producto.nit_proveedor; }

        private Decimal ProveedorTelefono(Proveedor Producto, ProveedorOutDTO ProductoDTO) { return Producto.telefono_proveedor; }

        private string ProveedorDireccion(Proveedor Producto, ProveedorOutDTO ProductoDTO) { return Producto.direccion_proveedor; }

        private string ProveedorCorreo(Proveedor Producto, ProveedorOutDTO ProductoDTO) { return Producto.correo_proveedor; }

    }
}
