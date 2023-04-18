using FilmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PersonGenre> PersonGenres { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=DESKTOP-2TAA998; Initial Catalog=MovieAPIDb;Integrated Security=true; TrustServerCertificate=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonGenre>()
                .HasOne(pg => pg.Persons)
                .WithMany(pg => pg.PersonGenres)
                .HasForeignKey(fkp => fkp.FkPersonId);
            modelBuilder.Entity<PersonGenre>()
                .HasOne(persg => persg.Genres)
                .WithMany(persg => persg.PersonGenres)
                .HasForeignKey(persfk => persfk.FkGenreId);
            modelBuilder.Entity<Rating>()
                .HasOne(pr => pr.Persons)
                .WithMany(pr => pr.Ratings)
                .HasForeignKey(pr => pr.FkPersonId);
            modelBuilder.Entity<Rating>()
                .HasOne(pm => pm.Movies)
                .WithMany(pm => pm.Ratings)
                .HasForeignKey(pmfk => pmfk.FkMovieId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movies)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(mg => mg.FkMovieid);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(gmg => gmg.Genres)
                .WithMany(gmg => gmg.MovieGenres)
                .HasForeignKey(gmg => gmg.FkGenreId);
            modelBuilder.Entity<Movie>()
                .HasOne(p => p.Persons)
                .WithMany(p => p.Movies)
                .HasForeignKey(p => p.FkPersonId);

        }
    }

}
