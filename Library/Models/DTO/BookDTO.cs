using Library.Enums;

namespace Library.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public StatusBook Status { get; set; }
    }
}
