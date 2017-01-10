using GigHub.Core;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.DataLayer
{
    public class ApplicationUserConfiguration: EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(u => u.Name).IsRequired().HasMaxLength(100);

            HasMany(u=>u.Followers)
                .WithRequired(f=>f.Follower)
                .WillCascadeOnDelete(false);

            HasMany(u=>u.Followees)
                .WithRequired(f=>f.Followee)
                .WillCascadeOnDelete(false);
        }
    }
}
