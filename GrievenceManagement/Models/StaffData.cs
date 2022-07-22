using System.ComponentModel.DataAnnotations;

namespace GrievenceManagement.Models
{
    public class StaffData
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string? Role { get; set; } = "User";
    }
}
