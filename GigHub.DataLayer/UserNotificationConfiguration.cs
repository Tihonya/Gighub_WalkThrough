using GigHub.Core;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.DataLayer
{
    internal class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(un => new {un.UserId, un.NotificationId});
        }
    }
}