using Library.Models;
using Library.Models.DTO;

namespace Library.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorDTO>> ShowAllAuthors();
        Task<AuthorDTO> GetById(int id);
        Task<AuthorDTO> AddAuthor (AuthorDTO Author);
        Task<AuthorDTO> UpdateAuthor (int id, AuthorDTO Author); 
        Task<bool> DeleteAuthor (int id);
        Task<AuthorDTO> GetAuthorByName(string title);
    }
}
