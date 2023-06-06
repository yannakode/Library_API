using Library.Data;
using Library.Models;
using Library.Models.DTO;
using Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        public readonly LibraryDbContext _dbContext;

        public AuthorRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthorDTO> GetById(int id)
        {
            var Author = await _dbContext.Author.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            var authorDTO = new AuthorDTO()
            {
                Id = id,
                Name = Author.Name,
                BookId = Author.BookId,
                Book = Author.Book
            };
            return authorDTO;
        }

        public async Task<List<AuthorDTO>> ShowAllAuthors()
        {
            var author = await _dbContext.Author.ToListAsync();
            var authorsDTO = author.Select(a => new AuthorDTO()
            {
                Id=a.Id,
                Name = a.Name,
                BookId = a.BookId,
                Book = a.Book
            }).ToList();
            return authorsDTO;
        }
        public async Task<AuthorDTO> AddAuthor(AuthorDTO authorDTO)
        {
            var author = new Author()
            {
                Id = authorDTO.Id,
                Name = authorDTO.Name,
                BookId = authorDTO.BookId,
                Book = authorDTO.Book

            };
            await _dbContext.Author.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            return authorDTO;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            Author authorToDelete = await _dbContext.Author.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            _dbContext.Author.Remove(authorToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<AuthorDTO> UpdateAuthor(int id, AuthorDTO authorDTO)
        {
            Author authorToUpdate = await _dbContext.Author.FirstOrDefaultAsync(b => b.Id == id);
            _dbContext.Author.Update(authorToUpdate);
            await _dbContext.SaveChangesAsync();
            var author = new AuthorDTO()
            {
                Id=id,
                Name = authorDTO.Name,
                BookId = authorDTO.BookId,
                Book = authorDTO.Book
            };
            return authorDTO;
        }
        public async Task<AuthorDTO> GetAuthorByName(string name)
        {
            Author authorByName = await _dbContext.Author.AsNoTracking().FirstOrDefaultAsync(a => a.Name == name);
            AuthorDTO author = new AuthorDTO()
            {
                Id= authorByName.Id, 
                Name = authorByName.Name,
                BookId = authorByName.BookId,
                Book = authorByName.Book
            };
            return author;
        }
    }
}
