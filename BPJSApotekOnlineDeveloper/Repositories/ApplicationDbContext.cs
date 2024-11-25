using BPJSApotekOnlineDeveloper.Areas.MasterData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurchasingSystemDeveloper.Models;

namespace BPJSApotekOnlineDeveloper.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Pendaftaran> Pendaftarans { get; set; }
        public DbSet<UserActive> UserActives { get; set; }
        public DbSet<PendataanResepMasuk> PendataanResepMasuks { get; set; }
        public DbSet<ResepMasuk> ResepMasuks { get; set; }
        public DbSet<PendataanPRBMTM> PendataanPRBMTMs { get; set; }
    }
}
