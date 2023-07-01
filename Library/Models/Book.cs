using Library.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public StatusBook Status { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
    }
}