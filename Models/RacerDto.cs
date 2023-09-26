using DragRacerApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace DragRacerApi.Models
{
    public class RacerDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int StripId { get; set; }

        public RaceStatus Status { get; private set; } = RaceStatus.NotStarted;
    }
}
