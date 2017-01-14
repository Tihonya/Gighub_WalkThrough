using AutoMapper;
using GigHub.Core;
using GigHub.DataLayer;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Web.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context=new ApplicationDbContext();
        }
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un=>un.Notification)
                .Include(n=>n.Gig.Artist)
                .ToList();
            
           Mapper.Initialize(cfg =>
           {
               cfg.CreateMap<ApplicationUser, UserDto>();
               cfg.CreateMap<Genre, GenreDto>();
               cfg.CreateMap<Gig, GigDto>();
               cfg.CreateMap<Notification, NotificationDto>();
           });

           return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
