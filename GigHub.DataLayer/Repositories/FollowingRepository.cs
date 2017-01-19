using System.Linq;
using GigHub.Core;
using GigHub.Core.Repositories;

namespace GigHub.DataLayer.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string artistId, string userId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }

    }
}