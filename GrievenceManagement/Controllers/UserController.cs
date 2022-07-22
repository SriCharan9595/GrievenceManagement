using GrievenceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrievenceManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly GrievenceManagementDbContext _context;

        public UserController(IConfiguration configuration, GrievenceManagementDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("createissue"), Authorize(Roles = "User")]
        public async Task<IActionResult> Issue(IssueData issue)
        {
            _context.IssueData.Add(issue);
            await _context.SaveChangesAsync();
            return Ok("Successfully Raised Your Issue !");
        }
    }
}
