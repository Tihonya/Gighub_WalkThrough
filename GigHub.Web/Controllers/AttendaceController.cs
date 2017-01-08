using GigHub.Core;
using GigHub.DataLayer;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Web.Controllers
{
    [Authorize]
    public class AttendacesController : ApiController
    {
        
        private ApplicationDbContext _context;

        public AttendacesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody]int gigId)
        {
            var attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = User.Identity.GetUserId()
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
