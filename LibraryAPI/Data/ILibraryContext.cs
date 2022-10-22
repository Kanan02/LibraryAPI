using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
    public interface ILibraryContext
    {
        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; } 
        DbSet<Genre> Genres { get; set; } 
        DbSet<BookAuthor> BookAuthors { get; set; } 
        DbSet<BookGenre> BookGenres { get; set; } 

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
