using FilmAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /*app.MapGet("/api/Movies/{Name}", async (DataContext context, string Name) =>
            {
                var movies = context.Movies;
                var moviePerson = movies.Join(context.Persons, movie => movie.FkPersonId,
                    per => per.PersonId, (movie, per) => new
                    {
                        movie.Name,
                        movie.Link,
                        per.FirstName

                    }).FirstOrDefaultAsync(x => x.Name == Name);
                return await moviePerson;
            });
            */

            app.MapGet("/api/Movies/{Name}", async (DataContext context, string Name) =>
            {
                var movies = context.Movies;
                var persons = context.Persons;
                var ratings = context.Ratings;
                var movieGenre = context.MovieGenres;
                var genre = context.Genres;
                var query = from pers in persons
                            join mov in movies on pers.PersonId equals mov.FkPersonId
                            join rat in ratings on mov.Id equals rat.FkMovieId

                            select new
                            {
                                pers.FirstName,
                                mov.Name,
                                rat.Ratings



                            };
                return await query.FirstOrDefaultAsync(x => x.Name == Name);
            });
            app.MapGet("api/moviegenre/{Name}", async (DataContext context, string Name) =>
            {
                var movies = context.Movies;
                var movieGenre = context.MovieGenres;
                var genre = context.Genres;
                var query = from movs in movies
                            join movgen in movieGenre on movs.Id equals movgen.FkMovieid
                            join gen in genre on movgen.FkGenreId equals gen.GenreId

                            select new
                            {
                                movgen.Movies.Name,
                                movgen.Genres.Title
                            };
                var result = query.GroupBy(x => x.Name)
                                .Select(x => new { Name = x.Key, Title = string.Join(", ", x.Select(y => y.Title)) }).FirstOrDefaultAsync(x => x.Name == Name);
                return await result;//query.GroupBy(x => x.Title).ToListAsync();
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/", () => "Welcome to minimal APIs");
            app.MapGet("/api/Movie/", async (DataContext context) => await context.Movies.ToListAsync());
            app.MapGet("/api/Genre/", async (DataContext context) => await context.Genres.ToListAsync());
            app.MapGet("/api/Person/{Name}", async (DataContext context, string Name) => await context.Persons.FirstOrDefaultAsync(x => x.FirstName == Name));
            app.Run();
        }
    }
}