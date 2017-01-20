using GigHub.Core;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.DataLayer
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new 
            {
                a.GigId,
                a.AttendeeId
            });
        }
    }
}