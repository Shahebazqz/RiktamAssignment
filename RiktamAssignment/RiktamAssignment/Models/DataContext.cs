using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Models
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<LikedMessage> LikedMessages { get; set; }

        public DataContext() : base()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          //  optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RiktamDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GroupUser>()
               .HasKey(g => new { g.GroupId, g.UserId });

            modelBuilder.Entity<GroupUser>()
                .HasOne(g => g.Group)
                .WithMany(gu => gu.Members)
                .HasForeignKey(g => g.UserId);

            modelBuilder.Entity<GroupUser>()
               .HasOne(g => g.User)
               .WithMany(gu => gu.Groups)
               .HasForeignKey(g => g.GroupId);

            modelBuilder.Entity<GroupMessage>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Messages)
                .HasForeignKey(m => m.GroupId);
        }
    }
}
