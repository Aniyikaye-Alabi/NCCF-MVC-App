using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NCCF_MVC_App.Models
{
    public partial class NCCF_DatabaseContext : DbContext
    {
        public NCCF_DatabaseContext()
        {
        }

        public NCCF_DatabaseContext(DbContextOptions<NCCF_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<FoodStuff> FoodStuffs { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<UsersProfile> UsersProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NC277NA\\SQLEXPRESS;Database=NCCF_Database;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateExpired).HasColumnType("datetime");

                entity.Property(e => e.EquipmentName).HasMaxLength(50);
            });

            modelBuilder.Entity<FoodStuff>(entity =>
            {
                entity.ToTable("FoodStuff");

                entity.Property(e => e.DateAdded).HasColumnType("date");

                entity.Property(e => e.DateUpdated).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberName).HasMaxLength(50);

                entity.Property(e => e.PostHeld).HasMaxLength(50);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomCondition).HasMaxLength(50);

                entity.Property(e => e.RoomName).HasMaxLength(50);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<UsersProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UsersProfile");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Password).HasMaxLength(25);

                entity.Property(e => e.UserName).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
