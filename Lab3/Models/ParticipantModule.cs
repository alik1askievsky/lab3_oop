namespace Lab3.Models
{
    public class ParticipantModule
    {
        public int Id { get; set; }

        public int? ParticipantId { get; set; }
        public Participant? Participant { get; set; }

        public int? ModuleId { get; set; }
        public Module? Module { get; set; }
    }
}
