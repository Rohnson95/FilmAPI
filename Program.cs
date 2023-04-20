using FilmAPI.Data;
using FilmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmAPI
{
    public class Program
    {
        /*
        [X] Det ska gå att lagra personer med grundläggande information om dem som namn och epostadresser.
        [X] Systemet ska kunna lagra ett obegränsat antal genres som de gillar. Varje genre ska ha en titel och en kort beskrivning.
        [X] Varje person ska kunna vara intresserad av valfritt antal genres
        [X] Det ska gå att lagra ett obegränsat antal länkar (till filmer) till varje genre för varje person. Om en person lägger in en länk så är den alltså kopplad både till den personen och till den genren.
        [X] Skapa applikationen med hänsyn till Repository pattern. 
        [X] Hämta alla personer i systemet
        [X] Hämta alla genrer som är kopplade till en specifik person
        [X] Hämta alla filmer som är kopplade till en specifik person
        [X] Lägga in och hämta "rating" på filmer kopplat till en person
        [X] Koppla en person till en ny genre
        [X] Lägga in nya länkar för en specifik person och en specifik genre
        [X] Få förslag på filmer i en viss genre från ett externt API, t.ex TMDB.Links to an external site.
         */
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var client = new HttpClient();

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
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapGet("/api/Discover/", async (DataContext context, string genreName) =>
            {
                var client = new HttpClient();
                var gen = await context.Genres.SingleOrDefaultAsync(g => g.Title == genreName);
                string apiKey = "";
                string apiUrl = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_keywords=action&with_watch_monetization_types=flatrate&with_genres={gen.GenreId}";
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();



                return Results.Content(content, contentType: "application/json");
            });
            app.MapGet("/api/Person/", async (DataContext context) =>
            {
                var query = from persons in context.Persons
                            select new { persons.FirstName, persons.Email };
                return await query.ToListAsync();
            });
            app.MapGet("/api/GetMovieByPerson/", async (DataContext context, string Name) =>
            {
                var query = from rat in context.Ratings
                            select new
                            {
                                rat.Persons.FirstName,
                                rat.Movies.Name,
                                rat.Movies.Link
                            };
                var result = query.GroupBy(x => x.FirstName).Select(x => new { Name = x.Key, Movie = x.Select(y => y.Name), Link = x.Select(z => z.Link).Distinct() }).Where(x => x.Name == Name).ToListAsync();
                return await result;
            });
            app.MapGet("/api/Genres/Movies/{Name}", async (DataContext context, string Name) =>
            {
                var movies = context.Movies;
                var persons = context.Persons;
                var movieGenre = context.MovieGenres;
                var genre = context.Genres;
                var query = from mov in movies
                            join movgen in movieGenre on mov.Id equals movgen.FkMovieid
                            join gen in genre on movgen.FkGenreId equals gen.GenreId

                            select new
                            {
                                mov.Name,
                                gen.Title,
                                mov.Link,
                                mov.Persons.FirstName,
                                gen.GenreId



                            };
                return await query.Where(x => x.Title == Name).ToListAsync();
            });
            app.MapGet("/api/moviegenre/{Name}", async (DataContext context, string Name) =>
            {
                var movieGenre = context.MovieGenres;
                var query = from movgen in movieGenre
                                //join movgen in movieGenre on movs.Id equals movgen.FkMovieid
                                //join gen in genre on movgen.FkGenreId equals gen.GenreId

                            select new
                            {
                                movgen.Movies.Name,
                                movgen.Movies.Link,
                                movgen.Genres.Title


                            };
                var result = query.GroupBy(x => x.Name)
                                .Select(x => new { Name = x.Key, Genre = string.Join(", ", x.Select(y => y.Title)), Link = string.Join(", ", x.Select(z => z.Link).Distinct()) }).Where(x => x.Name == Name).ToListAsync();
                return await result;
            });
            app.MapGet("/api/GetRatings/{Name}", async (DataContext context, string Name) =>
            {
                var query = from movrat in context.Ratings
                            select new
                            {
                                movrat.Persons.FirstName,
                                movrat.Movies.Name,
                                movrat.Ratings
                            };
                return await query.Where(x => x.FirstName == Name).ToListAsync();

            });
            app.MapPost("/api/Person/toGenre/", async (DataContext context, string Name, int GenreId) =>
            {
                var person = await context.Persons.SingleOrDefaultAsync(p => p.FirstName == Name);
                if (person == null)
                {
                    return Results.NotFound();

                }
                var perGen = context.PersonGenres;
                perGen.Add(new PersonGenre { FkPersonId = person.PersonId, FkGenreId = GenreId });
                await context.SaveChangesAsync();
                return Results.Created($"/api/Person/toGenre/", GenreId);
            });
            app.MapPost("/api/GiveRating/", async (DataContext context, string Name, int rating) =>
            {
                var mov = await context.Movies.SingleOrDefaultAsync(m => m.Name == Name);
                var rat = context.Ratings;
                rat.Add(new Rating { FkMovieId = mov.Id, FkPersonId = mov.FkPersonId, Ratings = rating });
                await context.SaveChangesAsync();
                return Results.Created($"/api/GiveRating/", rating);
            });
            app.MapPost("/api/AddMovieLink/", async (DataContext context, string name, string movieName, string link) =>
            {
                var person = await context.Persons.SingleOrDefaultAsync(p => p.FirstName == name);
                if (person == null)
                {
                    return Results.NotFound();
                }
                var addM = context.Movies;
                var addMg = context.MovieGenres;
                addM.Add(new Movie { FkPersonId = person.PersonId, Name = movieName, Link = link });
                await context.SaveChangesAsync();
                return Results.Created($"/api/AddMovieLink/", movieName);
            });
            app.MapPost("/api/AddGenreToMovie/", async (DataContext context, string movieName, string genreName) =>
            {
                var movie = await context.Movies.SingleOrDefaultAsync(m => m.Name == movieName);
                var movgen = context.MovieGenres;
                var gen = await context.Genres.SingleOrDefaultAsync(g => g.Title == genreName);
                if (movie == null)
                {
                    return Results.NotFound();
                }
                movgen.Add(new MovieGenre { FkGenreId = gen.GenreId, FkMovieid = movie.Id });
                await context.SaveChangesAsync();
                return Results.Created($"/api/AddGenreToMovie/", movieName);
            });
            app.MapGet("/api/PersonGenre/{Name}", async (DataContext context, string Name) =>
            {
                var pGen = context.PersonGenres;
                var query = from pg in pGen select new { pg.Persons.FirstName, pg.Genres.Title };
                var result = query.GroupBy(x => x.FirstName)
                                  .Select(x => new { Name = x.Key, Genre = string.Join(", ", x.Select(y => y.Title)) }).Where(x => x.Name == Name).ToListAsync();
                return await result;
            });
            app.MapGet("/api/MovieByGenre/{Name}", async (DataContext context, string Name) =>
            {
                var searchBy = context.MovieGenres;
                var query = from sb in searchBy select new { sb.Movies.Name, sb.Genres.Title };
                var result = query.GroupBy(x => x.Title).Select(x => new { Name = x.Key, Genre = string.Join(", ", x.Select(y => y.Name)) }).Where(x => x.Genre == Name).ToListAsync();
                return await result;
            });
            app.Run();
        }
    }
}