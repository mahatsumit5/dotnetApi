using AutoMapper;
using RoyalVilla_API.dtos;
using RoyalVilla_API.Models;

namespace RoyalVilla_API.mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            //we are mapping villa to villaDTO and vice versa
            CreateMap<User, UserDTO>();
        }
    }
}
