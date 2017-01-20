namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string artistId, string userId);
        void Remove(Following following);
        void Add(Following following);
    }
}