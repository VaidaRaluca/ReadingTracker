using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReadingTracker.Database.Services
{


    public class ReaderService : IReaderService
    {
        private readonly IReaderRepo readerRepo;

        public ReaderService(IReaderRepo readerRepo)
        {
            this.readerRepo = readerRepo;
        }

        public async Task<IEnumerable<Reader>> GetAllAsync()
        {
            return await this.readerRepo.GetAllAsync();
        }

        public async Task<Reader?> GetByIdAsync(int id)
        {
            return await this.readerRepo.GetByIdAsync(id);
        }

        public async Task<Reader> CreateAsync(Reader reader)
        {
            await this.readerRepo.AddAsync(reader);
            return reader;
        }

        public async Task<bool> UpdateAsync(Reader reader)
        {
            var existing = await this.readerRepo.GetByIdAsync(reader.Id);
            if (existing == null) return false;

            existing.Name = reader.Name;
            existing.Age = reader.Age;

            await this.readerRepo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await this.readerRepo.GetByIdAsync(id);
            if (existing == null) return false;

            await this.readerRepo.DeleteAsync(existing.Id);
            return true;
        }
    }

}
