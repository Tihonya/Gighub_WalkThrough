using GigHub.Core;
using System;

namespace GigHub.Web.Dtos
{
    public class NotificationDto
    {

        public int Id { get; set; }

        public DateTime DateTime { get;  set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get;  set; }


        public string OriginalVenue { get;  set; }

        public GigDto Gig { get;  set; }
    }
}