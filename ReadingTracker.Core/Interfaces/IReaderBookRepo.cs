using ReadingTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Interfaces
{
    public interface IReaderBookRepo
    {
        Task<IEnumerable<ReaderBook>> GetAllAsync();
        Task<ReaderBook?> GetAsync(int readerId, int bookId);
        Task AddAsync(ReaderBook readerBook);
        Task UpdateAsync(ReaderBook readerBook);
        Task DeleteAsync(int readerId, int bookId);

        Task<IEnumerable<ReaderBook>> GetByReaderIdAsync(int readerId);
        Task<IEnumerable<ReaderBook>> GetByBookIdAsync(int bookId);
    }


}
