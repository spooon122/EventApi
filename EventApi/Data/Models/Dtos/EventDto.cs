namespace EventApi.Data.Models.Dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string? AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
