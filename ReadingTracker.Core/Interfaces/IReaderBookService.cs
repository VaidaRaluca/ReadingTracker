using ReadingTracker.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Interfaces
{
    public interface IReaderBookService
    {
        Task<IEnumerable<ReaderBookDto>> GetAllAsync();
        Task<ReaderBookDto?> GetByIdsAsync(int readerId, int bookId);
        Task<bool> DeleteAsync(int readerId, int bookId);

        Task<ReaderBookDto> CreateAsync(ReaderBookDto dto);
        Task<bool> UpdateAsync(int readerId, int bookId, ReaderBookDto dto);
    }

}
