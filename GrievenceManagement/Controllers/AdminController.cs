using GrievenceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<StaffData> GetUsers()
        {
            
            //var staffs = new List<StaffData>();

            var staffs =  _context.StaffData.Where(x => x.Role == "User");

            return (staffs);
        }
    }
}
