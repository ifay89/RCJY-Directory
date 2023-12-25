using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static System.Collections.Specialized.BitVector32;

#nullable disable

namespace TestProject.DataDB
{
    public partial class RcjyDBContext : DbContext
    {
        public RcjyDBContext()
        {
        }

        public RcjyDBContext(DbContextOptions<RcjyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<EmpData> EmpData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=FAY;Database=database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasNoKey(); // Marking as keyless entity
                entity.ToTable("Sections");

                entity.Property(e => e.SecName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasNoKey(); // Marking as keyless entity
                entity.ToTable("Sectors");

                entity.Property(e => e.SectName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasNoKey(); // Marking as keyless entity
                entity.ToTable("Departments");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DeptNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpData>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.ToTable("EmpData");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CISCO)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.HidePhoneNumber)
                    .IsRequired()  // You may change this based on your requirements
                    .HasDefaultValue(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
