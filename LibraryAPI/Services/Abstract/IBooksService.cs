using LibraryAPI.Dtos;
using LibraryAPI.Models;

namespace LibraryAPI.Services.Abstract
{
    public interface IBooksService:IBase<BookDto,UpdateBookDto>
    {
         new Task<IEnumerable<Book>> GetAll();
         Task<IEnumerable<Book>> GetBooksByGenre(int id);
         Task<IEnumerable<object>> GetTop5Genres();
         Task<IEnumerable<object>> GetTop5Authors();
    }
}
