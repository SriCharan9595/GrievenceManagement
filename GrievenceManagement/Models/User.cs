namespace GrievenceManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string Role { get; set; } = null!;


    }
}
