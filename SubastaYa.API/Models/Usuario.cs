namespace SubastaYa.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relación 1 a 1 con Billetera
        public Billetera? Billetera { get; set; }

    }
}
