using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class ViewInvoice
    {
        [Key]
        public int Id { get; set; }
        public int AdminId { get; set; }
        public Admin Admins { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoices { get; set;}
        
    }
}
