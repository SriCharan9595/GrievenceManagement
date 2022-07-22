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

            var staffs =  _context.StaffData.Where(x => x.Role == "User");

            return (staffs);
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
        [Route("updateIssueStatus"), Authorize(Roles = "Admin")]
        public string updateIssueStatus([FromBody] IssueData issue)
        {
            try
            {
                var newChanges = _context.IssueData.Where(e => e.EmpId == issue.EmpId).SingleOrDefault();
                newChanges.EmpId = issue.EmpId;
                newChanges.TicketNo = issue.TicketNo;
                newChanges.Subject = issue.Subject;
                newChanges.Description = issue.Description;
                newChanges.Status = issue.Status;
                _context.SaveChanges();
                return "ssue ticket " + issue.TicketNo + " is being Updated";
            }
            catch (Exception ex)
            {
                return "Error Occured " + ex;
            }
        }
    }
}
