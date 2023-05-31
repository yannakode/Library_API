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
        public async Task<ActionResult<Author>> GetAllAuthorsById(int id)
        {
            return await _AuthorRepository.GetById(id);
        }
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] Author author)
        {
            if(author.BookId.HasValue)
            {

            }
            Author AuthorModel = await _AuthorRepository.AddAuthor(author);
            return Ok(AuthorModel);
        }
        [HttpPut("id")]
        public async Task<ActionResult<Author>> UpdateAuthor([FromBody] Author Author, int id)
        {
            Author existingAuthor = await _AuthorRepository.GetById(id);
            Author.Name = existingAuthor.Name;
            Author.BookId = existingAuthor.BookId;
            Author.Book = existingAuthor.Book;
            await _AuthorRepository.UpdateAuthor(id, existingAuthor);
            return Ok(existingAuthor);
        }
        [HttpDelete("id")]
        public async Task<ActionResult<bool>> DeleteAuthor(int id)
        {
            bool delete = await _AuthorRepository.DeleteAuthor(id);
            return Ok(delete);
        }
        [HttpGet("Search")]
        public async Task<ActionResult<Author>> FindByName(string name)
        {
            Author authorName = await _AuthorRepository.GetAuthorByTitle(name);
            return Ok(authorName);
        }
    }
}
