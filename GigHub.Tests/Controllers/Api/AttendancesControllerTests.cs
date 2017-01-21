using FluentAssertions;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Repositories;
using GigHub.Tests.Controllers.Extensions;
using GigHub.Web.Controllers.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
   public class AttendancesControllerTests
    {
        private Mock<IAttendanceRepository> _mockAttendanceRepository;
        private AttendancesController _controller;
        private string _userId;

        public AttendancesControllerTests()
        {
            _mockAttendanceRepository = new Mock<IAttendanceRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Attendances).Returns(_mockAttendanceRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId,"user1@domain.com");

        }

        //[TestMethod]
        //public void Attend_NoGigWithGivenIdExists_ShouldReturnBadRequest()
        //{
        //    //var gigs = new List<Gig> 
        //    //{
        //    //    new Gig {Id=1 },
        //    //    new Gig { Id = 2}
        //    //};
        //    _mockGigsRepository.Setup()
        //    _mockGigsRepository.Setup(r => r.GetGigWithAttendance(123)).Returns(null);
        //    var result = _controller.Attend(new AttendanceDto {GigId = 123});

        //    result.Should().BeOfType(typeof(BadRequestResult));

        //}

        [TestMethod]
        public void Attend_UserAttendingAGigForWhichHeHasAnAttendance_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();
            _mockAttendanceRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Delete_NoAttendanceWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Delete(new AttendanceDto {GigId = 1});

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_ValidRequest_ShouldReturnOk()
        {
            var attendance = new Attendance();
            _mockAttendanceRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

            var result = _controller.Delete(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkNegotiatedContentResult<AttendanceDto>>();
        }

        [TestMethod]
        public void Delete_ValidRequest_ShouldReturnTheAttendanceDtoOfDeletedAttendance()
        {
            var attendance = new Attendance();
            var attendanceDto = new AttendanceDto {GigId = 1};
            _mockAttendanceRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

            
            var result = (OkNegotiatedContentResult<AttendanceDto>)_controller.Delete(attendanceDto);

            result.Content.Should().Be(attendanceDto);
        }


    }
}
