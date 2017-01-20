using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetArtistFollowedBy(string userId);
    }
}