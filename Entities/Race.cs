using DragRacerApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DragRacerApi.Entities
{
    [Table("Races")]
    public class Race
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Session))]
        public int SessionId { get; set; }

        [JsonIgnore]
        public Session? Session { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        public int StripId { get; set; }

        public RaceStatus Status { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public double? Result { get; set; }
    }
}
