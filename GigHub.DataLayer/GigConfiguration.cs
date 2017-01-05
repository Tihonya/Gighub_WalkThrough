using GigHub.Core;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.DataLayer
{
    public class GigConfiguration: EntityTypeConfiguration<Gig>
   {
       public GigConfiguration()
       {
           Property(g => g.Venue).IsRequired().HasMaxLength(255);
           Property(g => g.ArtistId).IsRequired();
           Property(g => g.GenreId).IsRequired();


           // HasRequired(g => g.ArtistId); //WithRequiredDependent().WillCascadeOnDelete(false);
           //      HasRequired(g => g.Genre); //.WithMany().WillCascadeOnDelete(false);
           //    Property(g => g.DateTime).IsRequired();
           //   Property(g => g.Artist).IsRequired();



       }
    }
}
