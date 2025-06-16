namespace Lab3.Models
{
    public class Division
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // 1:N
        public List<Participant>? Participants { get; set; } = new List<Participant>();
    }
}
