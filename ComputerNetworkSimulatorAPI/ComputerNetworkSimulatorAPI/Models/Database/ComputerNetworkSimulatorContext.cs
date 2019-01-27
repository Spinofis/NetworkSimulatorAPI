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

        public virtual DbSet<Links> Links { get; set; }
        public virtual DbSet<Pc> Pc { get; set; }
        public virtual DbSet<Router> Router { get; set; }
        public virtual DbSet<Simulation> Simulation { get; set; }
        public virtual DbSet<Switch> Switch { get; set; }

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
            modelBuilder.Entity<Links>(entity =>
            {
                entity.Property(e => e.NodeNumber1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NodeNumber2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSimNavigation)
                    .WithMany(p => p.Links)
                    .HasForeignKey(d => d.IdSim)
                    .HasConstraintName("FK_Links_Simulation");
            });

            modelBuilder.Entity<Pc>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HostIdentity)
                    .HasColumnName("hostIdentity")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdSim).HasColumnName("id_sim");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NodeNumber)
                    .HasColumnName("node_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PcNumber).HasColumnName("pc_number");

                entity.HasOne(d => d.IdSimNavigation)
                    .WithMany(p => p.Pc)
                    .HasForeignKey(d => d.IdSim)
                    .HasConstraintName("FK_Pc_Simulation");
            });

            modelBuilder.Entity<Router>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HostIdentity)
                    .HasColumnName("hostIdentity")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdSim).HasColumnName("id_sim");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NodeNumber)
                    .HasColumnName("node_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RouterNumber).HasColumnName("router_number");

                entity.HasOne(d => d.IdSimNavigation)
                    .WithMany(p => p.Router)
                    .HasForeignKey(d => d.IdSim)
                    .HasConstraintName("FK_Router_Simulation");
            });

            modelBuilder.Entity<Simulation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateAdd)
                    .HasColumnName("date_add")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateEdit)
                    .HasColumnName("date_edit")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Switch>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HostIdentity)
                    .HasColumnName("hostIdentity")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdSim).HasColumnName("id_sim");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NodeNumber)
                    .HasColumnName("node_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SwitchNumber).HasColumnName("switch_number");

                entity.HasOne(d => d.IdSimNavigation)
                    .WithMany(p => p.Switch)
                    .HasForeignKey(d => d.IdSim)
                    .HasConstraintName("FK_Switch_Simulation");
            });
        }
    }
}
