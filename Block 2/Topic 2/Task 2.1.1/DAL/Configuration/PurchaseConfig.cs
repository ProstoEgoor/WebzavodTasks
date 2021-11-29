using System;
using Task_2._1._1.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text;

namespace Task_2._1._1.DAL.Configuration {
    class PurchaseConfig : IEntityTypeConfiguration<Purchase> {
        public void Configure(EntityTypeBuilder<Purchase> builder) {
            builder.ToTable("purchase");

            builder.HasKey("Id");

            builder.Property(purchase => purchase.Id)
                .HasColumnName("id")
                .HasColumnType("bigint");

            builder.Property(purchase => purchase.CardId)
                .HasColumnName("card_id")
                .HasColumnType("bigint");

            builder.Property(purchase => purchase.PurchaseSum)
                .HasColumnName("purchase_sum")
                .HasColumnType("decimal(18,0)");

            builder
                .HasOne(purchase => purchase.PersonalCard)
                .WithMany(personalCard => personalCard.Purchases)
                .HasForeignKey("CardId");
        }
    }
}
