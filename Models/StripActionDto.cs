using DragRacerApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace DragRacerApi.Models
{
    public class StripActionDto
    {
        [Required]
        public int StripId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public SensorPosition SensorPosition { get; set; }
    }
}
