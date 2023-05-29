using Library.Data;
using Library.Models;
using Library.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("Books/Controller")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            return await _bookRepository.ShowAllBooks();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Book>> GetAllBooksById(int id)
        {
            return await _bookRepository.GetById(id);
        }
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody ]Book book)
        {
            Book Book = await _bookRepository.AddBook(book);
            return Ok(Book);
        }
        [HttpPut("id")]
        public async Task<ActionResult<Book>> UpdateBook([FromBody] Book book, int id)
        {
            Book existingBook = await _bookRepository.GetById(id);
            book.Title = existingBook.Title;
            book.Description = existingBook.Description;
            book.Status = existingBook.Status;
            await _bookRepository.UpdateBook(id ,existingBook);
            return Ok(existingBook);
        }
        [HttpDelete("id")]
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {         
            bool delete = await _bookRepository.DeleteBook(id);
            return Ok(delete);
        }
    }
}
