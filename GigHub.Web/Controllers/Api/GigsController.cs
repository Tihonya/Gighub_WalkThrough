using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Web.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {

            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendance(id);
;
            if (gig == null)
            {
                return BadRequest("Not found");
            }

            if (gig.ArtistId != userId)
            {
                return Unauthorized();
            }

            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.Cancel();

           _unitOfWork.Complete();

            return Ok();
        }

    }
}
