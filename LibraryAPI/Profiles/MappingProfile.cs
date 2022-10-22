using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Models;

namespace LibraryAPI.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<Genre, GenreDto>();

            CreateMap<GenreDto, BookGenre>()
                .ForMember(d=>d.GenreId,opt=>opt.MapFrom(s=>s.Id))
                .ReverseMap()
                .ForMember(d=>d.Name,opt=>opt.MapFrom(s=>s.Genre.Name));
            CreateMap<AuthorDto, BookAuthor>()
               .ForMember(d => d.BookId, opt => opt.MapFrom(s => s.Id))
               .ReverseMap()
                 .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Author.Name));
            CreateMap<Book, BookDto>()
    .ForMember(d=>d.BookId,opt=>opt.MapFrom(s=>s.Id))
     .ForMember(d => d.GenreDtos, opt => opt.MapFrom(s => s.BookGenres
        .Select(c => new BookGenre { BookId = s.Id, GenreId = c.GenreId })))
     .ForMember(d => d.AuthorDtos, opt => opt.MapFrom(s => s.BookAuthors
        .Select(c => new BookAuthor { BookId = s.Id, AuthorId = c.AuthorId })))
    .ForMember(d=>d.AuthorDtos,opt=>opt.MapFrom(s=>s.BookAuthors))
    .ForMember(d=>d.GenreDtos,opt=>opt.MapFrom(s=>s.BookGenres))
    .ReverseMap()
    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.BookId))
    .ForMember(d => d.BookGenres, opt => opt.MapFrom(s => s.GenreDtos
        .Select(c => new BookGenre { BookId = s.BookId, GenreId = c.Id })))
    .ForMember(d => d.BookAuthors, opt => opt.MapFrom(s => s.AuthorDtos
        .Select(c => new BookAuthor { BookId = s.BookId, AuthorId = c.Id })))
    .ForMember(d => d.BookAuthors, opt => opt.MapFrom(s => s.AuthorDtos))
    .ForMember(d => d.BookGenres, opt => opt.MapFrom(s => s.GenreDtos));

        }
    }
}
