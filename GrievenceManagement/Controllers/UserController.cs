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
        public async Task<IActionResult> createTicket(IssueData issue, int? id)
        {

            var payloadData = HttpContext.User;
            var ID = "";
            if (payloadData?.Claims != null)
            {
                foreach (var claim in payloadData.Claims)
                {
                    ID = claim.Value;
                    break;
                }
            }
            
            if (id == Convert.ToInt32(ID))
            {
                var payloadId = Convert.ToInt32(ID);
                var staff = _context.StaffData.Where(x => x.Id == payloadId).SingleOrDefault();
                
                var complaint = new IssueData
                {
                    EmpId = staff.Id,
                    EmpName = staff.Username,
                    EmpDesignation = staff.Designation,
                    Defendent = issue.Defendent,
                    DefDesignation = issue.DefDesignation,
                    Subject = issue.Subject,
                    Description = issue.Description
                };

                _context.IssueData.Add(complaint);
                await _context.SaveChangesAsync();
                return Ok("Hey "+ complaint.EmpName +", Your Issue Is Raised Successfully");
            }
            else
            {
                return BadRequest("Incorrect EmpID !");
            }            
        }



        [HttpGet]
        [Route("viewTicket/{id}"), Authorize(Roles = "User")]
        public IEnumerable<IssueData> getTicket(int? id)
        {
            var ticket = _context.IssueData.Where(e => e.EmpId == id);
            return (ticket);
        }



        [HttpPut]
        [Route("updateTicket/{TicketNo}"), Authorize(Roles = "User")]
        public string updateTicket([FromBody] IssueData issue, int? TicketNo)
        {
            try
            {
                var updateData = _context.IssueData.Where(e => e.TicketNo == TicketNo).SingleOrDefault();

                updateData.Defendent = issue.Defendent;
                updateData.DefDesignation = issue.DefDesignation;
                updateData.Subject = issue.Subject;
                updateData.Description = issue.Description;
                _context.SaveChanges();
                return "Issue Ticket " + updateData.TicketNo + " Is Being Updated";
            }
            catch (Exception ex)
            {
                return "Error Occured " + ex;
            }
        }



        [HttpDelete]
        [Route("deleteTicket/{TicketNo}"), Authorize(Roles = "User")]
        public string deleteTicket(int? TicketNo)
        {
            try
            {
                var ticket = _context.IssueData.Where(e => e.TicketNo == TicketNo).SingleOrDefault();
                _context.IssueData.Remove(ticket);
                _context.SaveChanges();

                return "Your Ticket "+ TicketNo +" is Deleted Successfully";
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex;
            }
        }
    }
}

