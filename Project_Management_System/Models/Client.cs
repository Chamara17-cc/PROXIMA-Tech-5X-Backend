using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string NIC { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        public string? ClientEmail { get; set; }
        public string? ClientDescription { get; set; }
        public string? ProfilPhotoLink { get; set; }
        public double TotalPayment { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<Project> Projects { get; set; }
    }
}
