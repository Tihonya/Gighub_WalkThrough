using GigHub.Core;
using GigHub.Core.Dtos;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Web.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;


        public FollowingsController(IUnitOfWork unitOfWork )
        {

            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.Followings.GetFollowing(dto.FolloweeId, userId) != null)
            {
                return BadRequest("Following already exists.");
            }

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

           return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var follower = _unitOfWork.Followings.GetFollowing(dto.FolloweeId, userId);

            if (follower == null)
            {
                return NotFound();
            }

            _unitOfWork.Followings.Remove(follower);

            _unitOfWork.Complete();

            return Ok();
        }

    }
}
