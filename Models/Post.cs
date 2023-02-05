namespace Backend.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageURL { get; set; }
        public ForumUser Author { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
