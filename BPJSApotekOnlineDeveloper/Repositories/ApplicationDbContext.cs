using BPJSApotekOnlineDeveloper.Areas.MasterData.Models;
using Microsoft.EntityFrameworkCore;

namespace BPJSApotekOnlineDeveloper.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pendaftaran> Pendaftarans { get; set; }
    }
}
