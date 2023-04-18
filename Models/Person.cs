using System.ComponentModel.DataAnnotations;

namespace FilmAPI.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public List<PersonGenre>? PersonGenres { get; set; }
        public List<Rating>? Ratings { get; set; }
        public List<Movie>? Movies { get; set; }

    }
}
