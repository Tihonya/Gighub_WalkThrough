﻿using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.DataLayer.Repositories;

namespace GigHub.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendances = new AttendanceRepository(context);
            Followings = new FollowingRepository(context);
            Genres = new GenreRepository(context);
            Notifications= new NotificationRepository(context);
            UserNotifications=new UserNotificationRepository(context);
            ApplicationUsers= new ApplicationUserRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}