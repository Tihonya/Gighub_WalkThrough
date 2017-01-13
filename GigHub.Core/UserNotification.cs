using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core
{
    public class UserNotification
    {

        //TO DO: Composet primary key in Fluient API
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        public ApplicationUser User { get; set; }

        public Notification Notification { get; set; }

        public bool IsRead { get; set; }
    }
}
