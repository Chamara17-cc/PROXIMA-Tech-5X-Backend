namespace Project_Management_System.DTOs
{
    public class ClientPayementDto
    {
        public int ProjectId {  get; set; }
        public int PaymentId { get; set; }
        public string ProjectName { get; set; }
        public int PaymentAmount { get; set; }
        public DateTime Date {  get; set; }
        public string Mode { get; set; }
        public bool status {  get; set; }
        public int clientId { get; set; }
    }
}
