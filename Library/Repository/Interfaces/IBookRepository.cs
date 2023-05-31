using Library.Models;

namespace Library.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> ShowAllBooks();
        Task<Book> GetById(int id);
        Task<Book> AddBook (Book book);
        Task<Book> UpdateBook (int id, Book book); 
        Task<bool> DeleteBook (int id);
        Task<Book> GetBookByTitle (string title);
    }
}
