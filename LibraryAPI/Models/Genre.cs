using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Genre
    {
        public Genre()
        {
            BookGenres = new HashSet<BookGenre>();
        }
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}
