using Lab3.Models;

namespace Lab3.Models
{
    public class Award
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // 1:1
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }
    }
}
