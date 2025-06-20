using AutoMapper;
using ReadingTracker.Core.Dtos;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Exceptions;
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
            if (reader == null)
                throw new ResourceMissingException("Reader not found");
            return mapper.Map <ReaderDto>(reader);
        }

        public async Task<ReaderDto?> GetByEmailAsync(string email)
        {
            var reader = await readerRepo.GetByEmailAsync(email);
            if (reader == null)
                throw new ResourceMissingException("Reader not found");
            return mapper.Map<ReaderDto>(reader);
        }

        //public async Task<ReaderDto> CreateAsync(ReaderDto dto)
        //{
        //    var entity = mapper.Map<Reader>(dto);
        //    entity.Id = 0; // generate new key
        //    if (string.IsNullOrEmpty(dto.Password))
        //        throw new InvalidOperationException("Password is required.");

        //    var salt = authService.GenerateSalt();
        //    var hashedPasswordBase64 = authService.HashPassword(dto.Password, salt);
        //    var hashedPasswordBytes = Convert.FromBase64String(hashedPasswordBase64);

        //    entity.PasswordSalt = salt;
        //    entity.PasswordHash = hashedPasswordBytes;

        //    await readerRepo.AddAsync(entity);
        //    return mapper.Map<ReaderDto>(entity);
        //}

        public async Task<bool> UpdateAsync(int id, ReaderDto dto)
        {
            var reader = await readerRepo.GetByIdAsync(id);
            if (reader == null)
                throw new ResourceMissingException("Reader not found");

            mapper.Map(dto, reader);

            await readerRepo.UpdateAsync(reader);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reader = await readerRepo.GetByIdAsync(id);
            if (reader == null) return false;

            await readerRepo.DeleteAsync(reader.Id);
            return true;
        }


        public async Task<AuthDto> RegisterAsync(RegisterDto dto)
        {
            var reader = await readerRepo.GetByEmailAsync(dto.Email);
            if (reader != null)
                throw new ConflictException("A reader with this email is already registered.");

            var salt = authService.GenerateSalt();
            var hashedPasswordBase64 = authService.HashPassword(dto.Password, salt);
            var hashedPasswordBytes = Convert.FromBase64String(hashedPasswordBase64);

            var newreader = new Reader
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = hashedPasswordBytes,
                PasswordSalt = salt,
                IsAdmin = dto.IsAdmin
            };


            await readerRepo.AddAsync(newreader);
            var role = newreader.IsAdmin ? "Admin" : "Reader";
            var token = authService.GetToken(newreader,role);

            return new AuthDto
            {
                Token = token,
                Email = newreader.Email,
                Name = newreader.Name
            };
        }


        public async Task<AuthDto> LoginAsync(LoginDto dto)
        {
            var reader = await readerRepo.GetByEmailAsync(dto.Email);
            if (reader == null)
                throw new ResourceMissingException("Reader not found");

            var hashedBase64 = authService.HashPassword(dto.Password, reader.PasswordSalt);
            var hashedBytes = Convert.FromBase64String(hashedBase64);

            if (!hashedBytes.SequenceEqual(reader.PasswordHash))
                throw new UnauthorizedException("Invalid credentials");

            var role = GetRole(reader);
            var token = authService.GetToken(reader, role);
            return new AuthDto
            {
                Token = token,
                Email = reader.Email,
                Name = reader.Name
            };
        }


        private string GetRole(Reader reader)
        {
            if (reader.IsAdmin)
            {
                return "Admin";
            }
            else
            {
                return "Reader";
            }
        }
    }


}
