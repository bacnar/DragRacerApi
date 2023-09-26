using DragRacerApi.Entities;
using DragRacerApi.Hubs;
using DragRacerApi.Models;
using DragRacerApi.Repositories.Session;
using Microsoft.AspNetCore.SignalR;

namespace DragRacerApi.Services
{
    public class SessionManagerService : ISessionManagerService
    {
        private Session? _session = null;

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly IHubContext<TimingHub> _hubContext;

        public SessionManagerService(IServiceScopeFactory scopeFactory, IHubContext<TimingHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _hubContext = hubContext;
        }

        public async Task ManualStopSession()
        {
            _session!.Races!.ToList().ForEach(async race =>
            {
                if (race.Status == Enums.RaceStatus.Started || race.Status == Enums.RaceStatus.NotStarted)
                {
                    race.Status = Enums.RaceStatus.Dnf;
                    race.End = new DateTime();
                    await CalculateResult(race);
                }
            });

            await EndSession();
        }

        public async Task RegisterAction(StripActionDto action)
        {
            if (_session == null)
            {
                throw new Exception("Cannot start action");
            }

            var race = _session!.Races!.Where(s => s.StripId == action.StripId).First();

            if (_session.Status == Enums.SessionStatus.NotStarted)
            {
                _session.Status = Enums.SessionStatus.Started;
                await UpdateSession();
            }

            if (action.SensorPosition == Enums.SensorPosition.Start)
            {
                race.Start = action.Timestamp;
                race.Status = Enums.RaceStatus.Started;
                await _hubContext.Clients.All.SendAsync("raceStarted");
            }
            else
            {
                race.End = action.Timestamp;
                race.Status = Enums.RaceStatus.Stoped;
                await CalculateResult(race);
            }

            if (_session.Races!.All(s => s.Status == Enums.RaceStatus.Stoped))
            {
                await EndSession();
            }
        }

        public async Task ResetSession()
        {
            if (_session != null && _session.Status == Enums.SessionStatus.Started)
            {
                _session.Status = Enums.SessionStatus.Ended;
                _session.StartDate = DateTime.UtcNow;
                await UpdateSession();
            }
            _session = null;
        }

        public async Task StartSession(int sessionId)
        {
            using var scope = _scopeFactory.CreateScope();
            using var sessionRepository = scope.ServiceProvider.GetRequiredService<ISessionRepository>();

            var session = await sessionRepository.GetAsync(sessionId);
            if (_session is null && session!.Races!.All(r => r.Status == Enums.RaceStatus.NotStarted))
            {
                _session = session;
            }
            else
            {
                throw new Exception("Cannot start session that is already running");
            }
        }

        public Session? GetSessionInProggress()
        {
            return _session;
        }

        private async Task EndSession()
        {
            await ResetSession();
            await _hubContext.Clients.All.SendAsync("raceEnded");
        }

        private async Task CalculateResult(Race race)
        {
            if (race.End != null && race.Start != null)
            {
                race.Result = (race.End - race.Start).Value.TotalMilliseconds;
            }
            await _hubContext.Clients.All.SendAsync("timing", race.StripId, race.Result);
        }

        private async Task UpdateSession()
        {
            using var scope = _scopeFactory.CreateScope();
            using var sessionRepository = scope.ServiceProvider.GetRequiredService<ISessionRepository>();

            sessionRepository.Update(_session!);
            if (!await sessionRepository.SaveChangesAsync())
            {
                throw new Exception("Cannot update session");
            }
        }
    }
}
