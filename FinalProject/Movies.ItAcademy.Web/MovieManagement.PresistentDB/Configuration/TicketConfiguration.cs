using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.PresistentDB.Configuration
{
    public class TicketConfiguration:IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {

            builder.HasKey(x => new { x.UserId, x.MovieId });

            builder.HasOne<User>(um => um.User)
                    .WithMany(u => u.Tickets).OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(u => u.UserId);

            builder.HasOne<Movie>(um => um.Movie)
                  .WithMany(m => m.Tickets).OnDelete(DeleteBehavior.Cascade)
                  .HasForeignKey(m => m.MovieId);
        }

    }
}
