using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigHub.Core;

namespace GigHub.DataLayer
{
   public class GenerConfiguration : EntityTypeConfiguration<Genre>
   {
       public GenerConfiguration()
       {
           Property(g => g.Name).IsRequired().HasMaxLength(35);
       }
    }
}
