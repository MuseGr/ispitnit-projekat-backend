using Backend.Database;
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Backend.Services
{
    public class PostService : IPostService
    {
        public ForumDBContext _context;
        public UserManager<ForumUser> _userManager;
        public SignInManager<ForumUser> _signInManager;
        public IConfiguration _config;

        public PostService(ForumDBContext context, 
                           UserManager<ForumUser> userManager, 
                           SignInManager<ForumUser> signInManager,
                           IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        
    }
}
