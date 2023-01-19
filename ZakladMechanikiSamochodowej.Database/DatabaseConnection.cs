using Microsoft.EntityFrameworkCore;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Database
{
    public partial class DatabaseConnection : DbContext
    {
        public DatabaseConnection()
        {
        }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "\\Database.mdf;Integrated Security = True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__61CDFAE6B524812A");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Username");
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Password");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PhoneNumber");
                entity.Property(e => e.EmialAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EmialAddress");
                entity.Property(e => e.IsAdmin).HasColumnName("IsAdmin");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}