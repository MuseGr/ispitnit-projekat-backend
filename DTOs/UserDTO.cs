using Backend.Models;
using System.ComponentModel;

namespace Backend.DTOs
{
    public class UserDTO
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string displayName { get; set; }
        public bool isActive { get; set; }
        public DateTime dateCreated { get; set; }
        public string profileImageUrl { get; set; }
        public List<Post> postsList { get; set; }

        public UserDTO() { }
        public UserDTO(ForumUser user)
        {
            id = user.Id;
            firstName = user.FirstName;
            lastName = user.LastName;
            email = user.Email;
            displayName = user.DisplayName;
            isActive = user.IsActive;
            dateCreated = user.Created;
            profileImageUrl = user.ProfileImageUrl;
            postsList = user.PostsList;
        }
    }
}
