using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MuseumIstanbul.Models;

namespace MuseumIstanbul.Data;

	public class ApplicationContext : IdentityDbContext<User>
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
   
		public DbSet<Museum>? Museums { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Museum>()
            .HasMany(m => m.Comments)
            .WithOne(c => c.Museum)
            .HasForeignKey(c => c.MuseumId);
        }
}

