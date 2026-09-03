using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Models;

namespace SubastaYa.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Billetera> Billeteras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación 1 a 1 entre Usuario y Billetera
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Billetera)
            .WithOne(b => b.Usuario)
            .HasForeignKey<Billetera>(b => b.UsuarioId);
    }
}