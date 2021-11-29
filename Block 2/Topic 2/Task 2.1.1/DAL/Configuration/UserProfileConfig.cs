using System;
using Task_2._1._1.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text;

namespace Task_2._1._1.DAL.Configuration {
    class UserProfileConfig : IEntityTypeConfiguration<UserProfile> {
        public void Configure(EntityTypeBuilder<UserProfile> builder) {
            builder.ToTable("user_profile");

            builder.HasKey("UserId");

            builder.Property(userProfile => userProfile.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id")
                .HasColumnType("bigint");

            builder.Property(userProfile => userProfile.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(userProfile => userProfile.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("nvarchar(50)");

            builder.Property(userProfile => userProfile.LastName)
                .HasColumnName("last_name")
                .HasColumnType("nvarchar(50)");

            builder.Property(userProfile => userProfile.Birthdate)
                .HasColumnName("birthdate")
                .HasColumnType("date");

            builder
                .HasOne(userProfile => userProfile.PersonalCard)
                .WithOne(personalCard => personalCard.UserProfile)
                .HasForeignKey<UserProfile>("UserId");
        }
    }
}
