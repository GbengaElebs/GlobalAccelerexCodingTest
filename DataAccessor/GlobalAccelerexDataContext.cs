using System;
using DbModels.GlobalAccelerex;
using Microsoft.EntityFrameworkCore;

namespace DataAccessor.GlobalAccelerex
{
    public partial class GlobalAccelerexDataContext : DbContext
    {
        public GlobalAccelerexDataContext(DbContextOptions<GlobalAccelerexDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CharacterData> CharacterData { get; set; }
        public virtual DbSet<CommentsData> CommentsData { get; set; }
        public virtual DbSet<EpisodeCharacter> EpisodeCharacter { get; set; }
        public virtual DbSet<EpisodeData> EpisodeData { get; set; }
        public virtual DbSet<LocationData> LocationData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CharacterData>(entity =>
            {
                entity.HasKey(e => e.CharacterId)
                    .HasName("PRIMARY");

                entity.ToTable("CharacterData", "GlobalAccelerexdb");

                entity.HasIndex(e => e.LocationId)
                    .HasName("Location_idx");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnType("enum('MALE','FEMALE')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StateOfOrigin).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('ACTIVE','DEAD','UNKNOWN')");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CharacterData)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("Location");
                entity.Property<DateTime>(e => e.Created)
                        .HasColumnType("datetime(6)");
            });

            modelBuilder.Entity<CommentsData>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PRIMARY");

                entity.ToTable("CommentsData", "GlobalAccelerexdb");

                entity.HasIndex(e => e.EpisodeId)
                    .HasName("Episode_idx");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.IpAddressLocation)
                    .IsRequired()
                    .HasMaxLength(45);
                entity.Property<DateTime>(e => e.Created)
                        .HasColumnType("datetime(6)");

                entity.HasOne(d => d.Episode)
                    .WithMany(p => p.CommentsData)
                    .HasForeignKey(d => d.EpisodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Episode");
            });

            modelBuilder.Entity<EpisodeCharacter>(entity =>
            {
                entity.ToTable("EpisodeCharacter", "GlobalAccelerexdb");

                entity.HasIndex(e => e.CharacterId)
                    .HasName("CharacterId_idx");

                entity.HasIndex(e => e.EpisodeId)
                    .HasName("EpisodeId_idx");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.EpisodeCharacter)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CharacterId");

                entity.HasOne(d => d.Episode)
                    .WithMany(p => p.EpisodeCharacter)
                    .HasForeignKey(d => d.EpisodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EpisodeId");

                
            });

            modelBuilder.Entity<EpisodeData>(entity =>
            {
                entity.HasKey(e => e.EpisodeId)
                    .HasName("PRIMARY");

                entity.ToTable("EpisodeData", "GlobalAccelerexdb");

                entity.Property(e => e.EpisodeCode)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property<DateTime>(e => e.Created)
                        .HasColumnType("datetime(6)");
                entity.Property<DateTime>(e => e.ReleaseDate)
                        .HasColumnType("datetime(6)");
                
            });

            modelBuilder.Entity<LocationData>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PRIMARY");

                entity.ToTable("LocationData", "GlobalAccelerexdb");

                entity.Property(e => e.Latitude).HasColumnType("double(18,4)");

                entity.Property(e => e.Longitude).HasColumnType("double(18,4)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property<DateTime>(e => e.Created)
                        .HasColumnType("datetime(6)");
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
