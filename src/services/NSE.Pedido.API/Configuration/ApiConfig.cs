﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Pedidos.Infra.Data;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Pedido.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration( this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<PedidosContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            return services;
        }

        public static IApplicationBuilder UseApiConfiguration( this IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthConfiguration();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}

