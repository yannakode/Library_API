using AutoMapper;
using Library.Data;
using Library.Models;
using Library.Models.DTO;
using Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorRepository(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> GetById(int id)
        {
            var Author = await _dbContext.Author.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            var authorDTO = _mapper.Map<AuthorDTO>(Author);
            return authorDTO;
        }

        public async Task<IEnumerable<AuthorDTO>> ShowAllAuthors()
        {
            IEnumerable<Author> authorList = await _dbContext.Author.ToListAsync();
            IEnumerable<AuthorDTO> authorListDTO = _mapper.Map<IEnumerable<AuthorDTO>>(authorList);
            return authorListDTO;
        }
        public async Task<AuthorDTO> AddAuthor(AuthorDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            await _dbContext.Author.AddAsync(author);
            await _dbContext.SaveChangesAsync();
            var autorDTO = _mapper.Map<AuthorDTO>(author);
            return authorDTO;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            Author authorToDelete = await _dbContext.Author.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            _dbContext.Author.Remove(authorToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<AuthorDTO> UpdateAuthor(int id, AuthorDTO authorDTO)
        {
            Author authorToUpdate = await _dbContext.Author.FirstOrDefaultAsync(b => b.Id == id);
            var author = _mapper.Map<Author>(authorDTO);
            author.Id = authorDTO.Id;
            author.Name = authorDTO.Name;
            author.BookId = authorDTO.BookId;
            author.Book = authorDTO.Book;
            _dbContext.Author.Update(authorToUpdate);
            await _dbContext.SaveChangesAsync();
            var AuthorDTO = _mapper.Map<AuthorDTO>(author);
            return AuthorDTO;
        }
        public async Task<AuthorDTO> GetAuthorByName(string name)
        {
            Author authorByName = await _dbContext.Author.AsNoTracking().FirstOrDefaultAsync(a => a.Name == name);
            var authorDTO = _mapper.Map<AuthorDTO>(authorByName);
            return authorDTO;
        }
    }
}
