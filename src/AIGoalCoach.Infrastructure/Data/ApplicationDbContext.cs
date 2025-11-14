using AIGoalCoach.Domain.Goals;
using AIGoalCoach.Domain.GoalTasks;
using AIGoalCoach.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalTask> GoalTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //users
            modelBuilder.Entity<User>().HasIndex(u => u.EmailAddress).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();

            //Goals
            modelBuilder.Entity<Goal>().Property(g => g.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Goal>().Property(g => g.Id).ValueGeneratedOnAdd();

            //GoalTasks
            modelBuilder.Entity<GoalTask>().Property(gt => gt.IsCompleted).HasDefaultValue(false);
            modelBuilder.Entity<GoalTask>().Property(gt => gt.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<GoalTask>().Property(gt => gt.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<GoalTask>()
                .HasOne<Goal>()
                .WithMany()
                .HasForeignKey(gt => gt.GoalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
