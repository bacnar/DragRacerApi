using DragRacerApi.Contexts;
using DragRacerApi.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace DragRacerApi.Repositories.Session
{
    public class SessionRepository : GenericRepository<Entities.Session>, ISessionRepository
    {
        public SessionRepository(RacerContext context) : base(context)
        {

        }

        public Task<List<Entities.Session>> GetAllNotStarted()
        {
            return context.Sessions.Where(s => s.Status == Enums.SessionStatus.NotStarted).ToListAsync();
        }

        public Task<List<Entities.Session>> GetAllEndedByDate()
        {
            return context.Sessions.Where(s => s.Status == Enums.SessionStatus.Ended).OrderByDescending(s => s.StartDate).ToListAsync();
        }
    }
}
