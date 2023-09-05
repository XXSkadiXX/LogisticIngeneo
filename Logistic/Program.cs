using Infraestructure.Core.Data;
using Logistic.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.WriteTo.Console();
    configuration.MinimumLevel.Warning();
    configuration.WriteTo.File("Logs/LogLogistic.txt", rollingInterval: RollingInterval.Day);

});

#region SQL Lite Connection
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringSQLServer"));
});
#endregion

#region Inyection
DependencyInyectionHandler.DependencyInyectionConfig(builder.Services);
#endregion

#region Jwt Token Configuration
IConfigurationSection tokenAppSetting = builder.Configuration.GetSection("Tokens");
JwtConfigurationHandler.ConfigureJwtAuthentication(builder.Services, tokenAppSetting);

#endregion

#region CustimValidationFilterAttribute
builder.Services.Configure<ApiBehaviorOptions>(options
=> options.SuppressModelStateInvalidFilter = true);
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Logistic App",
        Description = "Prueba técnica para Ingeneo",
        Contact = new OpenApiContact
        {
            Name = "Maria Clara Jimenez",
            Email = "mariaclarajh@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/maria-clara-jimenez-hernandez-51a139277/"),
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // To Enable authorization using Swagger (JWT)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}

        }
    });
});

var app = builder.Build();

#region RunSeeding
var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = scopedFactory.CreateScope())
{
    var service = scope.ServiceProvider.GetService<SeedDb>();
    service!.ExecSeedAsync().Wait();
}
#endregion

JwtConfigurationHandler.ConfigureUseAuthentication(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
