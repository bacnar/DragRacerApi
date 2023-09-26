using DragRacerApi.Enums;

namespace DragRacerApi.Models
{
    public class UserResponseDto
    {
        private CarClass carClass = CarClass.None;

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Car { get; set; } = string.Empty;

        public CarClass CarClass { get => carClass; set => carClass = value; }

        public string CarClassNamed { get => carClass.ToString(); }
    }
}
