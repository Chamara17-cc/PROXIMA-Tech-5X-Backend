namespace Project_Management_System.DTOs
{
    public class ViewClientListDto
    {

        public int ClientId { get; set; }
        public string UserName { get; set; }
        public required string Email { get; set; }
        public string ClientName { get; set; }
    }
}

