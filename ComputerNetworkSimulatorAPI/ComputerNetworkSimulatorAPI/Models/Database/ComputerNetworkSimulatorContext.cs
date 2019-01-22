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

        public virtual DbSet<Pc> Pc { get; set; }
        public virtual DbSet<PcSwitch> PcSwitch { get; set; }
        public virtual DbSet<Router> Router { get; set; }
        public virtual DbSet<RouterInterface> RouterInterface { get; set; }
        public virtual DbSet<RouterSwitch> RouterSwitch { get; set; }
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
            modelBuilder.Entity<Pc>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdSim).HasColumnName("id_sim");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mask)
                    .HasColumnName("mask")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSimNavigation)
                    .WithMany(p => p.Pc)
                    .HasForeignKey(d => d.IdSim)
                    .HasConstraintName("FK_Pc_Simulation");
            });

            modelBuilder.Entity<PcSwitch>(entity =>
            {
                entity.ToTable("Pc_Switch");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdPc).HasColumnName("id_pc");

                entity.Property(e => e.IdSwitch).HasColumnName("id_switch");

                entity.HasOne(d => d.IdPcNavigation)
                    .WithMany(p => p.PcSwitch)
                    .HasForeignKey(d => d.IdPc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pc_Switch_Pc");

                entity.HasOne(d => d.IdSwitchNavigation)
                    .WithMany(p => p.PcSwitch)
                    .HasForeignKey(d => d.IdSwitch)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pc_Switch_Switch");
            });

            modelBuilder.Entity<Router>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdSim).HasColumnName("id_sim");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSimNavigation)
                    .WithMany(p => p.Router)
                    .HasForeignKey(d => d.IdSim)
                    .HasConstraintName("FK_Router_Simulation");
            });

            modelBuilder.Entity<RouterInterface>(entity =>
            {
                entity.ToTable("Router_interface");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdRouter).HasColumnName("id_router");

                entity.Property(e => e.IpHost)
                    .HasColumnName("ip_host")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IpNet)
                    .HasColumnName("ip_net")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mask)
                    .HasColumnName("mask")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRouterNavigation)
                    .WithMany(p => p.RouterInterface)
                    .HasForeignKey(d => d.IdRouter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Router_interface_Router");
            });

            modelBuilder.Entity<RouterSwitch>(entity =>
            {
                entity.ToTable("Router_switch");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdRouter).HasColumnName("id_router");

                entity.Property(e => e.IdSwitch).HasColumnName("id_switch");

                entity.HasOne(d => d.IdRouterNavigation)
                    .WithMany(p => p.RouterSwitch)
                    .HasForeignKey(d => d.IdRouter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Router_switch_Router");

                entity.HasOne(d => d.IdSwitchNavigation)
                    .WithMany(p => p.RouterSwitch)
                    .HasForeignKey(d => d.IdSwitch)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Router_switch_Switch");
            });

            modelBuilder.Entity<Simulation>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdSim).HasColumnName("id_sim");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSimNavigation)
                    .WithMany(p => p.Switch)
                    .HasForeignKey(d => d.IdSim)
                    .HasConstraintName("FK_Switch_Simulation");
            });
        }
    }
}
