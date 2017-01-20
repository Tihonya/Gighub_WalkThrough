using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGigWithAttendance(int gigId);
        Gig GetGigWithArtistAndGenre(int gigId);
        IEnumerable<Gig> GetGigsWhichBelongsToUser(string userId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> GetUpcomingGigs();
        void Add(Gig gig);
    }
}