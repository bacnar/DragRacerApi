using System.ComponentModel.DataAnnotations;

namespace DragRacerApi.Models
{
    public class AuthUserDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
