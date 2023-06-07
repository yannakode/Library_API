using Library.Models;
using Library.Models.DTO;

namespace Library.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDTO>> ShowAllBooks();
        Task<BookDTO> GetById(int id);
        Task<BookDTO> AddBook (BookDTO book);
        Task<BookDTO> UpdateBook (int id, BookDTO book); 
        Task<bool> DeleteBook (int id);
        Task<BookDTO> GetBookByTitle (string title);
        Task<BookDTO> UploadImage (BookDTO bookDTO);
    }
}
