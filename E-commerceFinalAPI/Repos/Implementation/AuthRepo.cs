using E_commerceFinalAPI.Data;
using E_commerceFinalAPI.DTO.Auth;
using E_commerceFinalAPI.Models;
using E_commerceFinalAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerceFinalAPI.Repos.Implementation
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _conf;
        public AuthRepo(AppDbContext context, IConfiguration conf)
        {
            _context = context;
            _conf = conf;
        }
        public async Task<string> Login(LoginDTO dto)
        {
            var user= await _context.Users.FirstOrDefaultAsync(c=>c.Email==dto.Email);
            if (user==null) { return null; }

            if (user.Password == dto.Password)
            {
                return GenerateToken(user);
            }
            return null;
        }

        public async Task<bool> Register(RegisterDTO dto)
        {
            var u = await _context.Users.AnyAsync(c => c.Email == dto.Email);
            if (u) { return false; }

            var user = new User
            {
                FullName=dto.FullName,
                Email=dto.Email,
                Password=dto.Password,
                Role=dto.Role,
            };
            
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        private string GenerateToken(User user)
        {
            var claim = new List<Claim> {

            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name,user.FullName),
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_conf["JWT:Key"]));
            var sinig = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var tokenobj = new JwtSecurityToken(

                issuer: _conf["JWT:Issuer"],
                audience: _conf["JWT:Audience"],
                claims:claim,
                signingCredentials:sinig,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_conf["JWT:ExpirationInHours"]))
                );

            var token=new JwtSecurityTokenHandler().WriteToken(tokenobj);
            return token;
        }
    }
}
