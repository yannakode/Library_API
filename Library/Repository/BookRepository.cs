using Library.Data;
using Library.Models;
using Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        public readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> GetById(int id)
        {
            return await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> ShowAllBooks()
        {
            return await _dbContext.Book.ToListAsync();
        }
        public async Task<Book> AddBook(Book book)
        {
            await _dbContext.Book.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBook(int id)
        {
            Book existingBook = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            _dbContext.Book.Remove(existingBook);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Book> UpdateBook(int id, Book book)
        {
            Book existingBook = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.Status = book.Status;
            _dbContext.Book.Update(existingBook);
            await _dbContext.SaveChangesAsync();
            return existingBook;
        }

        public async Task<Book> GetBookByTitle(string title)
        {
            Book GetByTitle = await _dbContext.Book.FirstOrDefaultAsync(b => b.Title == title);
            return GetByTitle;
        }
    }
}
