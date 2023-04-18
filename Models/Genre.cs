using System.ComponentModel.DataAnnotations;
namespace FilmAPI.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<PersonGenre>? PersonGenres { get; set; }
        public List<MovieGenre>? MovieGenres { get; set; }


    }
}
