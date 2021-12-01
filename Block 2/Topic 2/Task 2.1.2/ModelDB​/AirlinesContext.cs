using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Task_2._1._2.ModelDB​ {
    public partial class AirlinesContext : DbContext {

        public static T UsingDBContext<T>(Func<AirlinesContext, T> action, string connectionString = null) {
            var optionBuilder = new DbContextOptionsBuilder<AirlinesContext>();
            var options = optionBuilder
                .UseSqlServer(connectionString ?? new ConnectionStringManager().ConnectionString)
                .Options;

            using var context = new AirlinesContext(options);
            return action(context);
        }

        public AirlinesContext() {
        }

        public AirlinesContext(DbContextOptions<AirlinesContext> options)
            : base(options) {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<PassInTrip> PassInTrips { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(new ConnectionStringManager().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Company>(entity => {
                entity.HasKey(e => e.IdComp)
                    .HasName("PK__Company__744AB6C9F702065C");

                entity.ToTable("Company");

                entity.Property(e => e.IdComp).HasColumnName("ID_comp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PassInTrip>(entity => {
                entity.HasKey(e => new { e.TripNo, e.Date, e.IdPsg })
                    .HasName("PK__Pass_in___ECA074393D2F488D");

                entity.ToTable("Pass_in_trip");

                entity.Property(e => e.TripNo).HasColumnName("trip_no");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.IdPsg).HasColumnName("ID_psg");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("place")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdPsgNavigation)
                    .WithMany(p => p.PassInTrips)
                    .HasForeignKey(d => d.IdPsg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pass_in_t__ID_ps__2B3F6F97");

                entity.HasOne(d => d.TripNoNavigation)
                    .WithMany(p => p.PassInTrips)
                    .HasForeignKey(d => d.TripNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pass_in_t__trip___2A4B4B5E");
            });

            modelBuilder.Entity<Passenger>(entity => {
                entity.HasKey(e => e.IdPsg)
                    .HasName("PK__Passenge__18AE37007144A852");

                entity.ToTable("Passenger");

                entity.Property(e => e.IdPsg).HasColumnName("ID_psg");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Trip>(entity => {
                entity.HasKey(e => e.TripNo)
                    .HasName("PK__Trip__302538110F529737");

                entity.ToTable("Trip");

                entity.Property(e => e.TripNo).HasColumnName("trip_no");

                entity.Property(e => e.IdComp).HasColumnName("ID_comp");

                entity.Property(e => e.Plane)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("plane")
                    .IsFixedLength(true);

                entity.Property(e => e.TimeIn)
                    .HasColumnType("datetime")
                    .HasColumnName("time_in");

                entity.Property(e => e.TimeOut)
                    .HasColumnType("datetime")
                    .HasColumnName("time_out");

                entity.Property(e => e.TownFrom)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("town_from")
                    .IsFixedLength(true);

                entity.Property(e => e.TownTo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("town_to")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdCompNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.IdComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trip__ID_comp__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
