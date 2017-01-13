﻿using GigHub.DataLayer;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Web.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context= new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g=>g.Attendances.Select(a=>a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.Cancel();

            _context.SaveChanges();

            return Ok();
        }

    }
}
