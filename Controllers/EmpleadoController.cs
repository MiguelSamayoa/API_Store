using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;
using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using DesarrolloWeb.DTOs;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : Controller
    {
        private readonly IAperturaCajaServices aperturaCaja;
        private readonly IEmpleadoService servicioEmpleado;

        public EmpleadoController(IConfiguration configuration, IAperturaCajaServices AperturaCaja, IEmpleadoService ServicioEmpleado)
        {
            aperturaCaja = AperturaCaja;
            servicioEmpleado = ServicioEmpleado;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> GetEmpleado( EmpleadoLogin empleado ) {

            var existe = await servicioEmpleado.AutenticarEmpleado(empleado);
            if (existe != null)
            {
                AperturaCaja apertura = await aperturaCaja.PostApertura(new AperturaCajaDTO(existe.Id_empleado, 250));
                if(apertura != null)return Ok(new
                {
                    AperturaCaja = apertura,
                    Empleado = existe
                });
                else return NotFound("Usuario no oudo iniciar Sesion");

            }
            else return NotFound("Usuario no encontrado");
        }

    }
}
