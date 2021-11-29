using System;
using Microsoft.EntityFrameworkCore;
using Task_2._1._1.DAL.Configuration;
using Task_2._1._1.DAL.Model;

namespace Task_2._1._1.DAL {
    class CustomersCardsContext : DbContext {
        protected CustomersCardsContext() { }
        public CustomersCardsContext(DbContextOptions options) : base(options) { }

        public DbSet<PersonalCard> PersonalCards { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(new ConnectionStringManager().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new PersonalCardConfig());
            modelBuilder.ApplyConfiguration(new PurchaseConfig());
            modelBuilder.ApplyConfiguration(new UserProfileConfig());
        }
    }
}
