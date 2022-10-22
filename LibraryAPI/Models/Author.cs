using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Author
    {
        public Author()
        {
           
            BookAuthors = new HashSet<BookAuthor>();
        }
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
