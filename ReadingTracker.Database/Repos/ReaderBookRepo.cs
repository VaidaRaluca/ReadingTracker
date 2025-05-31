using Microsoft.EntityFrameworkCore;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Database.Repos
{
    public class ReaderBookRepo : IReaderBookRepo
    {
        private readonly ReadingTrackerDbContext context;

        public ReaderBookRepo(ReadingTrackerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ReaderBook>> GetAllAsync()
        {
            return await this.context.ReaderBooks
                .Include(rb => rb.Book)
                .Include(rb => rb.Reader)
                .ToListAsync();
        }

        public async Task<ReaderBook?> GetAsync(int readerId, int bookId)
        {
            return await this.context.ReaderBooks
                .Include(rb => rb.Book)
                .Include(rb => rb.Reader)
                .FirstOrDefaultAsync(rb => rb.ReaderId == readerId && rb.BookId == bookId);
        }

        public async Task AddAsync(ReaderBook readerBook)
        {
            this.context.ReaderBooks.Add(readerBook);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReaderBook readerBook)
        {
            this.context.ReaderBooks.Update(readerBook);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int readerId, int bookId)
        {
            var rb = await GetAsync(readerId, bookId);
   
                this.context.ReaderBooks.Remove(rb);
                await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReaderBook>> GetByReaderIdAsync(int readerId)
        {
            return await this.context.ReaderBooks
                .Include(rb => rb.Book)
                .Where(rb => rb.ReaderId == readerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ReaderBook>> GetByBookIdAsync(int bookId)
        {
            return await this.context.ReaderBooks
                .Include(rb => rb.Reader)
                .Where(rb => rb.BookId == bookId)
                .ToListAsync();
        }
    }
}