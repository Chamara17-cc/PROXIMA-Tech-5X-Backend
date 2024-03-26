using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class DeveloperRate
    {
        [Key]
        public int RateId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public double CurrentRate { get; set; }

    }
}
