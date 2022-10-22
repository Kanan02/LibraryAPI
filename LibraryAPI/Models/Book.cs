using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Book
    {
        public Book()
        {
            BookGenres = new HashSet<BookGenre>();
            BookAuthors=new HashSet<BookAuthor>();
        }
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}
