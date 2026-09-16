using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using SubastaYa.API.Data;
using SubastaYa.API.Services;
using SubastaYa.API.Workers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Agregar el servicio de fondo para cerrar subastas 
builder.Services.AddHostedService<SubastaFinalizacionWorker>();
// Agregue servicios al contenedor.
builder.Services.AddScoped<PujaAutomaticaService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar autenticación JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings"); 
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Obtener cadena de conexión y registrar DbContext con PostgreSQL e ignorar advertencias dinámicas
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseNpgsql(connectionString).ConfigureWarnings(warnings =>warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
var app = builder.Build();

//configuracion de pipelane de HTTP
if (app.Environment.IsDevelopment()) 
{ app.UseSwagger(); 
    app.UseSwaggerUI();
}


// Configure la canalización de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();