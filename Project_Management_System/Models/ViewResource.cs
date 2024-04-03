namespace Project_Management_System.Models
{
    public class ViewResource
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Users Users { get; set; }

        public int FileId { get; set; }
        public FileResource FileResources { get; set; }
    }
}
