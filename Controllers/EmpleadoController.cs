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

        private readonly IEmpleadoService servicioEmpleado;

        public EmpleadoController(IConfiguration configuration, IEmpleadoService ServicioEmpleado)
        {
            servicioEmpleado = ServicioEmpleado;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> GetEmpleado( EmpleadoLogin empleado ) {

            var existe = await servicioEmpleado.AutenticarEmpleado(empleado);
            if (existe != null)
            {
                return Ok(existe);
            }
            else return NotFound("Usuario no encontrado");
        }

        private async Task SetSession(EmpleadoLogin usuario)
        {
            List<Claim> c = new()
                                {
                                        new Claim(ClaimTypes.NameIdentifier, usuario.Correo)

                                };

            ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties p = new()
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
        }
    }
}
