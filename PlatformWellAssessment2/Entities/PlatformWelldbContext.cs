using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PlatformWellAssessment2.Models;

namespace PlatformWellAssessment2.Data
{
    public partial class PlatformWelldbContext : DbContext
    {
        public PlatformWelldbContext()
        {
        }

        public PlatformWelldbContext(DbContextOptions<PlatformWelldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Platform> Platforms { get; set; } = null!;
        public virtual DbSet<Well> Wells { get; set; } = null!;

        public virtual DbSet<PlatformExcel> PlatformExcel { get; set; } = null!;

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Data Source=FAZLIAMRI\\SQLEXPRESS; Initial Catalog=PlatformWelldb; trusted_connection=true; TrustServerCertificate=true;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>(entity =>
            {
                entity.ToTable("platforms");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.UniqueName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("uniqueName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Well>(entity =>
            {
                entity.ToTable("wells");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.PlatformId).HasColumnName("platformId");

                entity.Property(e => e.UniqueName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("uniqueName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<PlatformExcel>(entity =>
            {
                entity.ToTable("platformExcel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.UniqueName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("uniqueName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
