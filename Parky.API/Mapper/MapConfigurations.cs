using AutoMapper;
using Parky.API.Models;
using Parky.API.Models.Dtos;

namespace Parky.API.Mapper
{
    public class MapConfigurations:Profile
    {
        public MapConfigurations()
        {
            CreateMap<NationalPark,NationalParkDto>().ReverseMap();
        }
    }
}
