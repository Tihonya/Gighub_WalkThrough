using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GigHub.Core
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; set; }    
        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

    }
}
