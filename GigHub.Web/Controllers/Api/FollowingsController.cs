using GigHub.Core;
using GigHub.DataLayer;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;

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
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var followings = _context.Followings.ToList();

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

        [HttpDelete]
        public IHttpActionResult UnFollow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var followings = _context.Followings;

            var follower = followings.SingleOrDefault(f => f.FolloweeId == dto.FolloweeId && f.FollowerId == userId);

            if (follower ==null)
            {
                return NotFound();
            }
           
            _context.Followings.Remove(follower);
            _context.SaveChanges();

            return Ok();
        }

    }
}
