namespace Project_Management_System.DTOs
{
    public class PasswordResetDto
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
