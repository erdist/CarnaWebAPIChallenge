using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Admin, Admin>();
            CreateMap<User, User>();
            CreateMap<Content, Content>();
            CreateMap<AuthAdmin, AuthAdmin>();
        }
    }
}