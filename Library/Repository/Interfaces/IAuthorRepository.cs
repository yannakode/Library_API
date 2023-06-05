using Library.Models;
using Library.Models.DTO;

namespace Library.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> ShowAllAuthors();
        Task<Author> GetById(int id);
        Task<Author> AddAuthor (AuthorDTO Author);
        Task<Author> UpdateAuthor (int id, AuthorDTO Author); 
        Task<bool> DeleteAuthor (int id);
        Task<Author> GetAuthorByName(string title);
    }
}
