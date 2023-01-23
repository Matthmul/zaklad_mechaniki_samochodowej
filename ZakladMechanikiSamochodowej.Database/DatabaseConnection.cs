using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Database
{
    public partial class DatabaseConnection : DbContext
    {
        public DatabaseConnection()
        {
        }

        public virtual DbSet<User> LoginTable { get; set; }

        public virtual DbSet<Order> OrderTable { get; set; }

        public virtual DbSet<Cars> CarsTable { get; set; }

        // TODO Pomyslec czy da sie jakos ustawic inaczej sciezke do bazy
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(JsonConvert.DeserializeObject<DatabaseConfig>(File.ReadAllText(".//databaseConfig.json"))?.SqlString,
                providerOptions => { providerOptions.EnableRetryOnFailure(); });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Username");
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Password");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PhoneNumber");
                entity.Property(e => e.EmialAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmialAddress");
                entity.Property(e => e.IsAdmin)
                    .HasColumnName("IsAdmin");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ClientId)
                    .HasColumnName("ClientId");
                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Brand");
                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Model");
                entity.Property(e => e.Fix)
                    .HasColumnName("Fix");
                entity.Property(e => e.Review)
                    .HasColumnName("Review");
                entity.Property(e => e.Assembly)
                    .HasColumnName("Assembly");
                entity.Property(e => e.TechnicalConsultation)
                    .HasColumnName("TechnicalConsultation");
                entity.Property(e => e.Training)
                    .HasColumnName("Training");
                entity.Property(e => e.OrderingParts)
                    .HasColumnName("OrderingParts");
                entity.Property(e => e.NrVIN)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NrVIN");
                entity.Property(e => e.ProductionYear)
                    .HasColumnName("ProductionYear");
                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RegistrationNumber");
                entity.Property(e => e.EngineCapacity)
                    .HasColumnName("EngineCapacity");
                entity.Property(e => e.OrderState)
                    .HasColumnName("OrderState");
            });

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.Property(e => e.CarModel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CarModel");
                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Brand");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}