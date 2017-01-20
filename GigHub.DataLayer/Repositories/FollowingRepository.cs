using GigHub.Core;
using GigHub.Core.Repositories;
using System.Linq;

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


        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }
    }
}