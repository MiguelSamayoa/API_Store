﻿using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private ITipo_ProductoServices TipoServices;
        public TipoProductoController(ITipo_ProductoServices tipo_ProductoServices)
        {
            TipoServices = tipo_ProductoServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoProducto>>> GetAll()
        {
            var x = await TipoServices.GetAll();
            if (x != null) return Ok(x);
            return NotFound("Categoria no encontrada");
        }
    }
}
