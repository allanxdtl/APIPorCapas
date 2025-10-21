using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using WEBAPI.Context;
using WEBAPI.Repositories;
using WEBAPI.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).Enrich
    .FromLogContext().WriteTo.Console().CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
//Instanciamos dependencias globales
builder.Services.AddDbContext<ResidenciasContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

//Repositorios
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IRol, RolRepository>();

//Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();

//Mapeado de Dtos con modelos
builder.Services.AddAutoMapper(typeof(Program));

//CORS
builder.Services.AddCors(c => c.AddPolicy("default", policy =>
{
    policy.
        AllowAnyMethod().
        AllowAnyOrigin().
        AllowAnyHeader();
}));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de La Bajadita",
        Version = "v1.0",
        Description = "Documentación interna del sistema.",
        Contact = new OpenApiContact
        {
            Name = "Bryan Valdez",
            Email = "alan272_@hotmail.com"
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction() || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.UseCors("default");
app.Run();