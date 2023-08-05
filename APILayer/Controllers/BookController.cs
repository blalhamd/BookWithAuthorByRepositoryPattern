using DomainModelsLayer.Entites;
using DomainModelsLayer.EntitiesDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.APPDPCONTEXT;
using Repositories.EntitiesRepositories;
using Repositories.Interfaces;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IGenricReopsitories<Book> _BookRepository;

        public BookController(IGenricReopsitories<Book> BookRepository)
        {
            _BookRepository = BookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var query =await _BookRepository.GetAll();
                         
            if (query is null)
                return BadRequest("not exist Books");

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = await _BookRepository.GetById(id);
            
            if (query is null)
                return BadRequest("Book is not exist");

            return Ok(query);
        }

        [HttpGet("GetByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var query = await _BookRepository.Find(x=>x.Title== title);

            if (query is null)
                return BadRequest("Book is not exist");

            return Ok(query);
        }


        [HttpGet("authorId")]
        public async Task<IActionResult> GetByAuthorId(int authorId)
        {
            var query = await _BookRepository.Find(x => x.AuthorId == authorId);

            if (query is null)
                return BadRequest("Book is not exist");

            return Ok(query);
        }


        [HttpPost]
        public async Task<IActionResult> addBook([FromBody] BookDTO bookdto)
        {
           
            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (bookdto is null)
                return BadRequest("Book is null");


            Book book = new Book()
            {
                Title = bookdto.Title,
                AuthorId = bookdto.AuthorId,
            };

            _BookRepository.Add(book);
            _BookRepository.save();

            return Created("", book);
        }



        [HttpPost("{AddRange}")]
        public async Task<IActionResult> addRange([FromBody] List<BookDTO> bookDTOs)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (bookDTOs is null)
                return BadRequest("Book is null");

            List<Book> books = new List<Book>();

            foreach (var item in bookDTOs)
            {
                Book book = new Book()
                {
                    Title = item.Title,
                    AuthorId = item.AuthorId,

                };

                books.Add(book);
            }

            _BookRepository.AddRange(books);
            _BookRepository.save();

            return Created("", books);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDTO bookDto,int id)
        {
            var search= await _BookRepository.GetById(id);

            if (search == null)
                return NotFound("This Book is not exist");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (bookDto is null)
                return BadRequest("Book is null");

            search.Title = bookDto.Title;
            search.AuthorId = bookDto.AuthorId;       

            _BookRepository.Update(search);
            _BookRepository.save();

            return Ok(search);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBook(int id)
        {
            var query = await _BookRepository.GetById(id);

            if (query is null)
                return BadRequest("This book is null");

            _BookRepository.Delete(query);
            _BookRepository.save();

            return Ok(query);
        }
    }
}
