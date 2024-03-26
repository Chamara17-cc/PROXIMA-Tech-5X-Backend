using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project_Management_System.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string InvoiceName { get; set; }
        public string ExpenseType { get; set; }
        public double ExpenseAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }

        [JsonIgnore]
        public Project Project {  get; set; }   
        public int ProjectId { get; set; }
        public ICollection <ViewInvoice> ViewInvoices { get; set; }

    }
}
