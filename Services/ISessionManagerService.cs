using DragRacerApi.Entities;
using DragRacerApi.Models;

namespace DragRacerApi.Services
{
    public interface ISessionManagerService
    {
        Task StartSession(int sessionId);

        Task ResetSession();

        Task RegisterAction(StripActionDto action);

        Task ManualStopSession();

        Session? GetSessionInProggress();
    }
}
