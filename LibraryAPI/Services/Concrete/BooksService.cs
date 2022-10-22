using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services.Concrete
{
    public class BooksService : IBooksService
    {
        private readonly ILibraryContext _libraryContext;
        private readonly IBookAuthorsService _bookAuthorsService;
        private readonly IBookGenresService _bookGenresService;

        private readonly IMapper _mapper;
        public BooksService(ILibraryContext libraryContext,IBookGenresService bookGenresService,
            IBookAuthorsService bookAuthorsService,IMapper mapper)
        {
            _bookGenresService = bookGenresService;
            _bookAuthorsService = bookAuthorsService;
            _libraryContext = libraryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            
            return _libraryContext.Books.ToList();

        }
        public async Task<BookDto?>  Get(int id)
        {
            return  _mapper.Map<BookDto>(await _libraryContext.Books.FindAsync(id));
        }

        public async Task Add(BookDto obj)
        {
            Book book = new Book() { Title=obj.Title};

            _libraryContext.Books.Add(book);
            await _libraryContext.SaveChangesAsync();

            foreach (var genreDto in obj.GenreDtos)
            {
                var _mappedGenre= _mapper.Map<Genre>(genreDto);
                _libraryContext.Genres.Add(_mappedGenre);
                await _libraryContext.SaveChangesAsync();
                await _bookGenresService.Add(new BookGenre() { BookId = book.Id, GenreId = _mappedGenre.Id });
             
            }

            foreach (var authorDto in obj.AuthorDtos)
            {
                var _mappedAuthor = _mapper.Map<Author>(authorDto);
                _libraryContext.Authors.Add(_mappedAuthor);
                await _libraryContext.SaveChangesAsync();
                await _bookAuthorsService.Add(new BookAuthor() { BookId = book.Id, AuthorId = _mappedAuthor.Id });
            }
            
            await _libraryContext.SaveChangesAsync();
        }

        public async Task Delete(BookDto obj)
        {
            IList<BookAuthor> ba = _libraryContext.BookAuthors.ToList();
            IList<BookGenre> bg = _libraryContext.BookGenres.ToList();
            foreach (BookAuthor item in ba)
            {
                if (item.BookId==obj.BookId)
                {
                    await _bookAuthorsService.Delete(item);
                }
            }
            foreach (BookGenre item in bg)
            {
                if (item.BookId == obj.BookId)
                {
                   await _bookGenresService.Delete(item);

                }
            }
            var book =await _libraryContext.Books.FindAsync(obj.BookId);
            _libraryContext.Books.Remove(book);
            await _libraryContext.SaveChangesAsync();
        }


       

        public async Task<BookDto> Update(int id, UpdateBookDto source)
        {
            if (source != null)
            {
                Book b= await _libraryContext.Books.FindAsync(id);
                b.Title = source.Title;
                await _libraryContext.SaveChangesAsync();
                return _mapper.Map<BookDto>(b);
            }

            throw new NullReferenceException();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenre(int id)
        {
            //also possible:
            //var query = _libraryContext.Books.Select(book =>
            //book.BookGenres.Where(bg=>bg.GenreId == id )).ToList();
            //var books = _libraryContext.Books
            //             .Include(s => s.BookGenres)
            //             ;
                         
            //;
            var books = new List<Book>();
            foreach (var item in _libraryContext.BookGenres)
            {
                if (item.GenreId==id)
                {
                    var book = await _libraryContext.Books.FindAsync(item.BookId);
                    books.Add(book);
                }
            }

            return books;
        }
        public async Task<IEnumerable<object>> GetTop5Genres()
        {
            var genres = _libraryContext.Genres
            .Select(t => new
            {
                GenreName = t.Name,                     
                Count = t.BookGenres.Count()   
            })
            .OrderByDescending(x => x.Count).Take(5); 
            return genres;


        }

        public async Task<IEnumerable<object>> GetTop5Authors()
        {
            var authors = _libraryContext.Authors
           .Select(t => new
           {
               Author = t.Name,
               Count = t.BookAuthors.Count()
           })
           .OrderByDescending(x => x.Count).Take(5);
            return authors;

        }
    }
}
