using doItForMeBack.Entities;
using Microsoft.EntityFrameworkCore;

namespace doItForMeBack.Data
{
    /// <summary>
    /// Classe qui permet de se connecter à la base de donnée et d'y récupérer les informations
    /// </summary>
    public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) 
            : base(options) { }
    }
}
