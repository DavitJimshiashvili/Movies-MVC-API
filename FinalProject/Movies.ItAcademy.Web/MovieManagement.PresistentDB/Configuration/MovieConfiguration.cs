using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.PresistentDB.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasIndex(x => x.Tittle).IsUnique();

            builder.Property(x => x.Tittle).HasMaxLength(200).IsUnicode(false);

            builder.Property(x => x.Status).IsRequired();

            builder.Property(x => x.Country).HasMaxLength(40).IsUnicode(false);


        }
    }
}
