using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class RefreshToken
    {

        [Key]
        public int TokenId { get; set; }
        public string Token { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
