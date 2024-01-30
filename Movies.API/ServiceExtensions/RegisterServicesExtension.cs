using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Movies.Core.Entities;
using Movies.Core.Repositories.Interfaces;
using Movies.Repositories.Implementation;

namespace Movies.API.ServiceExtensions;

public static class RegisterServicesExtension
{
  public static void ConfigureBadRequest(this IServiceCollection services)
  {
    services.Configure<ApiBehaviorOptions>(a =>
    {
      a.InvalidModelStateResponseFactory = context =>
      {
        return new BadRequestObjectResult(new CustomBadRequest(context))
        {
          ContentTypes = { "application /json", "application/xml" },
        };
      };
    });
  }

  public static void ConfigureSwaggerGen(this IServiceCollection services)
  {
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "PAPSS Inbound API", Version = "v1" });
      c.EnableAnnotations();
      c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
      {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic authentication"
      });
      c.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "basic"
            }
          },
          new string[] { }
        }
      });
    });
  }

  
  public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
    where TContext : AppDbContext
  {
    services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
    services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
    return services;
  }
}