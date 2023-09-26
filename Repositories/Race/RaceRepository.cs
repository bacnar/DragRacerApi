using DragRacerApi.Contexts;
using DragRacerApi.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace DragRacerApi.Repositories.Race
{
    public class RaceRepository : GenericRepository<Entities.Race>, IRaceRepository
    {
        public RaceRepository(RacerContext context) : base(context)
        {

        }

        public List<Entities.Race> GetRacesByBestTime()
        {
            return context.Races
                .FromSql($"SELECT MIN(r1.Result) res, r1.* FROM Races r1 GROUP BY r1.UserId ORDER BY res ASC")
                .ToList();

        }
    }
}
