using GrievenceManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bcrypt = BCrypt.Net.BCrypt;

namespace Grievence_Management.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly GrievenceManagementDbContext _context;

        public RegisterController(IConfiguration configuration, GrievenceManagementDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<StaffData>> Register([FromBody] StaffData staffdata)
        {
            var checkEmail = await _context.StaffData.FirstOrDefaultAsync(x => x.Email == staffdata.Email);
            if (checkEmail == null)
            {
                if(staffdata.Email.Contains("@gmail.com") && staffdata.Password.Length > 8)
                {
                    staffdata.Password = bcrypt.HashPassword(staffdata.Password, 12);
                    _context.StaffData.Add(staffdata);
                    await _context.SaveChangesAsync();
                    return Ok("User Created Successfully");
                }
                else
                {
                    return BadRequest("Inputs does not meet requirements !");
                }
                
            }
            else
            {
                return BadRequest("User already exists !");
            }
        }

    }
}
