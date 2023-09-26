using DragRacerApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DragRacerApi.Entities
{
    [Table("Sessions")]
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<Race>? Races { get; set; }

        public SessionStatus Status { get; set; } = SessionStatus.NotStarted;

        public DateTime? StartDate { get; set; }
    }
}
