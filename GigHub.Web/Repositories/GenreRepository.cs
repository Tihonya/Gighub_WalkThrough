using GigHub.Core;
using GigHub.DataLayer;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Web.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetAllGeners()
        {
           return _context.Genres.ToList();
        }
    }
}