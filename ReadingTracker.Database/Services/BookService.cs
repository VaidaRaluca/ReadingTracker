using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Interfaces;
using ReadingTracker.Database.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReadingTracker.Database.Services
{

    public class BookService : IBookService
    {
        private readonly IBookRepo bookRepo;
        private readonly IMapper mapper;
        public BookService(IBookRepo bookRepo, IMapper mapper)
        {
            this.bookRepo = bookRepo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await bookRepo.GetAllAsync();
            return mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await bookRepo.GetByIdAsync(id);
            if (book == null) return null;

            return mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(BookDto dto)
        {
            var entity = mapper.Map<Book>(dto);
            await bookRepo.AddAsync(entity);
            return mapper.Map<BookDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, BookDto dto)
        {
            var existing = await bookRepo.GetByIdAsync(id);
            if (existing == null) return false;

            mapper.Map(dto, existing);

            await bookRepo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await bookRepo.GetByIdAsync(id);
            if (existing == null) return false;

            await bookRepo.DeleteAsync(existing.Id);
            return true;
        }
    }


}
