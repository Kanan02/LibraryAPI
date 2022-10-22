using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {
           _booksService = booksService;
        }
        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return Ok(await _booksService.GetAll());
        }
        
        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _booksService.Get(id);
            if (book == null)
            {
                return NotFound($"Book with id {id} was not found");
            }
            return Ok(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult> Post(BookDto book)
        {
            if (book == null)
            {
                return BadRequest("Please enter values of book");
            }
          
            await _booksService.Add(book);
            return Ok();
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> Put(int id, [FromBody] UpdateBookDto bookDto)
        {
            try
            {
                var itemToUpdate = await _booksService.Get(id);
                if (itemToUpdate == null)
                    return NotFound($"Book with id {id} was not found");

                return await _booksService.Update(id,bookDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var itemToDelete = await _booksService.Get(id);
            if (itemToDelete == null)
            {
                return NotFound($"Book with id {id} was not found");
            }
            await _booksService.Delete(itemToDelete);
            return Ok(id);
        }
        [HttpGet("getBooksByGenre/{id}")]
        public async Task<ActionResult> GetByGenre(int id)
        {

            return Ok(await _booksService.GetBooksByGenre(id));
        }
        [HttpGet("getTop5Genres")]
        public async Task<ActionResult> GetTopGenres()
        {
            return Ok(await _booksService.GetTop5Genres());
        }
        [HttpGet("getTop5Authors")]
        public async Task<ActionResult> GetTopAuthors()
        {
            return Ok(await _booksService.GetTop5Authors());
        }
    }
}
