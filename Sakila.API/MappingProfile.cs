using AutoMapper;

using Sakila.Core.Movies.DTOs;
using Sakila.Core.Movies.Models;

namespace Sakila.API
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Film, FilmDTO>().ReverseMap(); 
            CreateMap<FilmCategory, FilmCategoryDTO>().ReverseMap();
            CreateMap<FilmActor, FilmActorDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<Inventory, InventoryDTO>().ReverseMap();
        }
    }
}
