using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Services.Abstract;

namespace LibraryAPI.Services.Concrete
{
    public class BookAuthorsService : IBookAuthorsService
    {
        private readonly ILibraryContext _libraryContext;
        public BookAuthorsService(ILibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task Add(BookAuthor obj)
        {
            _libraryContext.BookAuthors.Add(obj);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task Delete(BookAuthor obj)
        {
            _libraryContext.BookAuthors.Remove(obj);
            await _libraryContext.SaveChangesAsync();
        }

        public Task<BookAuthor?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookAuthor> Update(int id, int source)
        {
            throw new NotImplementedException();
        }
    }
}
