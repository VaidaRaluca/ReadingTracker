using Microsoft.AspNetCore.Mvc;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Interfaces;
using System.Globalization;

namespace ReadingTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll(
            [FromQuery] string? sortBy, [FromQuery] string? order, [FromQuery] string? genre = null)
        {
            var books = await this.bookService.GetAllAsync();

            if (!string.IsNullOrEmpty(genre))
            {
                books = books.Where(b => b.Genre != null && b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                books = (sortBy.ToLower(), order.ToLower()) switch
                {
                    ("name", "asc") => books.OrderBy(b => b.Name, StringComparer.OrdinalIgnoreCase),
                    ("name", "desc") => books.OrderByDescending(b => b.Name, StringComparer.OrdinalIgnoreCase),
                    ("author", "asc") => books.OrderBy(b => b.Author, StringComparer.OrdinalIgnoreCase),
                    ("author", "desc") => books.OrderByDescending(b => b.Author, StringComparer.OrdinalIgnoreCase),
                    _ => books
                };
            }

            return Ok(books);
        }


        // GET: api/books/{id}
        [HttpGet("id/{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await this.bookService.GetByIdAsync(id);
            return Ok(book);
        }

        // GET: api/books/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<BookDto>> GetByName(string name)
        {
            var book = await this.bookService.GetByNameAsync(name);
            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<BookDto>> Create([FromBody] BookDto dto)
        {
            var created = await this.bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDto dto)
        {
            var updated = await this.bookService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await this.bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
