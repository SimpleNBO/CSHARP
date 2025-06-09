using Microsoft.EntityFrameworkCore;

namespace apiCSHARP.apiAluguel.Models
{
    public class Locatarios : DbContext
    {
        public Locatarios(DbContextOptions<Locatarios> options) : base(options) { }

        public DbSet<Pessoa> Locacoes { get; set; }
    }
}