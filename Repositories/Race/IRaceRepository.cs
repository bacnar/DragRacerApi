using DragRacerApi.Repositories.Generic;

namespace DragRacerApi.Repositories.Race
{
    public interface IRaceRepository : IRepository<Entities.Race>
    {
        List<Entities.Race> GetRacesByBestTime();
    }
}
