using JSoft.Infraestructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSoft.API
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
            //Agregar Cors
            services.AddCors();

            //Llamado a los repositorios
            services.AddScoped<ClientesRepository>();
            services.AddScoped<UsuariosRepository>();
            services.AddScoped<ServidoresRepository>();
            services.AddScoped<ServiciosRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JSoft.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JSoft.API v1"));
            }

            app.UseCors(options =>
            {
                options.WithOrigins("http://192.168.3.230").AllowAnyMethod();
                options.WithOrigins("http://192.168.3.205").AllowAnyMethod();
                options.WithOrigins("http://192.168.3.205:81").AllowAnyMethod();
                options.WithOrigins("http://192.168.10.55:3001").AllowAnyMethod();
                options.WithOrigins("http://192.168.3.230:3000").AllowAnyMethod();
                options.WithOrigins("http://192.168.3.230:3001").AllowAnyMethod();
                options.WithOrigins("http://192.168.3.230:3002").AllowAnyMethod();
                options.WithOrigins("http://190.146.168.21:3000").AllowAnyMethod();
                options.WithOrigins("http://190.146.168.21:3001").AllowAnyMethod();
                options.WithOrigins("http://190.146.168.21:3002").AllowAnyMethod();
                options.WithOrigins("http://localhost:3000").AllowAnyMethod();
                options.WithOrigins("http://localhost:3001").AllowAnyMethod();
                options.WithOrigins("http://localhost:3002").AllowAnyMethod();
                options.WithOrigins("https://localhost:44305").AllowAnyMethod();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            if (env.IsDevelopment())
            { app.UseDeveloperExceptionPage(); }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            { endpoints.MapControllers(); });

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
