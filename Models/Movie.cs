using System.ComponentModel.DataAnnotations;
namespace FilmAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Link { get; set; }
        public int FkPersonId { get; set; }
        public virtual Person? Persons { get; set; }
        public List<Rating>? Ratings { get; set; }
        //public List<PersonGenre>? PersonGenre { get; set; }
        public List<MovieGenre>? MovieGenres { get; set; }

    }
}
