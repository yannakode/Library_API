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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookRepository(LibraryDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<BookDTO>> ShowAllBooks()
        {
            IEnumerable<Book> bookList = await _dbContext.Book.ToListAsync();
            IEnumerable<BookDTO> bookListDTO = _mapper.Map<IEnumerable<BookDTO>>(bookList);

            return bookListDTO;
        }

        public async Task<BookDTO> GetById(int id)
        {
            var book = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            var bookDTO = _mapper.Map<BookDTO>(book);

            return bookDTO;
        }

        public async Task<BookDTO> AddBook(BookDTO bookDTO)
        {
            if (!ValidateFile(bookDTO.File))
            {
                return null;
            }
            var bookToAdd = await UploadImage(bookDTO);
            var newBook = _mapper.Map<Book>(bookToAdd);

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

            return bookDTO;
        }

        public async Task<BookDTO> GetBookByTitle(string title)
        {
            Book book = await _dbContext.Book.FirstOrDefaultAsync(b => b.Title == title);
            var bookDTO = _mapper.Map<BookDTO>(book);

            return bookDTO;
        }

        public async Task<BookDTO> UploadImage(BookDTO bookDTO)
        {
                string uniqueString = bookDTO.FileName + Guid.NewGuid().ToString() + "_" + bookDTO.File.FileName;
                string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", uniqueString);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await bookDTO.File.CopyToAsync(stream);
                }

                bookDTO.FileName = uniqueString;
                
                return bookDTO;
        }

        public bool ValidateFile(IFormFile file)
        {
            Dictionary<string, List<byte[]>> Signatures = new Dictionary<string, List<byte[]>>()
            {
                 {".jpg", new List<byte[]>
                {
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
                }
                },

                {".png", new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
                },

                {".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
                }
                }
            };
            foreach(string extensions  in Signatures.Keys)
            {
                using(var reader = new BinaryReader(file.OpenReadStream()))
                {
                    var signatures = Signatures[extensions];
                    var readerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                    if(signatures.Any(signature => readerBytes.Take(signature.Length).SequenceEqual(signature)) && file.Length <= 10485760)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
