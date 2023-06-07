using Library.Data;
using Library.Models;
using Library.Models.DTO;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            return Ok(await _AuthorRepository.ShowAllAuthors());
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorDTO>> GetAuthorsById(int id)
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
        public async Task<ActionResult<AuthorDTO>> FindByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            AuthorDTO author = await _AuthorRepository.GetAuthorByName(name);
            return Ok(author);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor([FromBody] AuthorDTO author)
        {
            if(await _AuthorRepository.GetAuthorByName(author.Name) != null)
            {
                ModelState.AddModelError("", "The author already exits");
                return BadRequest(ModelState);
            }
            AuthorDTO newAuthor = await _AuthorRepository.AddAuthor(author);
            return Ok(newAuthor);
        }
        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorDTO>> UpdateAuthor([FromBody] AuthorDTO Author, int id)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest();
            }
            if (id == null)
            {
                return NotFound();
            }
            AuthorDTO authorToUpdate = await _AuthorRepository.GetById(id);
            Author.Name = authorToUpdate.Name;
            Author.BookId = authorToUpdate.BookId;
            Author.Book = authorToUpdate.Book;
            await _AuthorRepository.UpdateAuthor(id, authorToUpdate);
            return Ok(authorToUpdate);
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
