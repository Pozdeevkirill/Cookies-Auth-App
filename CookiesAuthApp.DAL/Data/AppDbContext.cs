using CookiesAuthApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            Role admin = new() { Id = 1, Name = "admin" };
            Role moderator = new() { Id = 2, Name = "moderator" };
            Role user = new() { Id = 3, Name = "user" };

            builder.Entity<Role>().HasData(new Role[] { admin, moderator, user });
            base.OnModelCreating(builder);
        }
    }
}
