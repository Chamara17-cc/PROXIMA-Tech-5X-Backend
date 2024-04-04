using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class ViewInvoice
    {
        [Key]
        public int UserId { get; set; }
        public int InvoiceId { get; set; }
        public Admin Admins { get; set; }
        public Invoice Invoices { get; set; }

    }
}
