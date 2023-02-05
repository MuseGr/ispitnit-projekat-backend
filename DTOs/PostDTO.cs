using Backend.Models;

namespace Backend.DTOs
{
    public class PostDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string? imageURL { get; set; }
        public string author { get; set; }
        public string? authorProfilePicture { get; set; }
        public DateTime dateCreated { get; set; }

        public PostDTO() { }
        public PostDTO(Post post)
        {
            id = post.Id; 
            title = post.Title; 
            content = post.Content;
            imageURL = post.ImageURL;
            dateCreated = post.DateCreated;
            author = post.Author.DisplayName;
            authorProfilePicture = post.Author.ProfileImageUrl;
        }

    }
}
