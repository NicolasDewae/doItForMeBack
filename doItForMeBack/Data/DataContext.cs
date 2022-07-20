using doItForMeBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace doItForMeBack.Data
{
    /// <summary>
    /// Classe qui permet de se connecter à la base de donnée et d'y récupérer les informations
    /// </summary>
    public class DataContext: DbContext
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
            #region User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Mission)
                .WithOne();
            modelBuilder.Entity<User>()
                .HasMany(u => u.Rate)
                .WithOne();
            modelBuilder.Entity<User>()
                .HasOne(u => u.Ban)
                .WithOne(u => u.UserBan)
                .HasForeignKey<Ban>(b => b.Id);
            #endregion

            #region Ban
            modelBuilder.Entity<Ban>()
                .HasOne(b => b.UserBan)
                .WithOne();
                //.HasForeignKey<User>(u => u.Id);
            #endregion

            #region Mission
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Ban)
                .WithOne()
                .HasForeignKey<Ban>(m => m.Id);
            modelBuilder.Entity<Mission>()
                .HasMany(m => m.Report)
                .WithOne();
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Claimant)
                .WithOne()
                .HasForeignKey<User>(u => u.Id);
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Maker)
                .WithOne()
                .HasForeignKey<User>(u => u.Id);
            #endregion

            #region Report
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Mission)
                .WithOne()
                .HasForeignKey<Mission>(m => m.Id);
            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithOne()
                .HasForeignKey<User>(m => m.Id);
            #endregion

            #region Rate
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.UserRate)
                .WithOne()
                .HasForeignKey<User>(b => b.Id);
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.UserRated)
                .WithOne()
                .HasForeignKey<User>(b => b.Id);
            #endregion
        }
    }
}