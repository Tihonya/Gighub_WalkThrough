using AutoMapper;
using GigHub.Core;
using GigHub.Web.Controllers.Api;

namespace GigHub.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {          
             CreateMap<ApplicationUser, UserDto>();
             CreateMap<Genre, GenreDto>();
             CreateMap<Gig, GigDto>();
             CreateMap<Notification, NotificationDto>();                    
        }
    }
}