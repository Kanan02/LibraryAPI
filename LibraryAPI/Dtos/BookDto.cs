using LibraryAPI.Models;

namespace LibraryAPI.Dtos
{
    public class BookDto
    {
        public BookDto()
        {
            AuthorDtos = new List<AuthorDto>();
            GenreDtos=new List<GenreDto>();
        }
        public int BookId { get; set; }
        public string? Title { get; set; }
        public ICollection<AuthorDto>? AuthorDtos { get; set; }
        public ICollection<GenreDto> ?GenreDtos { get; set; }
    }
}
