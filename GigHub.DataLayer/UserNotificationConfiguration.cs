using GigHub.Core;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.DataLayer
{
    internal class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasRequired(un=>un.User).WithMany(au=>au.UserNotifications).WillCascadeOnDelete(false);
        }
    }
}