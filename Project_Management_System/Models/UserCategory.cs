using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class UserCategory
    {

        [Key]
        public string CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public List<Users> Users { get; set; }


    }
}
