namespace Lab3.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        // 1:1
        public Award? Award { get; set; }

        // 1:N
        public int? DivisionId { get; set; }
        public Division? Division { get; set; }

        // N:N
        public List<ParticipantModule>? ParticipantModules { get; set; } = new List<ParticipantModule>();
    }
}
