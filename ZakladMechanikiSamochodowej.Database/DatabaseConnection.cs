using Microsoft.EntityFrameworkCore;

namespace ZakladMechanikiSamochodowej.Database
{
    public partial class DatabaseConnection : DbContext
    {
        public DatabaseConnection()
        {
        }

        public virtual DbSet<DatabaseModels.Users> Users { get; set; }

        public virtual DbSet<DatabaseModels.Orders> Orders { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "\\Database.mdf;Integrated Security = True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}