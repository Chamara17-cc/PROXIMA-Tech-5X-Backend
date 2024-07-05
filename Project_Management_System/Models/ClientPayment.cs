using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project_Management_System.Models

{
    public class ClientPayment
    {
        [Key]
        public int PaymentId {  get; set; }
        public int Payment { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool Status { get; set; }
        public string Mode { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
        public int ProjectId { get; set; }

    }
}
