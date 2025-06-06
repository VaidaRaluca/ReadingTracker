using AutoMapper;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<BookDto, Book>()
          .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Reader, ReaderDto>();
        CreateMap<ReaderDto, Reader>();
    }
}
