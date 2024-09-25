namespace EventApi.Data.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string? AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }

        public Event(string? authorId, string? name, string? description, DateTime startDate)
        {
            CreatedDate = DateTime.UtcNow;
            AuthorId = authorId;
            Name = name;
            Description = description;
            StartDate = startDate;
        }
    }
}
