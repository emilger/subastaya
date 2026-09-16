using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Models;
using BCrypt.Net;

namespace SubastaYa.API.Data;

public class AplicationDbContext : DbContext
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
    public DbSet<Billetera> Billeteras { get; set; } = null!;
    public DbSet<Subasta> Subastas { get; set; } = null!;
    public DbSet<Puja> Pujas { get; set; } = null!;
    public DbSet<Categoria> Categorias { get; set; } = null!;
    public DbSet<TransaccionLedger> TransaccionesLedger { get; set; } = null!;
    public DbSet<Auditoria_Log> Auditorias { get; set; } = null!;

    public DbSet<PujaAutomatica> PujasAutomaticas { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Usuario <-> Billetera (1 a 1)
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Billetera)
            .WithOne(b => b.Usuario)
            .HasForeignKey<Billetera>(b => b.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Columna física calculada en PostgreSQL para SaldoDisponible
        modelBuilder.Entity<Billetera>()
            .Property(b => b.SaldoDisponible)
            .HasComputedColumnSql("\"SaldoTotal\" - \"SaldoRetenido\"", stored: true);

        // 2. Usuario <-> Subasta (Vendedor) (1 a N)
        modelBuilder.Entity<Subasta>()
            .HasOne(s => s.Vendedor)
            .WithMany()
            .HasForeignKey(s => s.VendedorId)
            .OnDelete(DeleteBehavior.Restrict);

        // 3. Categoria <-> Subasta (1 a N)
        modelBuilder.Entity<Subasta>()
            .HasOne(s => s.categoria)
            .WithMany()
            .HasForeignKey(s => s.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        // 4. Subasta <-> Puja (1 a N)
        modelBuilder.Entity<Puja>()
            .HasOne(p => p.Subasta)
            .WithMany(s => s.Pujas)
            .HasForeignKey(p => p.SubastaId)
            .OnDelete(DeleteBehavior.Cascade);

        // 5. Usuario <-> Puja (Comprador) (1 a N)
        modelBuilder.Entity<Puja>()
            .HasOne(p => p.Comprador)
            .WithMany()
            .HasForeignKey(p => p.CompradorId)
            .OnDelete(DeleteBehavior.Restrict);

        // 6. Billetera <-> TransaccionLedger (1 a N)
        modelBuilder.Entity<TransaccionLedger>()
            .HasOne(t => t.Billetera)
            .WithMany()
            .HasForeignKey(t => t.BilleteraId)
            .OnDelete(DeleteBehavior.Cascade);

        // 7. Subasta <-> TransaccionLedger (1 a N opcional)
        modelBuilder.Entity<TransaccionLedger>()
            .HasOne(t => t.Subasta)
            .WithMany()
            .HasForeignKey(t => t.SubastaId)
            .OnDelete(DeleteBehavior.SetNull);

        // 8. Usuario <-> Auditoria_Log (1 a N opcional)
        modelBuilder.Entity<Auditoria_Log>()
            .ToTable("Auditoria_Log") // Mapeo explícito para evitar error 42P01 en PostgreSQL
            .HasOne<Usuario>()
            .WithMany(u => u.Auditorias)
            .HasForeignKey(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);

        // Subasta <-> PujaAutomatica (1 a N)
        modelBuilder.Entity<PujaAutomatica>()
            .HasOne(pa => pa.Subasta)
            .WithMany()
            .HasForeignKey(pa => pa.SubastaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Usuario <-> PujaAutomatica (1 a N)
        modelBuilder.Entity<PujaAutomatica>()
            .HasOne(pa => pa.Comprador)
            .WithMany()
            .HasForeignKey(pa => pa.CompradorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Carga de datos semillas sin dependencias
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { 
                CategoriaId = 1,
                Nombre = "Tecnologia", 
                UrlIcono = "bi-cpu" },
            new Categoria { 
                CategoriaId = 2, 
                Nombre = "Coleccionables",
                UrlIcono = "bi-star" },
            new Categoria {
                CategoriaId = 3, 
                Nombre = "Indumentaria",
                UrlIcono = "bi-tshirt" },
            new Categoria { 
                CategoriaId = 4,
                Nombre = "Vehiculos",
                UrlIcono = "bi-car" }
        );

        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                UsuarioId = 1,
                NombreUsuario = "Franco",
                Alias = "elque vende",
                Email = "vendedor@test.com",
                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Password123"),
                FechaRegistro = DateTime.UtcNow
            },
            new Usuario
            {
                UsuarioId = 2,
                NombreUsuario = "Juan",
                Alias = "el que compra1",
                Email = "comprador1@test.com",
                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Contrasena8520"),
                FechaRegistro = DateTime.UtcNow
            },
            new Usuario
            {
                UsuarioId = 3,
                NombreUsuario = "Pedro",
                Alias = "el que compra2",
                Email = "comprador2@test.com",
                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("825825474"),
                FechaRegistro = DateTime.UtcNow
            },
            new Usuario
            {
                UsuarioId = 4,
                NombreUsuario = "Maria",
                Alias = "la que compra3",
                Email = "sinfondos@test.com",
                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("9999999"),
                FechaRegistro = DateTime.UtcNow
            }
        );

        // Carga de datos semillas con dependencias directas
        modelBuilder.Entity<Billetera>().HasData(
            new Billetera { 
                BilleteraId = 1, 
                UsuarioId = 1, 
                SaldoTotal = 0, 
                SaldoRetenido = 0, 
                Version = 1 },
            new Billetera { 
                BilleteraId = 2, 
                UsuarioId = 2, 
                SaldoTotal = 150000, 
                SaldoRetenido = 45000, 
                Version = 1 },
            new Billetera { 
                BilleteraId = 3,
                UsuarioId = 3, 
                SaldoTotal = 200000, 
                SaldoRetenido = 0, 
                Version = 1 },
            new Billetera { 
                BilleteraId = 4, 
                UsuarioId = 4, 
                SaldoTotal = 500,
                SaldoRetenido = 0, 
                Version = 1 }
        );

        modelBuilder.Entity<Subasta>().HasData(
           new Subasta
           {
               SubastaId = 1,
               VendedorId = 1,
               CategoriaId = 1,
               Titulo = "Laptop",
               Descripcion = "Laptop de gama alta",
               Estado = "ACTIVA",
               UrlImagen = "https://picsum.photos/id/0/600/400",
               PrecioInicial = 5000,
               PujaMinima = 1000,
               Version = 1,
               FechaInicio = DateTime.UtcNow,
               FechaFin = DateTime.UtcNow.AddMinutes(30)
           },
           new Subasta
           {
               SubastaId = 2,
               VendedorId = 1,
               CategoriaId = 2,
               Titulo = "Maquina de escribir vintage",
               Descripcion = "Maquina de escribir vintage en buen estado",
               Estado = "ACTIVA",
               UrlImagen = "https://picsum.photos/id/111/600/400",
               PrecioInicial = 50,
               PujaMinima = 10,
               Version = 1,
               FechaInicio = DateTime.UtcNow,
               FechaFin = DateTime.UtcNow.AddMinutes(1.30)
           },
           new Subasta
           {
               SubastaId = 3,
               VendedorId = 1,
               CategoriaId = 3,
               Titulo = "Campera de invierno",
               Descripcion = "Campera de invierno en buen estado",
               Estado = "PROGRAMADA",
               UrlImagen = "https://picsum.photos/id/338/600/400",
               PrecioInicial = 100,
               PujaMinima = 300,
               Version = 1,
               FechaInicio = DateTime.UtcNow.AddDays(1),
               FechaFin = DateTime.UtcNow.AddDays(3)
           },
           new Subasta
           {
               SubastaId = 4,
               VendedorId = 1,
               CategoriaId = 4,
               Titulo = "Autos antiguos",
               Descripcion = "Autos antiguos en buen estado",
               Estado = "FINALIZADA",
               UrlImagen = "https://picsum.photos/id/133/600/400",
               PrecioInicial = 300,
               PujaMinima = 1000,
               Version = 1,
               FechaInicio = DateTime.UtcNow.AddDays(-5),
               FechaFin = DateTime.UtcNow.AddDays(-3)
           },
           new Subasta
           {
               SubastaId = 5,
               VendedorId = 1,
               CategoriaId = 1,
               Titulo = "Smartphone",
               Descripcion = "Smartphone de última generación",
               Estado = "FINALIZADA",
               UrlImagen = "https://picsum.photos/id/3/600/400",
               PrecioInicial = 800,
               PujaMinima = 200,
               Version = 1,
               FechaInicio = DateTime.UtcNow.AddDays(-2),
               FechaFin = DateTime.UtcNow.AddDays(-1)
           }
        );

        modelBuilder.Entity<Auditoria_Log>().HasData(
            new Auditoria_Log { 
                AuditoriaId = 1,
                UsuarioId = 1,
                Entidad = "SUBASTA",
                EntidadId = 1, 
                Accion = "CREAR_SUBASTA",
                Detalle_Json = "{}", 
                Fecha = DateTime.UtcNow },
            new Auditoria_Log { 
                AuditoriaId = 2,
                UsuarioId = 1
                , Entidad = "SUBASTA",
                EntidadId = 2,
                Accion = "CREAR_SUBASTA", 
                Detalle_Json = "{}", 
                Fecha = DateTime.UtcNow },
            new Auditoria_Log { 
                AuditoriaId = 3,
                UsuarioId = 1,
                Entidad = "SUBASTA",
                EntidadId = 3, 
                Accion = "CREAR_SUBASTA", 
                Detalle_Json = "{}",
                Fecha = DateTime.UtcNow }
        );

        // Carga de datos semillas con dependencias indirectas
        modelBuilder.Entity<Puja>().HasData(
            new Puja { 
                PujaId = 1, 
                SubastaId = 1, 
                CompradorId = 3, 
                FechaPuja = DateTime.UtcNow.AddMinutes(-10), 
                MontoPuja = 30000 },
            new Puja { 
                PujaId = 2, 
                SubastaId = 1, 
                CompradorId = 2, 
                FechaPuja = DateTime.UtcNow.AddMinutes(-5), 
                MontoPuja = 45000 }
        );

        modelBuilder.Entity<TransaccionLedger>().HasData(
            new TransaccionLedger { 
                TransaccionId = 1, 
                BilleteraId = 2,
                SubastaId = null,
                TipoTransaccion = "DEPOSITO", 
                Monto = 150000, 
                Fecha = DateTime.UtcNow.AddDays(-1) },
            new TransaccionLedger {
                TransaccionId = 2, 
                BilleteraId = 3,
                SubastaId = null, 
                TipoTransaccion = "DEPOSITO",
                Monto = 200000,
                Fecha = DateTime.UtcNow.AddDays(-1) },
            new TransaccionLedger { 
                TransaccionId = 3, 
                BilleteraId = 4, 
                SubastaId = null, 
                TipoTransaccion = "DEPOSITO",
                Monto = 500,
                Fecha = DateTime.UtcNow.AddDays(-1) },
            new TransaccionLedger { 
                TransaccionId = 4, 
                BilleteraId = 2,
                SubastaId = 1,
                TipoTransaccion = "RETENCION", 
                Monto = -45000,
                Fecha = DateTime.UtcNow.AddMinutes(-5) }
        );
    }
}