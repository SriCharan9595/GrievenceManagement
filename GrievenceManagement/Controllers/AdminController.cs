using GrievenceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grievence_Management.Controllers
{
    //[Route("[controller]")]
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
        [Route("getUsers"), Authorize(Roles = "Admin")]
        public ActionResult GetUsers()
        {
            var staff = _context.StaffData.Where(x => x.Role == "User");

            if (staff != null)
            {
                var users = staff.Select(x => new
                {
                    Id = x.Id,
                    UserName = x.Username,
                    Email = x.Email,
                    Designation = x.Designation
                });
                return Ok(users);
            }
            else
            {
                return BadRequest("No Users Are There...");
            }
        }



        [HttpGet]
        [Route("getIssues"), Authorize(Roles = "Admin")]
        public IEnumerable<IssueData> GetIssues()
        { 
            var issues = new List<IssueData>();
            issues = _context.IssueData.ToList();

            return (issues);
        }



        [HttpPut]
        [Route("updateStatus/{TicketNo}"), Authorize(Roles = "Admin")]
        public string updateStatus([FromBody] StatusDTO status, int? TicketNo)
        {
            try
            {
                var updateStatus = _context.IssueData.Where(e => e.TicketNo == TicketNo).SingleOrDefault();

                //var changes = new IssueData
                //{
                //    Defendent = updateStatus.Defendent,
                //    DefDesignation = updateStatus.DefDesignation,
                //    Subject = updateStatus.Subject,
                //    Description = updateStatus.Description,
                //    Status = issue.Status
                //};
                updateStatus.Status = status.Status;
                _context.SaveChanges();
                Send.Producer(status.Status);
                return "Issue Ticket " + updateStatus.TicketNo + " Is Being Updated";            
            }

            catch (Exception ex)
            {
                return "Error Occured " + ex;
            }
        }
    }
}
