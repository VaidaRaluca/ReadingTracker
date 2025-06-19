using Microsoft.EntityFrameworkCore;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Interfaces;
using ReadingTracker.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Database.Repos
{
    public class BookRepo : IBookRepo
    {
        private readonly ReadingTrackerDbContext context;

        public BookRepo(ReadingTrackerDbContext context) => this.context = context;

        public async Task<IEnumerable<Book>> GetAllAsync() => await this.context.Books.Include(b => b.ReaderBooks).ToListAsync();
        public async Task<Book> GetByIdAsync(int id)
        {
            var result = await this.context.Books.Include(b => b.ReaderBooks).FirstOrDefaultAsync(b => b.Id == id);
            if (result == null)
                throw new ResourceMissingException("Book not found");
            return result;
        }
        public async Task AddAsync(Book book) { this.context.Books.Add(book); await this.context.SaveChangesAsync(); }
        public async Task UpdateAsync(Book book) { this.context.Books.Update(book); await this.context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var book = await this.context.Books.FindAsync(id);
            if (book != null)
            {
                this.context.Books.Remove(book);
                await this.context.SaveChangesAsync();
            }
        }
    }

}
