using GigHub.Core;
using GigHub.DataLayer;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;

namespace GigHub.Web.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            string userId = User.Identity.GetUserId();

            if (_context.Attendances.Any
                (a => a.AttendeeId == userId
                      && a.GigId == dto.GigId))
            {
                return BadRequest("The attendance already exists.");
            }

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var attendances = _context.Attendances.ToList();

            var attendance = attendances
                .SingleOrDefault(a => a.AttendeeId == userId
                                      && a.GigId == dto.GigId);

            if (attendance == null) { 
                return NotFound();
            }

            _context.Attendances.Remove(attendance);

            _context.SaveChanges();

            return Ok();
        }

    }
}
