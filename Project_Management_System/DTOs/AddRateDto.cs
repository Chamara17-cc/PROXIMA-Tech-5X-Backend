using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.DTOs
{
    public class AddRateDto
    {
        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public double CurrentRate { get; set; }
    }
}
