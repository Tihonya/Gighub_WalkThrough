using GigHub.Core;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.DataLayer
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
  {
      public NotificationConfiguration()
      {
          HasRequired(n => n.Gig);
      }
    }
}
