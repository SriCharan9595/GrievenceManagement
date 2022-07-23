using GrievenceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrievenceManagement.Controllers
{
    //[Route("[controller]")]
    [ApiController]
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
        [Route("createTicket/{id}"), Authorize(Roles = "User")]
        public async Task<IActionResult> Issue(IssueData issue, int? id)
        {

            var principal = HttpContext.User;
            var ID = "";
            if (principal?.Claims != null)
            {
                foreach (var claim in principal.Claims)
                {
                    ID = claim.Value;
                    break;
                }
            }
            
            if (id == Convert.ToInt32(ID))
            {
                issue.EmpId = Convert.ToInt32(ID);
                _context.IssueData.Add(issue);
                await _context.SaveChangesAsync();
                return Ok("Successfully Raised Your Issue !");
            }
            else
            {
                return BadRequest("Incorrect EmpID");
            }            
        }

        [HttpPut]
        [Route("updateTicket/{TicketNo}"), Authorize(Roles = "User")]
        public string updateIssueStatus([FromBody] IssueData issue, int? TicketNo)
        {
            try
            {
                var newChanges = _context.IssueData.Where(e => e.TicketNo == TicketNo).SingleOrDefault();
                newChanges.Status = issue.Status;
                _context.SaveChanges();
                return "Issue Ticket " + issue.TicketNo + " Is Being Updated";
            }
            catch (Exception ex)
            {
                return "Error Occured " + ex;
            }
        }

        [HttpGet] 
        [Route("viewTicket/{id}"), Authorize(Roles = "User")]
        public IEnumerable<IssueData> viewTickets(int? id)
        {
            var ticket = _context.IssueData.Where(e => e.EmpId == id);
            return (ticket);
        }
}
}

