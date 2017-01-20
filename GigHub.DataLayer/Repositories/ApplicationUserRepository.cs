using GigHub.Core;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.DataLayer.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ApplicationUser> GetArtistFollowedBy(string userId)
        {
            return _context.Followings
                  .Where(f => f.FollowerId == userId)
                  .Select(f => f.Followee)
                  .ToList();
        }
    }
}
