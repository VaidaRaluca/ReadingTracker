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
        public class ReaderRepo : IReaderRepo
        {
            private readonly ReadingTrackerDbContext context;

            public ReaderRepo(ReadingTrackerDbContext context) => this.context = context;

            public async Task<IEnumerable<Reader>> GetAllAsync() => await this.context.Readers.Include(r => r.ReaderBooks).ToListAsync();
            public async Task<Reader> GetByIdAsync(int id) => await this.context.Readers.Include(r => r.ReaderBooks).FirstOrDefaultAsync(r => r.Id == id);
            public async Task AddAsync(Reader reader) { this.context.Readers.Add(reader); await this.context.SaveChangesAsync(); }
            public async Task UpdateAsync(Reader reader) { this.context.Readers.Update(reader); await this.context.SaveChangesAsync(); }
            public async Task DeleteAsync(int id)
            {
                var reader = await this.context.Readers.FindAsync(id);
                if (reader != null)
                {
                    this.context.Readers.Remove(reader);
                    await this.context.SaveChangesAsync();
                }
            }
        }


}
