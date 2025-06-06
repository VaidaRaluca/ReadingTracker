using AutoMapper;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Interfaces;
namespace ReadingTracker.Core.Services
{


    public class ReaderService : IReaderService
    {
        private readonly IReaderRepo readerRepo;
        private readonly IMapper mapper;

        public ReaderService(IReaderRepo readerRepo, IMapper mapper)
        {
            this.readerRepo = readerRepo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReaderDto>> GetAllAsync()
        {
            var readers = await readerRepo.GetAllAsync();
            return mapper.Map<IEnumerable<ReaderDto>>(readers);
        }

        public async Task<ReaderDto?> GetByIdAsync(int id)
        {
            var reader = await readerRepo.GetByIdAsync(id);
            if (reader == null) return null;
            return mapper.Map <ReaderDto>(reader);
        }

        public async Task<ReaderDto> CreateAsync(ReaderDto dto)
        {
            var entity = mapper.Map<Reader>(dto);
            entity.Id = 0; // idk
            await readerRepo.AddAsync(entity);
            return mapper.Map<ReaderDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, ReaderDto dto)
        {
            var existing = await readerRepo.GetByIdAsync(id);
            if (existing == null) return false;

            mapper.Map(dto, existing);

            await readerRepo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await readerRepo.GetByIdAsync(id);
            if (existing == null) return false;

            await readerRepo.DeleteAsync(existing.Id);
            return true;
        }
    }


}
