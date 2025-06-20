using AutoMapper;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Exceptions;
using ReadingTracker.Core.Interfaces;
using System.Reflection.PortableExecutable;
namespace ReadingTracker.Core.Services
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
            if (book == null)
                throw new ResourceMissingException("Book not found");

            return mapper.Map<BookDto>(book);
        }


        public async Task<BookDto?> GetByNameAsync(string name)
        {
            var book = await bookRepo.GetByNameAsync(name);
            if (book == null)
                throw new ResourceMissingException("Book not found");

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
            var book = await bookRepo.GetByIdAsync(id);
            if (book == null)
                throw new ResourceMissingException("Book not found");

            mapper.Map(dto, book);

            await bookRepo.UpdateAsync(book);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await bookRepo.GetByIdAsync(id);
            if (book == null)
                throw new ResourceMissingException("Book not found");

            await bookRepo.DeleteAsync(book.Id);
            return true;
        }
    }


}
