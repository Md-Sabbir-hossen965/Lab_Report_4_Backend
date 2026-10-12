using System;
using System.Collections.Generic;
using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data;

public partial class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCharacter> MovieCharacters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Actor__7EABD08BD1437FF8");

            entity.ToTable("Actor");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("Person_ID");
            entity.Property(e => e.ActorName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Actor_Name");
            entity.Property(e => e.BirthYear).HasColumnName("Birth_Year");
            entity.Property(e => e.NoOfAwards).HasColumnName("No_Of_Awards");
            entity.Property(e => e.NoOfFilms).HasColumnName("No_Of_Films");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Director__7EABD08BB5129F43");

            entity.ToTable("Director");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("Person_ID");
            entity.Property(e => e.BirthYear).HasColumnName("Birth_Year");
            entity.Property(e => e.DirectorName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Director_Name");
            entity.Property(e => e.NoOfAwards).HasColumnName("No_Of_Awards");
            entity.Property(e => e.NoOfFilms).HasColumnName("No_Of_Films");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__7A8804056ECFC60D");

            entity.ToTable("Movie");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("Movie_ID");
            entity.Property(e => e.DirectorId).HasColumnName("Director_ID");
            entity.Property(e => e.Genre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ImdbRating)
                .HasColumnType("numeric(3, 1)")
                .HasColumnName("IMDb_Rating");
            entity.Property(e => e.MovieName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Movie_Name");

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK_Movie_Director");
        });

        modelBuilder.Entity<MovieCharacter>(entity =>
        {
            entity.HasKey(e => e.CharacterId).HasName("PK__Movie_Ch__5F617C5264C81A05");

            entity.ToTable("Movie_Characters");

            entity.Property(e => e.CharacterId)
                .ValueGeneratedNever()
                .HasColumnName("Character_ID");
            entity.Property(e => e.CharacterName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Character_Name");

            entity.HasMany(d => d.Movies).WithMany(p => p.Characters)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieCharacterRelationship",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MCR_Movie"),
                    l => l.HasOne<MovieCharacter>().WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MCR_Character"),
                    j =>
                    {
                        j.HasKey("CharacterId", "MovieId");
                        j.ToTable("Movie_Character_Relationship");
                        j.IndexerProperty<int>("CharacterId").HasColumnName("Character_ID");
                        j.IndexerProperty<int>("MovieId").HasColumnName("Movie_ID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
