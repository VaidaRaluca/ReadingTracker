using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Services
{
    public class ReaderBookService : IReaderBookService
    {
        private readonly IReaderBookRepo readerBookRepo;

        public ReaderBookService(IReaderBookRepo readerBookRepo)
        {
            this.readerBookRepo = readerBookRepo;
        }

        public async Task<IEnumerable<ReaderBookDto>> GetAllAsync()
        {
            var items = await readerBookRepo.GetAllAsync();
            return items.Select(rb => new ReaderBookDto
            {
                BookId = rb.BookId,
                BookName = rb.Book?.Name ?? "",
                ReaderId = rb.ReaderId,
                ReaderName = rb.Reader?.Name ?? "",
                Rating = rb.Rating,
                StartedAt = rb.StartedAt,
                EndedAt = rb.EndedAt
            });
        }

        public async Task<ReaderBookDto?> GetByIdsAsync(int readerId, int bookId)
        {
            var rb = await readerBookRepo.GetAsync(readerId, bookId);
            if (rb == null) return null;

            return new ReaderBookDto
            {
                BookId = rb.BookId,
                BookName = rb.Book?.Name ?? "",
                ReaderId = rb.ReaderId,
                ReaderName = rb.Reader?.Name ?? "",
                Rating = rb.Rating,
                StartedAt = rb.StartedAt,
                EndedAt = rb.EndedAt
            };
        }

        public async Task<ReaderBookDto> CreateAsync(ReaderBookCreateDto dto)
        {
            var entity = new ReaderBook
            {
                BookId = dto.BookId,
                ReaderId = dto.ReaderId,
                Rating = dto.Rating,
                StartedAt = dto.StartedAt,
                EndedAt = dto.EndedAt
            };

            await readerBookRepo.AddAsync(entity);

            // Fetch related names for response
            var created = await readerBookRepo.GetAsync(dto.ReaderId, dto.BookId);

            return new ReaderBookDto
            {
                BookId = created.BookId,
                BookName = created.Book?.Name ?? "",
                ReaderId = created.ReaderId,
                ReaderName = created.Reader?.Name ?? "",
                Rating = created.Rating,
                StartedAt = created.StartedAt,
                EndedAt = created.EndedAt
            };
        }

        public async Task<bool> UpdateAsync(int readerId, int bookId, ReaderBookCreateDto dto)
        {
            var existing = await readerBookRepo.GetAsync(readerId, bookId);
            if (existing == null) return false;

            existing.Rating = dto.Rating;
            existing.StartedAt = dto.StartedAt;
            existing.EndedAt = dto.EndedAt;

            await readerBookRepo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int readerId, int bookId)
        {
            var existing = await readerBookRepo.GetAsync(readerId, bookId);
            if (existing == null) return false;

            await readerBookRepo.DeleteAsync(readerId, bookId);
            return true;
        }
    }

}
