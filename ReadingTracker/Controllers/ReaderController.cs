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
        public async Task<ActionResult<IEnumerable<ReaderDto>>> GetAll()
        {
            var readers = await this.readerService.GetAllAsync();
            return Ok(readers);
        }

        // GET: api/readers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReaderDto>> GetById(int id)
        {
            var reader = await this.readerService.GetByIdAsync(id);
            return Ok(reader);
        }

        // POST: api/readers
        [HttpPost]
        public async Task<ActionResult<ReaderDto>> Create([FromBody] ReaderDto dto)
        {
            var created = await this.readerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
    }
}
