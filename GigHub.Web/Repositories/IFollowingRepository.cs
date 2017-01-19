using GigHub.Core;

namespace GigHub.Web.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string artistId, string userId);
    }
}