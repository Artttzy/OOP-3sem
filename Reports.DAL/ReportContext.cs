using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Core.Models;

namespace Reports.DAL
{
    public class ReportContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<WeeklyReport> WeeklyReports { get; set; }
        public DbSet<Change> Changes { get; set; }

        public ReportContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Goals)
                .WithOne(g => g.Owner);


            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Slaves)
                .WithOne(s => s.Manager);
            modelBuilder.Entity<WeeklyReport>()
                .HasMany(d => d.DailyReports)
                .WithOne(w => w.WeeklyReport);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.DailyReports)
                .WithOne(s => s.Owner);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.WeeklyReports)
                .WithOne(s => s.Owner);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Reports.db");
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.GetHashCode();
        }
    }
}