using AutoMapper;
using BookShopService.Domain.Models;
using BookShopService.API.DTOs;

namespace BookShopService.API.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, UniqueBook>();
            CreateMap<UniqueBook, Book>();
            CreateMap<CommonBook, Book>();
            CreateMap<Book, CommonBook>();
        }
    }
}