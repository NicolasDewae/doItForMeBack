using doItForMeBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace doItForMeBack.Data
{
    /// <summary>
    /// Classe qui permet de se connecter à la base de donnée et d'y récupérer les informations
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        /// <summary>
        /// Définition des relations entre les entités
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.UserRate)
                .WithMany();
            modelBuilder.Entity<Ban>()
                .HasOne(b => b.Banner)
                .WithMany();
        }
    }
}