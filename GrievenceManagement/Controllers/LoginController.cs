using Grievence_Management.Models;
using GrievenceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using bcrypt = BCrypt.Net.BCrypt;

namespace Grievence_Management.Controllers
{

    //[Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly GrievenceManagementDbContext _context;

        public LoginController(IConfiguration configuration, GrievenceManagementDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }



        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<StaffData>> Login([FromBody] Login login)
        {
            var checkEmail = await _context.StaffData.FirstOrDefaultAsync(x => x.Email == login.Email);
            if (checkEmail != null)
            {
                if(bcrypt.Verify(login.Password, checkEmail.Password))
                {
                    var token = CreateToken(checkEmail);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
            }
            else
            {
                return BadRequest("Invalid User");
            } 
        }



        private string CreateToken(StaffData staffdata)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("ID",staffdata.Id.ToString()),
                new Claim(ClaimTypes.Email, staffdata.Email),
                new Claim(ClaimTypes.Role, staffdata.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("SecretKey:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}

