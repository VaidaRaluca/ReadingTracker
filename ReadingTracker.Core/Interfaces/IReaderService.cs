using ReadingTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Interfaces
{

    public interface IReaderService
    {
        Task<IEnumerable<Reader>> GetAllAsync();
        Task<Reader?> GetByIdAsync(int id);
        Task<Reader> CreateAsync(Reader reader);
        Task<bool> UpdateAsync(Reader reader);
        Task<bool> DeleteAsync(int id);
    }

}
