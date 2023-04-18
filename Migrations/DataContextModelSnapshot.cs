﻿// <auto-generated />
using FilmAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FilmAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FilmAPI.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("FilmAPI.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FkPersonId")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FkPersonId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("FilmAPI.Models.MovieGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FkGenreId")
                        .HasColumnType("int");

                    b.Property<int>("FkMovieid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FkGenreId");

                    b.HasIndex("FkMovieid");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("FilmAPI.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("FilmAPI.Models.PersonGenre", b =>
                {
                    b.Property<int>("PersonGenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonGenreId"));

                    b.Property<int>("FkGenreId")
                        .HasColumnType("int");

                    b.Property<int>("FkPersonId")
                        .HasColumnType("int");

                    b.HasKey("PersonGenreId");

                    b.HasIndex("FkGenreId");

                    b.HasIndex("FkPersonId");

                    b.ToTable("PersonGenres");
                });

            modelBuilder.Entity("FilmAPI.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<int>("FkMovieId")
                        .HasColumnType("int");

                    b.Property<int>("FkPersonId")
                        .HasColumnType("int");

                    b.Property<int>("Ratings")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("FkMovieId");

                    b.HasIndex("FkPersonId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("FilmAPI.Models.Movie", b =>
                {
                    b.HasOne("FilmAPI.Models.Person", "Persons")
                        .WithMany("Movies")
                        .HasForeignKey("FkPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persons");
                });

            modelBuilder.Entity("FilmAPI.Models.MovieGenre", b =>
                {
                    b.HasOne("FilmAPI.Models.Genre", "Genres")
                        .WithMany("MovieGenres")
                        .HasForeignKey("FkGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmAPI.Models.Movie", "Movies")
                        .WithMany("MovieGenres")
                        .HasForeignKey("FkMovieid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genres");

                    b.Navigation("Movies");
                });

            modelBuilder.Entity("FilmAPI.Models.PersonGenre", b =>
                {
                    b.HasOne("FilmAPI.Models.Genre", "Genres")
                        .WithMany("PersonGenres")
                        .HasForeignKey("FkGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmAPI.Models.Person", "Persons")
                        .WithMany("PersonGenres")
                        .HasForeignKey("FkPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genres");

                    b.Navigation("Persons");
                });

            modelBuilder.Entity("FilmAPI.Models.Rating", b =>
                {
                    b.HasOne("FilmAPI.Models.Movie", "Movies")
                        .WithMany("Ratings")
                        .HasForeignKey("FkMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmAPI.Models.Person", "Persons")
                        .WithMany("Ratings")
                        .HasForeignKey("FkPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movies");

                    b.Navigation("Persons");
                });

            modelBuilder.Entity("FilmAPI.Models.Genre", b =>
                {
                    b.Navigation("MovieGenres");

                    b.Navigation("PersonGenres");
                });

            modelBuilder.Entity("FilmAPI.Models.Movie", b =>
                {
                    b.Navigation("MovieGenres");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("FilmAPI.Models.Person", b =>
                {
                    b.Navigation("Movies");

                    b.Navigation("PersonGenres");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
