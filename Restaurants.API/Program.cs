
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Persistence;
using Restraurants.Domain.Entities;
using Serilog;
using Serilog.Events;

namespace Restaurants.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { 
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                             Type = ReferenceType.SecurityScheme, Id = "Bearer"
                            }
                        },
                    []
                    }
                });
            });
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<TimeLoggingMiddleware>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<TimeLoggingMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSerilogRequestLogging();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.MapGroup("/api/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
