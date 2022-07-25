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


        public string payloadData()
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
            return ID;
        }



        [HttpPost]
        [Route("createTicket/{id}"), Authorize(Roles = "User")]
        public async Task<IActionResult> createTicket([FromBody]IssueData issue, int? id)
        { 
            var payloadId = Convert.ToInt32(payloadData());

            if (id == payloadId)
            {
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
        [Route("getTicket/{id}"), Authorize(Roles = "User")]
        public async Task<IActionResult> getTicket(int? id)
        {
            var payloadId = Convert.ToInt32(payloadData());

            if (id == payloadId)
            {
                var ticket = _context.IssueData.Where(e => e.EmpId == id);
                return Ok(ticket);
            }

            else
            {
                return BadRequest("Incorrect EmpID !");
            }
        }



        [HttpPut]
        [Route("updateTicket/{TicketNo}"), Authorize(Roles = "User")]
        public string updateTicket([FromBody] IssueData issue, int? TicketNo)
        {
            var payloadId = Convert.ToInt32(payloadData());
            
            var updateTicket = _context.IssueData.Where(e => e.TicketNo == TicketNo).SingleOrDefault();

            if (updateTicket.EmpId == payloadId)
            {
                updateTicket.Defendent = issue.Defendent;
                updateTicket.DefDesignation = issue.DefDesignation;
                updateTicket.Subject = issue.Subject;
                updateTicket.Description = issue.Description;
                _context.SaveChanges();
                return "Issue Ticket " + updateTicket.TicketNo + " Is Being Updated";
            }

            else
            {
                return "Error Occured " ;
            }
        }



        [HttpDelete]
        [Route("deleteTicket/{TicketNo}"), Authorize(Roles = "User")]
        public string deleteTicket(int? TicketNo)
        {
            var payloadId = Convert.ToInt32(payloadData());

            var ticket = _context.IssueData.Where(e => e.TicketNo == TicketNo).SingleOrDefault();

            if (ticket.EmpId == payloadId)
            {
                _context.IssueData.Remove(ticket);
                _context.SaveChanges();

                return "Your Ticket "+ TicketNo +" is Deleted Successfully";
            }

            else
            {
                return "Error Occured ";
            }
        }
    }
}

