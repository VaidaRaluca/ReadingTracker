using AutoMapper;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Interfaces;
using System.Reflection.PortableExecutable;
namespace ReadingTracker.Core.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepo readerRepo;
        private readonly IMapper mapper;

        public AuthService authService { get; set; }

        public ReaderService(IReaderRepo readerRepo, IMapper mapper, AuthService authService)
        {
            this.readerRepo = readerRepo;
            this.mapper = mapper;
            this.authService = authService;
        }

        public async Task<IEnumerable<ReaderDto>> GetAllAsync()
        {
            var readers = await readerRepo.GetAllAsync();
            return mapper.Map<IEnumerable<ReaderDto>>(readers);
        }

        public async Task<ReaderDto?> GetByIdAsync(int id)
        {
            var reader = await readerRepo.GetByIdAsync(id);
            if (reader == null) return null;
            return mapper.Map <ReaderDto>(reader);
        }

        public async Task<ReaderDto?> GetByEmailAsync(string email)
        {
            var reader = await readerRepo.GetByEmailAsync(email);
            if (reader == null) return null;
            return mapper.Map<ReaderDto>(reader);
        }

        public async Task<ReaderDto> CreateAsync(ReaderDto dto)
        {
            var entity = mapper.Map<Reader>(dto);
            entity.Id = 0; // idk
            await readerRepo.AddAsync(entity);
            return mapper.Map<ReaderDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, ReaderDto dto)
        {
            var existing = await readerRepo.GetByIdAsync(id);
            if (existing == null) return false;

            mapper.Map(dto, existing);

            await readerRepo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await readerRepo.GetByIdAsync(id);
            if (existing == null) return false;

            await readerRepo.DeleteAsync(existing.Id);
            return true;
        }


        public async Task<AuthDto> RegisterAsync(RegisterDto dto)
        {
            var existing = await readerRepo.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new Exception("Email already registered");

            var salt = authService.GenerateSalt();
            var hashed = authService.HashPassword(dto.Password, salt);
            var hashedPasswordBytes = Convert.FromBase64String(hashed);

            var reader = new Reader
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = hashedPasswordBytes,
                PasswordSalt = salt
            };

            await readerRepo.AddAsync(reader);
            var token = authService.GetToken(reader, "reader");

            return new AuthDto
            {
                Token = token,
                Email = reader.Email,
                Name = reader.Name
            };
        }

        public async Task<AuthDto> LoginAsync(LoginDto dto)
        {
            var reader = await readerRepo.GetByEmailAsync(dto.Email);
            if (reader == null)
                throw new Exception("Reader not found");

            var hashed = authService.HashPassword(dto.Password, reader.PasswordSalt);

            var storedHashString = Convert.ToBase64String(reader.PasswordHash);

            if (hashed != storedHashString)
                throw new Exception("Invalid credentials");

            var token = authService.GetToken(reader, "reader");
            return new AuthDto
            {
                Token = token,
                Email = reader.Email,
                Name = reader.Name
            };
        }

    }


}
