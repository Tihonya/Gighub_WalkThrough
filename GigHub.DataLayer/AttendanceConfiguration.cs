using GigHub.Core;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.DataLayer
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasRequired(a=>a.Gig).WithMany(g=>g.Attendances).WillCascadeOnDelete(false);
        }
    }
}