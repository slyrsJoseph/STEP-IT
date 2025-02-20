using AutoMapper;
using WebApiHomeTask1.Data.Models;
using WebApiHomeTask1.DTO.Requests.Book;

namespace WebApiHomeTask1.Data.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookRequest, Book>()
            .ForMember(dest => dest.BookName, 
                opt => opt.MapFrom(src => src.BookName));







    }





}