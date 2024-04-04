using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class FileResource
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string LocalStoragePath { get; set; }
        public DateTime UpdatedDate { get; set; }
        //   public int ProjectId { get; set; }



    }
}
