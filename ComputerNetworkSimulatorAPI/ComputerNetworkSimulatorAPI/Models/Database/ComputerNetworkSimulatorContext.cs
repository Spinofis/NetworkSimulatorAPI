using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class ComputerNetworkSimulatorContext : DbContext
    {
        public ComputerNetworkSimulatorContext()
        {
        }

        public ComputerNetworkSimulatorContext(DbContextOptions<ComputerNetworkSimulatorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TestTable> TestTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=BARTEKP;Database=ComputerNetworkSimulator;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
