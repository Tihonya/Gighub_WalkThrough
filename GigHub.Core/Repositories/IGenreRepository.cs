using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAllGeners();
    }
}