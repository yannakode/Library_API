using Library.Data;
using Library.Models;
using Library.Models.DTO;
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

        public async Task<BookDTO> GetById(int id)
        {
            var Book = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            var BookDTO = new BookDTO()
            {
                Id = id,
                Title = Book.Title,
                Description = Book.Description,
                Status = Book.Status
             };
            return BookDTO;
        }

        public async Task<List<BookDTO>> ShowAllBooks()
        {
            var Books = await _dbContext.Book.ToListAsync();
            var BookDTO = Books.Select(book => new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Status = book.Status
            }).ToList();
            return BookDTO;
        }
        public async Task<BookDTO> AddBook(BookDTO bookDTO)
        {
            var book = new Book()
            {
                Id = bookDTO.Id,
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                Status = bookDTO.Status
            };
            await _dbContext.Book.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return bookDTO;
        }

        public async Task<bool> DeleteBook(int id)
        {
            Book existingBook = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            _dbContext.Book.Remove(existingBook);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<BookDTO> UpdateBook(int id, BookDTO bookDTO)
        {
            var book = new Book()
            {
                Id = bookDTO.Id,
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                Status = bookDTO.Status
            };
            Book existingBook = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.Status = book.Status;
            _dbContext.Book.Update(existingBook);
            await _dbContext.SaveChangesAsync();
            return bookDTO;
        }

        public async Task<BookDTO> GetBookByTitle(string title)
        {
            Book Book = await _dbContext.Book.FirstOrDefaultAsync(b => b.Title == title);
            var bookDTO = new BookDTO()
            {
                Id = Book.Id,
                Title = Book.Title,
                Description = Book.Description,
                Status = Book.Status
            };
            
            return bookDTO;
        }
    }
}
