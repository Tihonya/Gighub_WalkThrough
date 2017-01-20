using GigHub.Core;
using GigHub.Web.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null )
        {
            var upcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs();

            if ( !query.IsNullOrWhiteSpace())
            {
                var lquery = query.ToLower();

                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.ToLower().Contains(lquery)||
                        g.Genre.Name.ToLower().Contains(lquery)||
                        g.Venue.ToLower().Contains(lquery))
                        .ToList();
            }

            string userId = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                .ToLookup(a=>a.GigId);


            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs.ToList(),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcomming Gigs",
                SearchTerm = query,
                Attendances = attendances
               
            };

            return View("Gigs", viewModel);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}