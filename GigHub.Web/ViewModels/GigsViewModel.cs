using GigHub.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Web.ViewModels
{
    public class GigsViewModel
    {
        public bool ShowActions { get; set; }
        public List<Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }
        [MaxLength(100,ErrorMessage = "Search query must be shoter then 100 carecters.")]
        public string SearchTerm { get; set; }
    }
}