using Library.Data;
using Library.Models;
using Library.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Library.Models.DTO;

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
        public async Task<ActionResult<List<BookDTO>>> GetAllBooks()
        {
            return await _bookRepository.ShowAllBooks();
        }

        [HttpGet("id")]
        public async Task<ActionResult<BookDTO>> GetAllBooksById(int id)
        {
            return await _bookRepository.GetById(id);
        }
        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook([FromForm]Book book)
        {
            if (book.Photo != null && book.Photo.Length > 0)
            {
                var filePath = Path.Combine("Storage", book.Photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await book.Photo.CopyToAsync(stream);
                }
            }
            return Ok(book);
        }
        [HttpPut("id")]
        public async Task<ActionResult<BookDTO>> UpdateBook([FromBody] BookDTO book, int id)
        {
            BookDTO bookToUpdate = await _bookRepository.GetById(id);
            book.Title = bookToUpdate.Title;
            book.Description = bookToUpdate.Description;
            book.Status = bookToUpdate.Status;
            await _bookRepository.UpdateBook(id ,bookToUpdate);
            return Ok(bookToUpdate);
        }
        [HttpDelete("id")]
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {         
            bool delete = await _bookRepository.DeleteBook(id);
            return Ok(delete);
        }
        [HttpGet("Search")]
        public async Task<ActionResult<BookDTO>> FindBook(string name)
        {
            BookDTO FindBookByTitle = await _bookRepository.GetBookByTitle(name);
            return Ok(FindBookByTitle);
        }
    }
}
