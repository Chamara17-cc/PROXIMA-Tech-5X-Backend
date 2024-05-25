namespace Project_Management_System.DTOs
{
    public class ViewUserListDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public required string Email { get; set; }
        public string UserCategoryType { get; set; }
    }
}
