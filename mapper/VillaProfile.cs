using AutoMapper;
using RoyalVilla_API.dtos;
using RoyalVilla_API.Models;

namespace RoyalVilla_API.mapper
{
    public class VillaProfile : Profile
    {
        public VillaProfile()
        {

            //we are mapping villa to villaDTO and vice versa
            CreateMap<Villa, VillaDTO>();
            //while createing a villa we dont have id and created date so we are not mapping those properties
            CreateMap<VillaCreateDTO, Villa>();

            CreateMap<UpdateVillaDTO, Villa>();
        }
    }
}
