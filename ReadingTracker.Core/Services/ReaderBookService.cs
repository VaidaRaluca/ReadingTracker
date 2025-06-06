using AutoMapper;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Interfaces;

namespace ReadingTracker.Core.Services
{
    public class ReaderBookService : IReaderBookService
    {
        private readonly IReaderBookRepo readerBookRepo;
        private readonly IMapper mapper;

        public ReaderBookService(IReaderBookRepo readerBookRepo, IMapper mapper)
        {
            this.readerBookRepo = readerBookRepo;
            this.mapper = mapper;
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

        public async Task<ReaderBookDto> CreateAsync(ReaderBookDto dto)
        {
            var entity = mapper.Map<ReaderBook>(dto);
            await readerBookRepo.AddAsync(entity);
            return mapper.Map<ReaderBookDto>(entity);
        }

        public async Task<bool> UpdateAsync(int readerId, int bookId, ReaderBookDto dto)
        {
            var existing = await readerBookRepo.GetAsync(readerId, bookId);
            if (existing == null) return false;

            mapper.Map(dto, existing);
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
