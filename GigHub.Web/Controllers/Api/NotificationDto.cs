using GigHub.Core;
using System;

namespace GigHub.Web.Controllers.Api
{
    public class NotificationDto
    {
        

        public DateTime DateTime { get;  set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get;  set; }


        public string OriginalVenue { get;  set; }

        public GigDto Gig { get;  set; }
    }
}