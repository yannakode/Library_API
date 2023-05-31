using Library.Models;

namespace Library.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> ShowAllAuthors();
        Task<Author> GetById(int id);
        Task<Author> AddAuthor (Author Author);
        Task<Author> UpdateAuthor (int id, Author Author); 
        Task<bool> DeleteAuthor (int id);
        Task<Book> GetAuthorByTitle(string title);
    }
}
