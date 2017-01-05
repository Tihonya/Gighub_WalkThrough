using GigHub.Core;
using GigHub.DataLayer;
using GigHub.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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
        public ActionResult Create()
        {
            var  viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()           
            };
            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(GigFormViewModel viewModel)

        {
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = DateTime.Parse(string.Format("{0} {1}", viewModel.Date , viewModel.Time)),
                GenreId =  viewModel.Genre,
                Venue = viewModel.Venue

            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}