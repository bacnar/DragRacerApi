using DragRacerApi.Repositories.Generic;

namespace DragRacerApi.Repositories.Session
{
    public interface ISessionRepository : IRepository<Entities.Session>
    {
        Task<List<Entities.Session>> GetAllNotStarted();

        Task<List<Entities.Session>> GetAllEndedByDate();
    }
}
