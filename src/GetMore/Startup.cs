using System.Reflection;
using GetMore.Api.Converters;
using GetMore.Application.Interfaces;
using GetMore.Application.Queries;
using GetMore.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;

namespace GetMore.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = _configuration.GetConnectionString("GetMoreCN");

            services.AddScoped<ISqlConnectionFactory>(c => new SqlConnectionFactory(sqlConnectionString));

            var domainAssembly = typeof(GetUnitsOfWorkForWeekQuery).GetTypeInfo().Assembly;

            services.AddMediatR(domainAssembly);

            services.AddFluentValidation(new[] { domainAssembly });

            services
                .AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}