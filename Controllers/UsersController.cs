using Backend.Database;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Backend.Filters;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    public class UsersController : Controller
    {
        public ForumDBContext _context;
        public IUserService _userService;
        
        public UsersController(ForumDBContext context, IUserService userService)
        {
            _context = context;
            _userService= userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterModel model)
        {
            await _userService.RegisterUser(model);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel model)
        {
            var token = await _userService.LoginUser(model);
            return Ok(token);
        }

        [UserFilter]
        [HttpGet("get-user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await _userService.GetUserFromToken(Request);
            return Ok(user);
        }
        [UserFilter]
        [HttpPost("change-user-info")]
        public async Task<IActionResult> EditUser([FromBody] UserDTO user)
        {
            var editUser = _context.ForumUsers.Where(u => u.Id == user.id).FirstOrDefault();
            if (editUser == null)
                return BadRequest("User ID not found");

            editUser.DisplayName = user.displayName;
            editUser.Email = user.email;
            editUser.ProfileImageUrl = user.profileImageUrl;
            _context.SaveChanges();
            return Ok(new UserDTO(editUser));
        }

        [HttpGet("get-users")]
        public IActionResult GetUsers()
        {
            var allUsers = _context.ForumUsers.ToList();

            return Ok(allUsers);
        }
        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] ForumUser _user)
        {
            return Ok();
        }

        [UserFilter]
        [HttpPost("create-post")]
        public async Task<IActionResult> CretePost([FromBody] PostDTO _post)
        {
            var userId = (await _userService.GetUserFromToken(Request)).id;

            var user = _context.ForumUsers.Include(u => u.PostsList).Where(u => u.Id == userId).FirstOrDefault();

            var post = new Post()
            {
                Title = _post.title,
                Content = _post.content,
                ImageURL = _post.imageURL,
                DateCreated = DateTime.Now
            };

            user.PostsList.Add(post);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("get-posts")]
        public IActionResult GetPosts()
        {
            var allPosts = _context.Posts.Include(u => u.Author).ToList();
            var results = new List<PostDTO>();
            foreach (var post in allPosts)
            {
                results.Add(new PostDTO(post));
            }

            return Ok(results);
        }
        [HttpGet("get-posts-for-user")]
        public async Task<IActionResult> GetPostsForUser()
        {
            var userId = (await _userService.GetUserFromToken(Request)).id;
            var userPosts = _context.Posts.Include(u => u.Author).Where(u => u.Author.Id == userId).ToList();
            var results = new List<PostDTO>();
            foreach (var post in userPosts)
            {
                results.Add(new PostDTO(post));
            }

            return Ok(results);
        }
    }
}
