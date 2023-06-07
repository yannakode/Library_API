using AutoMapper;
using Library.Data;
using Library.Models;
using Library.Models.DTO;
using Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookRepository(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BookDTO> GetById(int id)
        {
            var Book = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            var bookDTO = _mapper.Map<BookDTO>(Book);
            return bookDTO;
        }

        public async Task<IEnumerable<BookDTO>> ShowAllBooks()
        {
            IEnumerable<Book> bookList = await _dbContext.Book.ToListAsync();
            IEnumerable<BookDTO> bookListDTO = _mapper.Map<IEnumerable<BookDTO>>(bookList);
            return bookListDTO;
        }
        public async Task<BookDTO> AddBook(BookDTO bookDTO)
        {
            var newBook = _mapper.Map<Book>(bookDTO);

            await _dbContext.Book.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            var newBookDTO = _mapper.Map<BookDTO>(newBook);
            return newBookDTO;
        }

        public async Task<bool> DeleteBook(int id)
        {
            Book bookToDelete = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            _dbContext.Book.Remove(bookToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<BookDTO> UpdateBook(int id, BookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            Book bookToUpdate = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            bookToUpdate.Title = book.Title;
            bookToUpdate.Description = book.Description;
            bookToUpdate.Status = book.Status;
            _dbContext.Book.Update(bookToUpdate);
            await _dbContext.SaveChangesAsync();
            var updatedBookDTO = _mapper.Map<BookDTO>(bookToUpdate);
            return updatedBookDTO;
        }

        public async Task<BookDTO> GetBookByTitle(string title)
        {
            Book book = await _dbContext.Book.FirstOrDefaultAsync(b => b.Title == title);

            var bookDTO = _mapper.Map<BookDTO>(book);

            return bookDTO;
        }
    }
}
