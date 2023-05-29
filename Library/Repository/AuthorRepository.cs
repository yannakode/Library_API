using Library.Data;
using Library.Models;
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

        public async Task<Author> GetById(int id)
        {
            return await _dbContext.Author.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Author>> ShowAllAuthors()
        {
            return await _dbContext.Author.ToListAsync();
        }
        public async Task<Author> AddAuthor(Author author)
        {
            await _dbContext.Author.AddAsync(author);
            await _dbContext.SaveChangesAsync();
            return author;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            Author existingAuthor = await _dbContext.Author.FirstOrDefaultAsync(b => b.Id == id);
            _dbContext.Author.Remove(existingAuthor);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Author> UpdateAuthor(int id, Author Author)
        {
            Author existingAuthor = await _dbContext.Author.FirstOrDefaultAsync(b => b.Id == id);
            existingAuthor.Name = Author.Name;
            existingAuthor.Book = Author.Book;
            existingAuthor.BookId = Author.BookId;
            _dbContext.Author.Update(existingAuthor);
            await _dbContext.SaveChangesAsync();
            return existingAuthor;
        }
    }
}
