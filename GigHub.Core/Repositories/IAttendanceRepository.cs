using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int gigId, string userId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        void Remove(Attendance attendance);
        void Add(Attendance attendance);
    }
}