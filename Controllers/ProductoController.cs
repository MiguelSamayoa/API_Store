using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;
using AutoMapper;
using DesarrolloWeb.DTOs;
using Microsoft.Identity.Client.Extensions.Msal;
using DesarrolloWeb.Services;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductosServices productosServices;
        private readonly ITipo_ProductoServices tipoProducto;
        private readonly IProveedoresServices proveedoresServices;
        private readonly IMapper mapper;

        public ProductoController(IProductosServices productosServices, 
                                    ITipo_ProductoServices TipoProducto, 
                                    IProveedoresServices proveedoresServices,
                                    IMapper mapper)
        {
            this.productosServices = productosServices;
            tipoProducto = TipoProducto;
            this.proveedoresServices = proveedoresServices;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoOutDTO>>> GetAll()
        {
            var productos = await productosServices.getProductosWithData();

            if (productos != null)
            {
                var Productos = mapper.Map<List<ProductoOutDTO>>(productos);
                return Ok(Productos);
            }
            return NotFound("No hay productos disponibles");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> GetByID( int id)
        {
            var p = await productosServices.GetProducto(id);

            if (p != null) return Ok(p);
            return NotFound("Producto no econtrado");
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto( Producto producto ){
            var p = await productosServices.setProducto(producto);

            if (p != null) return Ok(p);
            return BadRequest("No fue posible ingresar el producto");
        }
    }
}
