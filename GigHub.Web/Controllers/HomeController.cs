using GigHub.DataLayer;
using GigHub.Web.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.DataLayer.Repositories;

namespace GigHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;


        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public ActionResult Index(string query = null )
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g=>g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled)
                .ToList();

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
            var attendances = _attendanceRepository.GetFutureAttendances(userId)
                .ToLookup(a=>a.GigId);


            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
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