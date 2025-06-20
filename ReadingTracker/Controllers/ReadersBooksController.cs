using Microsoft.AspNetCore.Mvc;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Interfaces;
using System.Globalization;

namespace ReadingTracker.Api.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class ReaderBooksController : ControllerBase
        {
            private readonly IReaderBookService readerBookService;

            public ReaderBooksController(IReaderBookService readerBookService)
            {
                this.readerBookService = readerBookService;
            }

            // GET: api/readerbooks
            [HttpGet]
            public async Task<ActionResult<IEnumerable<ReaderBookDto>>> GetAll(
                 [FromQuery] string? sortBy = null,[FromQuery] string? order = "asc", [FromQuery] int? rating = null, [FromQuery] string? startMonth = null, [FromQuery] string? endMonth = null)
            {
                var items = await this.readerBookService.GetAllAsync();

            if (rating.HasValue)
            {
                items = items.Where(i => Math.Floor(i.Rating) == rating.Value);
            }

            if (!string.IsNullOrEmpty(startMonth)
                 && DateTime.TryParseExact(startMonth, "MMMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            {
                var monthNum = dt.Month; 
                items = items.Where(i => i.StartedAt.Month == monthNum);
            }

            if (!string.IsNullOrEmpty(endMonth)
                  && DateTime.TryParseExact(endMonth, "MMMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var et))
            {
                var monthNum = et.Month; 
                items = items.Where(i => i.EndedAt.Month == monthNum);
            }


            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("rating", StringComparison.OrdinalIgnoreCase))
                {
                    items = order.ToLower() switch
                    {
                        "desc" => items.OrderByDescending(i => i.Rating),
                        _ => items.OrderBy(i => i.Rating),
                    };
                }
            }
            return Ok(items);
            }

            // GET: api/readerbooks/{readerId}/{bookId}
            [HttpGet("{readerId:int}/{bookId:int}")]
            public async Task<ActionResult<ReaderBookDto>> GetByIds(int readerId, int bookId)
            {
                var item = await this.readerBookService.GetByIdsAsync(readerId, bookId);

                return Ok(item);
            }

            // POST: api/readerbooks
            [HttpPost]
            public async Task<ActionResult<ReaderBookDto>> Create([FromBody] ReaderBookDto dto)
            {
                var created = await this.readerBookService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByIds), new { readerId = created.ReaderId, bookId = created.BookId }, created);
            }

            // PUT: api/readerbooks/{readerId}/{bookId}
            [HttpPut("{readerId:int}/{bookId:int}")]
            public async Task<IActionResult> Update(int readerId, int bookId, [FromBody] ReaderBookDto dto)
            {
                var updated = await this.readerBookService.UpdateAsync(readerId, bookId, dto);

                return NoContent();
            }

            // DELETE: api/readerbooks/{readerId}/{bookId}
            [HttpDelete("{readerId:int}/{bookId:int}")]
            public async Task<IActionResult> Delete(int readerId, int bookId)
            {
                var deleted = await this.readerBookService.DeleteAsync(readerId, bookId);

                return NoContent();
            }
        }

}
