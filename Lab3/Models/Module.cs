namespace Lab3.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // N:N
        // public List<Participant>? Participants { get; set; } = new List<Participant>();
        public List<ParticipantModule>? ParticipantModules { get; set; } = new List<ParticipantModule>();
    }
}
