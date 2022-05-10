using bad115_api_core.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bad115_api_core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContextSystem>(o => o.UseSqlServer(Configuration.GetConnectionString("Conexion")));
            //services.AddControllers();
            services.AddControllers(options => { options.AllowEmptyInputInBodyModelBinding = true; });
            //Agregar el repositorio
            services.AddScoped<RepositoryAdmin_catalogoValue>();
            services.AddScoped<RepositoryDetalle_orden_compraValue>();
            services.AddScoped<RepositoryDetallerequisicionValue>();
            services.AddScoped<RepositoryDivisasValue>();
            services.AddScoped<RepositoryEmpleadoValue>();
            services.AddScoped<RepositoryEmpresaValue>();
            services.AddScoped<RepositoryOrden_compraValue>();
            services.AddScoped<RepositoryPaisValue>();
            services.AddScoped<RepositoryProductoValue>();
            services.AddScoped<RepositoryProveedorValue>();
            services.AddScoped<RepositoryRequisicionValue>();
            services.AddScoped<RepositorySucursalValue>();
            services.AddScoped<RepositoryUsuarioValue>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());
            app.UseHttpsRedirection();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
