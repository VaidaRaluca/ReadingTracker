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


    public class ReaderService : IReaderService
    {
        private readonly IReaderRepo readerRepo;

        public ReaderService(IReaderRepo readerRepo)
        {
            this.readerRepo = readerRepo;
        }

        public async Task<IEnumerable<ReaderDto>> GetAllAsync()
        {
            var readers = await readerRepo.GetAllAsync();
            return readers.Select(r => new ReaderDto
            {
                Name = r.Name,
                Age = r.Age
            });
        }

        public async Task<ReaderDto?> GetByIdAsync(int id)
        {
            var reader = await readerRepo.GetByIdAsync(id);
            if (reader == null) return null;

            return new ReaderDto
            {
                Name = reader.Name,
                Age = reader.Age
            };
        }

        public async Task<ReaderDto> CreateAsync(ReaderDto dto)
        {
            var reader = new Reader
            {
                Name = dto.Name,
                Age = dto.Age
            };

            await readerRepo.AddAsync(reader);
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ReaderDto dto)
        {
            var existing = await readerRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Age = dto.Age;

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
