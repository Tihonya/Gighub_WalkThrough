using AutoMapper;
using GigHub.Core;
using GigHub.DataLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;

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


           return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }


        /// <summary>
        /// This method has a BAD logic. 
        /// What if between GetNewNotifications() method and MarkAsRead() user get some new notifications?
        /// In that case user will never  see this notifications. 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n=> n.Read());

            _context.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// not in use
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NotificationDto> GetMostRecentNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Where(n=>n.DateTime>= DateTime.Now - new TimeSpan(5,0,0,0))
                .Include(n => n.Gig.Artist)
                .ToList();


            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }



    }
}
