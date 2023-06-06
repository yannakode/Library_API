﻿using AutoMapper;
using Library.Models;
using Library.Models.DTO;

namespace Library
{
    public class MappingConfig: Profile
    {
        public MappingConfig() 
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<BookDTO, Book>().ReverseMap();
        }
    }
}
