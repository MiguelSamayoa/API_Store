using Microsoft.AspNetCore.Mvc;
using Dapper;
using DesarrolloWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace DesarrolloWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturaController:ControllerBase
    {
        private readonly string connectionString;

        public DetalleFacturaController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        
    }
}
