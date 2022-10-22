using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Services.Abstract;

namespace LibraryAPI.Services.Concrete
{
    public class BookGenresService : IBookGenresService
    {
        private readonly ILibraryContext _libraryContext;
        public BookGenresService(ILibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task Add(BookGenre obj)
        {

            _libraryContext.BookGenres.Add(obj);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task Delete(BookGenre obj)
        {
           _libraryContext.BookGenres.Remove(obj);
            await _libraryContext.SaveChangesAsync();
        }

        public Task<BookGenre?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookGenre> Update(int id, int source)
        {
            throw new NotImplementedException();
        }
    }
}
