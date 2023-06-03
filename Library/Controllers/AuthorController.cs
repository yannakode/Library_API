using Library.Data;
using Library.Models;
using Library.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("Authors/Controller")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _AuthorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _AuthorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            return await _AuthorRepository.ShowAllAuthors();
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Author>> GetAllAuthorsById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            if(id == null)
            {
                return NotFound();
            }
            return Ok(await _AuthorRepository.GetById(id));
        }
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Author>> FindByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            Author authorName = await _AuthorRepository.GetAuthorByName(name);
            return Ok(authorName);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] Author author)
        {
            if(await _AuthorRepository.GetAuthorByName(author.Name) != null)
            {
                ModelState.AddModelError("", "The author already exits");
                return BadRequest(ModelState);
            }
            Author AuthorModel = await _AuthorRepository.AddAuthor(author);
            return Ok(AuthorModel);
        }
        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Author>> UpdateAuthor([FromBody] Author Author, int id)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest();
            }
            if (id == null)
            {
                return NotFound();
            }
            Author existingAuthor = await _AuthorRepository.GetById(id);
            Author.Name = existingAuthor.Name;
            Author.BookId = existingAuthor.BookId;
            Author.Book = existingAuthor.Book;
            await _AuthorRepository.UpdateAuthor(id, existingAuthor);
            return Ok(existingAuthor);
        }
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteAuthor(int id)
        {
            if(await _AuthorRepository.GetById(id) == null)
            {
                ModelState.AddModelError("", "The author doesn't exits");
                return BadRequest(ModelState);
            }
            if (id == 0 || id < 0)
            {
                return BadRequest();
            }
            if (id == null)
            {
                return NotFound();
            }
            bool delete = await _AuthorRepository.DeleteAuthor(id);
            return Ok(delete);
        }
       
    }
}
