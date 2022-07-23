﻿using GrievenceManagement.Models;
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
            var staffs = _context.StaffData.Where(x => x.Role == "User");

            if (staffs != null)
            {
                var users = staffs.Select(u => new
                {
                    Id = u.Id,
                    UserName = u.Username,
                    Email = u.Email,
                    Designation = u.Designation
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
    }
}
