using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Agregue servicios al contenedor.

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

// Obtener cadena de conexión y registrar DbContext con PostgreSQL e ignorar advertencias dinámicas
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString).ConfigureWarnings(warnings =>warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
var app = builder.Build();

// Configure la canalización de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
