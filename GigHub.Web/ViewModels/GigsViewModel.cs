using GigHub.Core;
using System.Collections.Generic;

namespace GigHub.Web.ViewModels
{
    public class GigsViewModel
    {
        public bool ShowActions { get; set; }
        public List<Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }
    }
}