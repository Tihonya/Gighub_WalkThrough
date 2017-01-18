using GigHub.Core;
using GigHub.DataLayer;
using GigHub.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Web.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a=>a.Gig)
                .Include(g=>g.Artist)
                .Include(g=>g.Genre)
                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = attendances              
            };

            return View("Gigs",viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => 
                g.ArtistId == userId && 
                g.DateTime> DateTime.Now && 
                !g.IsCanceled)
                .Include(g=>g.Genre)
                .ToList();

            return View(gigs);
        }
        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }


        [Authorize]
        public ActionResult Create()
        {
            var  viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
                
            };
            return View("GigForm",viewModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)

        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId =  viewModel.Genre,
                Venue = viewModel.Venue

            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int gigId)
        {

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == gigId && g.ArtistId==userId);

            var viewModel = new GigFormViewModel
            {
                Id = gigId,
                Date = gig.DateTime.Date.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Genres = _context.Genres.ToList(),
                Heading = "Edit a Gig"
                                  
            };
            return View("GigForm",viewModel);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)

        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            string userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g=>g.Attendances.Select(a=>a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Update(venue: viewModel.Venue, dateTime: viewModel.GetDateTime(),genreId: viewModel.Genre);



          
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g =>g.Artist).Include(g=>g.Genre).SingleOrDefault(g=>g.Id == id);

            if (gig == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GigDetailsViewModel {Gig = gig};


            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
            }


            return View( "Details",viewModel);
        }


        public ActionResult GigDetais(int id)
        {

            Gig gig;
            GigDetailsViewModel__ viewModel;


            if (User.Identity.IsAuthenticated)
            {

                var useId = User.Identity.GetUserId();
                gig = _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g=>g.Artist.Followers)
                    .Include(g => g.Attendances)
                    .Include(g => g.Genre)
                    .Single(g => g.Id == id);
                var going = gig.Attendances.Select(a => a.AttendeeId == useId).Any();
                var follow = gig.Artist.Followers.Select(f => f.FolloweeId == useId).Any();
                viewModel = new GigDetailsViewModel__
                {
                    Id = gig.Id,
                    DateTime = gig.DateTime,
                    ArtistName = gig.Artist.Name,
                    ArtistId = gig.ArtistId,
                    Venue = gig.Venue,
                    Authorized = true,
                    Going = going,
                    Follow = follow
                };

            }
            else
            {
                 gig = _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .Single(g => g.Id == id);

                viewModel = new GigDetailsViewModel__
                {
                    Id = gig.Id,
                    DateTime = gig.DateTime,
                    ArtistName = gig.Artist.Name,
                    ArtistId = gig.ArtistId,
                    Venue = gig.Venue,
                    Authorized = false,
                    Going = false,
                    Follow = false
                };

            }

            return View(viewModel);
        }
    }
}