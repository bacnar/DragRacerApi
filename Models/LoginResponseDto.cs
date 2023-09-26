using System.ComponentModel.DataAnnotations;

namespace DragRacerApi.Models
{
    public class LoginResponseDto
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
