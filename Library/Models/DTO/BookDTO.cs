using Library.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.DTO
{
    public class BookDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public StatusBook Status { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
    }
}
