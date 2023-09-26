using System.ComponentModel.DataAnnotations;

namespace DragRacerApi.Models
{
    public class RegisterSessionDto
    {
        [MaxLength(2)]
        [Required]
        public List<RacerDto> RacerDtos { get; set; }
    }
}
