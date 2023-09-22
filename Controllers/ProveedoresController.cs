using AutoMapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Models;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController:Controller
    {

        private IProveedoresServices provedorServices;
        private readonly IMapper mapper;

        public ProveedoresController(IProveedoresServices provedorServices, IMapper mapper)
        {
            this.provedorServices = provedorServices;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProveedorOutDTO>>> GetAll()
        {
            var prov = await provedorServices.GetProveedoresAsync();

            var proveedores = mapper.Map<List<ProveedorOutDTO>>(prov);
            return Ok(proveedores);
        }
    }
}
