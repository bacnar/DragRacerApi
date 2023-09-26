using DragRacerApi.Enums;

namespace DragRacerApi.Models
{
    public class SessionResponseDto
    {
        public int Id { get; set; }

        public ICollection<RaceResponseDto>? Races { get; set; }

        public SessionStatus Status { get; set; }

        public string StatusNamed { get => Status.ToString(); }
    }
}
