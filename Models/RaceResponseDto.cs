using DragRacerApi.Enums;

namespace DragRacerApi.Models
{
    public class RaceResponseDto
    {
        public int Id { get; set; }

        public UserResponseDto User { get; set; }

        public int StripId { get; set; }

        public RaceStatus Status { get; set; }

        public string StatusNamed { get => Status.ToString(); }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double? Result { get; set; }
    }
}
