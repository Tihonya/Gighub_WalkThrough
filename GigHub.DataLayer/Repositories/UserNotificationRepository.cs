﻿using GigHub.Core;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.DataLayer.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetUserNotificationsFor(string userId)
        {
           return _context.UserNotifications
               .Where(un => un.UserId == userId && !un.IsRead)
               .ToList();
        }
    }

}
