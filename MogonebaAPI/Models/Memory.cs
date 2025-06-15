namespace MogonebaAPI.Models
{
    public class Memory
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty; // Optional image path
        public string UserId { get; set; } = string.Empty; // To associate with a user
    }

}
