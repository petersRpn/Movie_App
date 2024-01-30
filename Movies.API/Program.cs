
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Movies.API.HealthChecks;
using Movies.API.ServiceExtensions;
using Movies.Core.Entities;
using Movies.Core.Services;
using Movies.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = builder.Configuration;
bool _useHealthCheckUI = config.GetValue<bool>("UseHealthCheckUI");
bool _useMock = config.GetValue<bool>("UseMock");




// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

ConfigureDatabase(builder);
//builder.Services.AddDbContext<AppDbContext>(options => 
//  options.UseSqlServer(builder.Configuration.GetConnectionString("papssInbound")))
//  .AddUnitOfWork<AppDbContext>();

if (_useMock == false)
{
   
    builder.Services.AddTransient<IMovieApi, MovieApi>();
}
else
{
    //builder.Services.AddTransient<IMovieApi, MovieApiMoq>();
}




builder.Services.ConfigureBadRequest();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddAuthentication("BasicAuthentication").
//AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddHealthChecks()
  .AddCheck<MovieAppHealthCheck>("MovieAppHealthCheck", tags: new[] { "Movie Service" });

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SchemaFilter<Movies.SwaggerSchemaExampleFilter>();
});

if (_useHealthCheckUI)
{
    builder.Services.AddHealthChecksUI();
}

var app = builder.Build();

//create db if does not exist
CreateDb(builder);

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
}).AllowAnonymous();

//Health UI
if (_useHealthCheckUI)
{
    app.MapHealthChecksUI().AllowAnonymous();
    app.UseHealthChecksUI(options => { options.UIPath = "/health-dashboard"; });
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();


void ConfigureDatabase(WebApplicationBuilder builder)
{
    //real db
    var conn = builder.Configuration["ConnectionStrings:MovieApp"];

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(conn,
            u => u.EnableRetryOnFailure());
    }, ServiceLifetime.Transient);
}

void CreateDb(WebApplicationBuilder builder)
{
    using (var service = builder.Services.BuildServiceProvider())
    {
        using (var serviceScope = service.CreateScope())
        {
            var scopeServiceProvider = serviceScope.ServiceProvider;

            var db = scopeServiceProvider.GetService<AppDbContext>();
            try
            {
                db.Database.EnsureCreated();
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
