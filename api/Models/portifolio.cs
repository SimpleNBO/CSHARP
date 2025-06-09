using Microsoft.EntityFrameworkCore;

namespace apiCSHARP.apiAluguel.Models
{
    public class portifolio : DbContext
    {
        public portifolio(DbContextOptions<portifolio> options) : base(options) { }

        public DbSet<Imovel> Imoveis { get; set; }
    }
}