using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.PresistentDB.Context
{
    public class MovieManagementContext:IdentityDbContext<User>//, IdentityRole,string>
    {
        public MovieManagementContext(DbContextOptions<MovieManagementContext> options) : base(options)
        {

        }
            
        public DbSet<User> User { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieManagementContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
