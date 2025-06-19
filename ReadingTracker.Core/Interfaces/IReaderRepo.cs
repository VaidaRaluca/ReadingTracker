using ReadingTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Interfaces
{
    public interface IReaderRepo
    {
        Task<IEnumerable<Reader>> GetAllAsync();
        Task<Reader> GetByIdAsync(int id);
        Task<Reader> GetByEmailAsync(string email);
        Task AddAsync(Reader reader);
        Task UpdateAsync(Reader reader);
        Task DeleteAsync(int id);
    }

}
