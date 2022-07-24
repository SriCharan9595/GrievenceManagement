using System.ComponentModel.DataAnnotations;

namespace GrievenceManagement.Models
{
    public class IssueData
    {
        [Key]
        public int TicketNo { get; set; }
        public int EmpId { get; set; }   
        public string? EmpName { get; set; }
        public string? EmpDesignation { get; set; }
        public string Defendent { get; set; }
        public string DefDesignation { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string? Status { get; set; } = "Sent";
    }
}
