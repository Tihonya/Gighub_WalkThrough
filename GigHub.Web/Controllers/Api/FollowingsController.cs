using GigHub.Core;
using GigHub.DataLayer;
using GigHub.Web.Dtos;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Web.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context =new ApplicationDbContext();
        }

        [HttpPost]
        [Route("api/followings/follow")]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var followings = _context.Followings.ToList();
         //   || userId == dto.FolloweeId

            if (followings.Any(f=>f.FolloweeId == dto.FolloweeId && f.FollowerId == userId) )
            {
                return BadRequest("Following already exists.");
            }

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();
            

            return Ok();
        }
        [Route("api/followings/follow/un")]
        [HttpPost]
        public IHttpActionResult UnFollow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var followings = _context.Followings;


            if (!followings.Any(f => f.FolloweeId == dto.FolloweeId && f.FollowerId == userId))
            {
                return BadRequest("Follower is apsent");
            }


            var follower = followings.Single(f => f.FolloweeId == dto.FolloweeId && f.FollowerId == userId);
            //   || userId == dto.FolloweeId


            _context.Followings.Remove(follower);
            _context.SaveChanges();


            return Ok();
        }

    }
}
