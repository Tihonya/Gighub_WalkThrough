using System.Collections.Generic;
using System.Linq;
using GigHub.Core;
using GigHub.Core.Repositories;

namespace GigHub.DataLayer.Repositories
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