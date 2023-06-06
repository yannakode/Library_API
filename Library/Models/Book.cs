using Library.Enums;
using Microsoft.AspNetCore.Http;
namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public StatusBook Status { get; set; }
    }
}
