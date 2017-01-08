using System.Data.Entity.ModelConfiguration;
using GigHub.Core;

namespace GigHub.DataLayer
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasRequired(a=>a.Gig).WithMany().WillCascadeOnDelete(false);
        }
    }
}