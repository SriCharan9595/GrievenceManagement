using GrievenceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grievence_Management.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly GrievenceManagementDbContext _context;

        public AdminController(IConfiguration configuration, GrievenceManagementDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getusers"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok("Hi");
        }
    }
}
