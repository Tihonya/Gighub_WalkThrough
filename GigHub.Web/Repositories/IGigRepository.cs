using System.Collections.Generic;
using GigHub.Core;

namespace GigHub.Web.Repositories
{
    public interface IGigRepository
    {
        Gig GetGigWithAttendance(int gigId);
        Gig GetGigWithArtistAndGenre(int gigId);
        IEnumerable<Gig> GetGigsWhichBelongsToUser(string userId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        void Add(Gig gig);
    }
}