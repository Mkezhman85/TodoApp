using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.ModelConfigurations;

namespace WebApi.Models
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext(DbContextOptions<TodoAppContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
        }

        public DbSet<Todo> Todo { get; set; }

        public DbSet<User> user { get; set; }
    }
}
