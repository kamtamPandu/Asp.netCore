using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserLoginController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _userRepository.GetAll()
                .FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var jwtSettings = _configuration.GetSection("Jwt");
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Ensure the key is at least 32 bytes for HmacSha256
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}