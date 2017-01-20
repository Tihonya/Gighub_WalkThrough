using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Web.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult FollowingArtists()
        {
            var userId = User.Identity.GetUserId();

            var artists = _unitOfWork.ApplicationUsers.GetArtistFollowedBy(userId);
                
            return View(artists);
        }
    }
}