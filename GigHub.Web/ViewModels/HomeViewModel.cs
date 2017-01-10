using System.Collections.Generic;
using GigHub.Core;

namespace GigHub.Web.ViewModels
{
    public class HomeViewModel
    {
        public bool ShowActions { get; set; }
        public List<Gig> UpcomingGigs { get; set; }
    }
}