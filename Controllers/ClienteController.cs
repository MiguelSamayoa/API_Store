using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;
using AutoMapper;
using DesarrolloWeb.DTOs;
using DesarrolloWeb.Services;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController:ControllerBase
    {
        private readonly IClienteServices clientesServices;

        public ClienteController(IClienteServices ClientesServices)
        {
            clientesServices = ClientesServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaGenericoOut>>> GetCliente()
        {
            List<PersonaGenericoOut> personas = await clientesServices.GetCliente();

            if (personas.Count == 0) return NotFound("No hay clientes");

            return Ok(personas);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PersonaGenericoOut>> GetClienteID(int Id)
        {
            PersonaGenericoOut personas = await clientesServices.GetClienteID(Id);

            if (personas == null) return NotFound("No existe el cliente");

            return personas;
        }

        [HttpPost]
        public async Task<ActionResult<PersonaGenericoOut>> PostCliente(PersonaGenericoOut cliente)
        {
            try
            {
                PersonaGenericoOut PersonaIngresada = await clientesServices.PostCliente(cliente);

                if (PersonaIngresada == null) return NotFound();

                return Ok(PersonaIngresada);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }


    }
}
