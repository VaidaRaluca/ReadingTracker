using ReadingTracker.Core.Dtos;
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
        Task<IEnumerable<ReaderDto>> GetAllAsync();
        Task<ReaderDto?> GetByIdAsync(int id);
        Task<ReaderDto?> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(int id, ReaderDto readerDto);
        Task<bool> DeleteAsync(int id);
        Task<AuthDto> RegisterAsync(RegisterDto dto);
        Task<AuthDto> LoginAsync(LoginDto dto);


    }


}
