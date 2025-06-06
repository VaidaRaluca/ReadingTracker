using AutoMapper;
using ReadingTracker.Core.Entities;
using ReadingTracker.Core.Dtos;
using Microsoft.EntityFrameworkCore.Design;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<BookDto, Book>()
          .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Reader, ReaderDto>();
        CreateMap<ReaderDto, Reader>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ReaderBook, ReaderBookDto>()
    .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Name))
    .ForMember(dest => dest.ReaderName, opt => opt.MapFrom(src => src.Reader.Name));

        CreateMap<ReaderBookDto, ReaderBook>()
            .ForMember(dest => dest.Book, opt => opt.Ignore())     // handled by foreign key
            .ForMember(dest => dest.Reader, opt => opt.Ignore());  // handled by foreign key

    }
}
