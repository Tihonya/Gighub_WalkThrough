using GigHub.Core;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.DataLayer
{
    public class FollowingConfiguration: EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(f => new {f.FollowerId, f.FolloweeId});
        }
    }
}
