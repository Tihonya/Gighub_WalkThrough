using GigHub.Core;
using GigHub.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Repositories;

namespace GigHub.Web.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;



        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId).ToList(),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)              
            };

            return View("Gigs",viewModel);
        }


        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetGigsWhichBelongsToUser(userId);

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
                Genres = _unitOfWork.Genres.GetAllGeners(),
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
                viewModel.Genres = _unitOfWork.Genres.GetAllGeners();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId =  viewModel.Genre,
                Venue = viewModel.Venue

            };

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int gigId)
        {

            var gig = _unitOfWork.Gigs.GetGigWithAttendance(gigId);

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
                Genres = _unitOfWork.Genres.GetAllGeners(),
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
                viewModel.Genres = _unitOfWork.Genres.GetAllGeners();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendance(viewModel.Id);

            if (gig==null)
            {
                return HttpNotFound();
            }
            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            gig.Update(venue: viewModel.Venue, dateTime: viewModel.GetDateTime(),genreId: viewModel.Genre);
          
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigWithArtistAndGenre(id);

            if (gig == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GigDetailsViewModel {Gig = gig};

            if (User.Identity.IsAuthenticated)
            {

                viewModel.IsAttending = _unitOfWork.Attendances
                    .GetAttendance(id, User.Identity.GetUserId()) !=null;

                viewModel.IsFollowing = _unitOfWork.Followings
                    .GetFollowing(gig.ArtistId, User.Identity.GetUserId()) != null;
            }

            return View( "Details",viewModel);
        }


    }
}