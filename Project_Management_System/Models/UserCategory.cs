using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class UserCategory
    {

        [Key]
        public int UserCategoryId { get; set; }
        [Required]
        public string UserCategoryType { get; set; }
    }
}
