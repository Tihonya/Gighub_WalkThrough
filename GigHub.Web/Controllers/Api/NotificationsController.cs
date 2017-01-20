using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Web.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notifications.GetNewNotificationsFor(userId);


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
            var notifications = _unitOfWork.UserNotifications
                .GetUserNotificationsFor(userId);

            notifications.ForEach(n=> n.Read());

            _unitOfWork.Complete();

            return Ok();
        }
        /// <summary>
        /// not in use
        /// </summary>
        /// <returns></returns>
       
        //public IEnumerable<NotificationDto> GetMostRecentNotifications()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var notifications = _context.UserNotifications
        //        .Where(un => un.UserId == userId)
        //        .Select(un => un.Notification)
        //        .Where(n=>n.DateTime>= DateTime.Now - new TimeSpan(5,0,0,0))
        //        .Include(n => n.Gig.Artist)
        //        .ToList();


        //    return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        //}



    }
}
