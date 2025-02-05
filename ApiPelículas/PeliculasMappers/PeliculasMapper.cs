using ApiPelículas.Models;
using ApiPelículas.Models.Dtos;
using AutoMapper;

namespace ApiPelículas.PeliculasMappers
{
    public class PeliculasMapper : Profile
    {
        public PeliculasMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }
    }
}
