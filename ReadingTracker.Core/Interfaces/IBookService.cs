using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto?> GetByNameAsync(string name);
        Task<BookDto> CreateAsync(BookDto dto);
        Task<bool> UpdateAsync(int id, BookDto dto);
        Task<bool> DeleteAsync(int id);
    }


}
