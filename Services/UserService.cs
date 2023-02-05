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
    public class UserService : IUserService
    {
        public ForumDBContext _context;
        public UserManager<ForumUser> _userManager;
        public SignInManager<ForumUser> _signInManager;
        public IConfiguration _config;

        public UserService(ForumDBContext context, 
                           UserManager<ForumUser> userManager, 
                           SignInManager<ForumUser> signInManager,
                           IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        public async Task<bool> RegisterUser(RegisterModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user != null)
                throw (new Exception("Email already exists"));

            user = new ForumUser() { Email = model.email,
                                     UserName = model.email,
                                     DisplayName = model.displayName,
                                     ProfileImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png"
            };

            var res = await _userManager.CreateAsync(user, model.password);
            if (res.Succeeded)
            {
                return true;
            }
            else
            {
                throw (new Exception(res.Errors.FirstOrDefault().Description));
            }
        }
        public async Task<JwtToken> LoginUser(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if(user == null)
            {
                throw (new Exception("Wrong email"));
            }
                
            var res = await _signInManager.CheckPasswordSignInAsync(user, model.password, false);
            if (res.Succeeded)
            {
                return CreateToken(user);
            }
            else
            {
                throw (new Exception("Wrong password"));
            }
        }
        public JwtToken CreateToken(ForumUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            return new JwtToken()
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
        public async Task<UserDTO> GetUserFromToken(HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"].ToString();
            try
            {
                var token = authHeader.Substring(7).Trim();
                var jwt =  new JwtSecurityTokenHandler().ReadJwtToken(token);

                var email = jwt.Claims.First(claim => claim.Type == "sub").Value;
                
                //provera isteka tokena ==> istek je u type == "exp"

                return new UserDTO(await _userManager.FindByEmailAsync(email));
            }
            catch 
            {
                return null;
            }
        }
    }
}
