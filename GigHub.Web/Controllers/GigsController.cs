using GigHub.Core;
using GigHub.DataLayer;
using GigHub.Web.Repositories;
using GigHub.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Web.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;
        private readonly GenreRepository _genreRepository;
        private readonly FollowingRepository _followingRepository;



        public GigsController()
        {
            
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
            _genreRepository = new GenreRepository(_context);
            _followingRepository= new FollowingRepository(_context);

        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId).ToList(),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _attendanceRepository.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)              
            };

            return View("Gigs",viewModel);
        }


        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _gigRepository.GetGigsWhichBelongsToUser(userId);

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
                Genres = _genreRepository.GetAllGeners(),
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
                viewModel.Genres = _genreRepository.GetAllGeners();
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

            var gig = _gigRepository.GetGigWithAttendance(gigId);

            if (gig == null)
            {
                return HttpNotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }


            var viewModel = new GigFormViewModel
            {
                Id = gigId,
                Date = gig.DateTime.Date.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Genres = _genreRepository.GetAllGeners(),
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
                viewModel.Genres = _genreRepository.GetAllGeners();
                return View("GigForm", viewModel);
            }

            var gig = _gigRepository.GetGigWithAttendance(viewModel.Id);

            if (gig==null)
            {
                return HttpNotFound();
            }
            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            gig.Update(venue: viewModel.Venue, dateTime: viewModel.GetDateTime(),genreId: viewModel.Genre);
          
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Details(int id)
        {
            var gig = _gigRepository.GetGigWithArtistAndGenre(id);

            if (gig == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GigDetailsViewModel {Gig = gig};

            if (User.Identity.IsAuthenticated)
            {

                viewModel.IsAttending = _attendanceRepository
                    .GetAttendance(id, User.Identity.GetUserId()) !=null;

                viewModel.IsFollowing = _followingRepository
                    .GetFollowing(gig.ArtistId, User.Identity.GetUserId()) != null;
            }

            return View( "Details",viewModel);
        }


    }
}