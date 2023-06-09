<img src= "https://www.themoviedb.org/assets/2/v4/logos/v2/blue_long_1-8ba2ac31f354005783fab473602c34c3f4fd207150182061e425d366e4f34596.svg">
# Moviesystem API
Made By <a href="https://www.linkedin.com/in/robert-johnson-4466101aa/"  target="_blank">Robert Johnson</a>

## A Minimal API
This project was made for the purpose of building a minimal api from scratch using a local Database and an external API.
All of the posts are sent to the database, which is built on Entity Framework and using SQL server. The point of the api is to track what movies and genres a person enjoys, and to be able to add more genres and movies, aswell as give ratings to said movies. The external API uses a "Discover" function that calls for movies that have the genres a person likes.

### The Gets & The Posts
```bash
/api/Discover/?genreName=History - Input genre (i.e Comedy) to get a list of movies that have that genre.
```
```bash
/api/Person/ - Gets a list of all people in the database and their emails.
```
```bash
/api/GetMovieByPerson/?Name={nameGoesHere} - Shows all movies and their links based on
what person added the movie.	
```
```bash
/api/Genres/Movies/Action - Shows all existing movies in the database that have a certain genre.
```
```bash
/api/moviegenre/{MovieName}?Name=Die%20Hard - Gives you the moviename, genre and link to an existing movie
based on input.
```
```bash
/api/GetRatings/Robert - Gets what a specific individual has rated a movie.
```
```bash
/api/Person/toGenre/?Name=Robert&GenreId=37 - Currently allows you to assign a genreId to a person, this 
will be changed in the future so you can only input the name of the genre, assuming it exists.
```
```bash
/api/AddMovieLink/?name=Robert&movieName={moviename}&link={Link to movie} - Allows you to add a movie with a link into the database.
```
```bash
/api/GiveRating/?Name=Ant-Man%20and%20the%20Wasp%3A%20Quantumania&rating=6 - Allows you to rate A movie
you have added.
```
```bash
/api/AddGenreToMovie/?movieName=Ant-Man%20and%20the%20Wasp%3A%20Quantumania&genreName=Science%20Fiction
Allows you to add genres to a movie.
```
```bash
/api/PersonGenre/Robert - Shows what genres a person likes.
```

### Reflection
This was a tough thing starting off, especially considering everything felt very new and unexplored.
Getting things to work straight away was a challenge, but ultimately taught me <b>a lot</b> when it came to using minimal APIs and writing them yourself.
I can see areas of improvement in the program like naming conventions etc, and this is something I want to keep building on. 
This was also a great exercise in teaching yourself thing that are unknown to you, as I started out having a headache not knowing where to start, but when the wheels started turning, I could see the light at the end of the tunnel.
I will keep exploring apis for sure because this was a fun, and challenging project to tackle, and something I will bring with me for future endeavors.
I chose to go with a minimal api in visual studio, instead of using MVC, mostly because the program felt small enough for that to suffice. 
I built the database code first, as that felt like the right thing to do, and used an ER-model I made to build the structure of the database.
You can see what <b>software</b> I used under this section.

<ul>
<li>Visual Studio
<li>SQL Server 
<li>Swagger
<li>Insomnia
<li>Entity Framework
</ul>
<b>Dependencies</b> 
<ul>
<li> EF Core
<li> EF Core Design
<li> EF Core SQL server
<li> EF Core Tools
<li> Swashbuckle AspNetCore
</ul>
