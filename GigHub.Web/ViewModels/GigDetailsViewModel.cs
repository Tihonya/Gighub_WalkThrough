using GigHub.Core;
using System;

namespace GigHub.Web.ViewModels
{
    public class GigDetailsViewModel__
    {
        public int Id { get; set; }
        public string ArtistId { get; set; }
        public bool Authorized { get; set; }
        public bool Follow { get; set; }
        public bool Going { get; set; }

        public DateTime DateTime { get; set; }
        public string Venue { get; set; }

        public string ArtistName { get; set; }


    }

    public class GigDetailsViewModel
    {
        public Gig  Gig { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }

    }

}