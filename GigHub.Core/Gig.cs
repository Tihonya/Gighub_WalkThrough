using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get;private set; }    
        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }
        
        //set private set 
        public DateTime DateTime { get; set; }
        public string Venue { get;  set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = new Notification(this, NotificationType.GigCanceled);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Update(string venue, DateTime dateTime, byte genreId )
        {
            var notification = new Notification(this, NotificationType.GigUpdated, Venue,DateTime);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genreId;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }



        }
    }
}
