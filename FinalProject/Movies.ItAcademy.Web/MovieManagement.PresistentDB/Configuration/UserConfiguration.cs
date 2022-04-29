using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.PresistentDB.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasIndex(x => x.UserName).IsUnique();

           // builder.HasKey(x => x.Id); // თუ key -დ აღებულია Id, Ef თვითონ ხვდება 
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);

            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();


            builder.Property(x => x.Password).IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.Tickets).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

