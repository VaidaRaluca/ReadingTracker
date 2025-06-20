using Microsoft.AspNetCore.Mvc;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Interfaces;

namespace ReadingTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService readerService;

        public ReadersController(IReaderService readerService)
        {
            this.readerService = readerService;
        }

        // GET: api/readers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReaderDto>>> GetAll(
             [FromQuery] string? sortBy, [FromQuery] string? order, [FromQuery] int? age, [FromQuery] bool? isadmin)
        {
            var readers = await this.readerService.GetAllAsync();

            if (age!=null)
            {
                readers = readers.Where(b => b.Age != null && b.Age==age);
            }

            if (isadmin != null)
            {
                readers = readers.Where(b => b.IsAdmin != null && b.IsAdmin == isadmin);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                readers = (sortBy.ToLower(), order.ToLower()) switch
                {
                    ("name", "asc") => readers.OrderBy(r => r.Name, StringComparer.OrdinalIgnoreCase),
                    ("name", "desc") => readers.OrderByDescending(r => r.Name, StringComparer.OrdinalIgnoreCase),
                    ("age", "asc") => readers.OrderBy(r => r.Age),
                    ("age", "desc") => readers.OrderByDescending(r => r.Age),
                    _ => readers
                };
            }

            return Ok(readers);
        }

        // GET: api/readers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReaderDto>> GetById(int id)
        {
            var reader = await this.readerService.GetByIdAsync(id);
            return Ok(reader);
        }

        // PUT: api/readers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReaderDto dto)
        {
            var updated = await this.readerService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/readers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await this.readerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthDto>> Register(RegisterDto dto)
        {
            var result = await readerService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthDto>> Login(LoginDto dto)
        {
            var result = await readerService.LoginAsync(dto);
            return Ok(result);
        }

    }
}
