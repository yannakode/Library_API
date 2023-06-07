using Library.Models;
using Library.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            var response = await _bookRepository.ShowAllBooks();
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetBookById(int id)
        {
            var book = await _bookRepository.GetById(id);
            return Ok();
        }

        [HttpPost]
        [Route("CreateBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookDTO>> CreateBook([FromForm] BookDTO bookDTO)
        {
            var bookToAdd = await _bookRepository.AddBook(bookDTO);
            if(bookToAdd != null)
            {
                return Ok(bookToAdd);
            }
            return BadRequest();               
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> UpdateBook([FromBody] BookDTO book, int id)
        {
            BookDTO bookToUpdate = await _bookRepository.GetById(id);
            book.Title = bookToUpdate.Title;
            book.Description = bookToUpdate.Description;
            book.Status = bookToUpdate.Status;
            await _bookRepository.UpdateBook(id, bookToUpdate);
            return Ok(bookToUpdate);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {
            bool delete = await _bookRepository.DeleteBook(id);
            return Ok(delete);
        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookDTO>> FindBook(string name)
        {
            BookDTO FindBookByTitle = await _bookRepository.GetBookByTitle(name);
            return Ok(FindBookByTitle);
        }



    }    
}
