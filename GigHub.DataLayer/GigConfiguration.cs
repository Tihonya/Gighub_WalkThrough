using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigHub.Core;

namespace GigHub.DataLayer
{
   public class GigConfiguration: EntityTypeConfiguration<Gig>
   {
       public GigConfiguration()
       {
           Property(g => g.Venue).IsRequired().HasMaxLength(255);
           HasRequired(g => g.Artist).WithRequiredDependent().WillCascadeOnDelete(false);
           HasRequired(g => g.Genre).WithMany().WillCascadeOnDelete(false);
            Property(g => g.DateTime).IsRequired();
        //   Property(g => g.Artist).IsRequired();



       }
    }
}
