using DesarrolloWeb.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;

namespace DesarrolloWeb
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }

        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevOrigin",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            


            // Registrar la instancia en el contenedor de inyección de dependencias
            services.AddSingleton<ITipo_ProductoServices, TipoProductoServicesWithDapper>();

            // Configurar AutoMapper y pasar la instancia

            services.AddSingleton<IEmpleadoService, EmpleadoServicesWithDapper>();
            services.AddSingleton<IProductosServices, ProductoServicioWhithDapper>();
            services.AddSingleton<IProveedoresServices, ProveedoresServicesWithDapper>();
            services.AddSingleton<IFacturasServices, FacturasServicesWhithDapper>();

            services.AddAutoMapper();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAngularDevOrigin");

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nombre de tu API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
