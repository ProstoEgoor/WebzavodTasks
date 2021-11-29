using System;
using Task_2._1._1.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text;

namespace Task_2._1._1.DAL.Configuration {
    class PersonalCardConfig : IEntityTypeConfiguration<PersonalCard> {
        public void Configure(EntityTypeBuilder<PersonalCard> builder) {
            builder.ToTable("personal_card");

            builder.HasKey("Id");

            builder.Property(personalCard => personalCard.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .HasColumnType("bigint");

            builder.Property(personalCard => personalCard.Discount)
                .HasColumnName("discount")
                .HasColumnType("real");
        }
    }
}
