﻿using GigHub.Core;
using GigHub.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Web.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances
                .SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId
                && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }
    }
}