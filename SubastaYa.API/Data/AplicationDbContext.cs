using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Models;

namespace SubastaYa.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Billetera> Billeteras { get; set; }
    public DbSet<Subasta> Subastas { get; set; }
    public DbSet<Puja> Pujas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<TransaccionLedger> TransaccionesLedger { get; set; }
    public DbSet<Auditoria_Log> Auditorias { get; set; }

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
            .HasOne(s => s.Usuario)
            .WithMany()
            .HasForeignKey(s => s.VendedorId)
            .OnDelete(DeleteBehavior.Restrict);

        // 3. Categoria <-> Subasta (1 a N)
        modelBuilder.Entity<Subasta>()
            .HasOne(s => s.Categoria)
            .WithMany()
            .HasForeignKey(s => s.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // 4. Subasta <-> Puja (1 a N)
        modelBuilder.Entity<Puja>()
            .HasOne(p => p.Subasta)
            .WithMany()
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
            .HasForeignKey(t => t.BilleteraFk)
            .OnDelete(DeleteBehavior.Cascade);

        // 7. Subasta <-> TransaccionLedger (1 a N opcional)
        modelBuilder.Entity<TransaccionLedger>()
            .HasOne(t => t.Subasta)
            .WithMany()
            .HasForeignKey(t => t.SubastaFk)
            .OnDelete(DeleteBehavior.SetNull);

        // 8. Usuario <-> Auditoria_Log (1 a N opcional)
        modelBuilder.Entity<Auditoria_Log>()
            .HasOne<Usuario>()
            .WithMany(u => u.Auditorias)
            .HasForeignKey(a => a.UsuarioFK)
            .OnDelete(DeleteBehavior.SetNull);
    }
}