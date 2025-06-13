using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence
{
    public class TodoAppDBContext : DbContext
    {

        public TodoAppDBContext() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>()
               .HasKey(u => u.Id)                             
               .Property(u => u.Id)
               .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Todos)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(true);

            // Todo configuration

            modelBuilder.Entity<Todo>().ToTable("Todos");

            modelBuilder.Entity<Todo>()
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
