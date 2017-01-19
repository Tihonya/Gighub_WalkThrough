using System.Collections.Generic;
using GigHub.Core;

namespace GigHub.Web.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAllGeners();
    }
}