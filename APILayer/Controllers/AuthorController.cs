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
    public class AuthorController : ControllerBase
    {
        private readonly IGenricReopsitories<Author> _AuthorReopsitories;

        public AuthorController(IGenricReopsitories<Author> genricReopsitories)
        {
            _AuthorReopsitories = genricReopsitories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var query = await _AuthorReopsitories.GetAll();
                              
            if (query is null)
                return BadRequest("not exist authors");

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = await _AuthorReopsitories.GetById(id);

            if (query is null)
                return BadRequest("author is not exist");

            return Ok(query);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var query = await _AuthorReopsitories.Find(x=>x.Name==name);

            if (query is null)
                return BadRequest("author is not exist");

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> addAuthor([FromBody] AuthorDTO authorDto)
        {
          
            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (authorDto is null)
                return BadRequest("author is null");

            Author author = new Author()
            {
                Name = authorDto.Name,
            };

            _AuthorReopsitories.Add(author);
            _AuthorReopsitories.save();

            return Created("", author);
        }

        [HttpPost("addRange")]
        public async Task<IActionResult> addRangeAuthor([FromBody] List<AuthorDTO> authorsDto)
        {
          
            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (authorsDto is null)
                return BadRequest("author is null");

            List<Author> authorList = new List<Author>();

            foreach (var item in authorsDto)
            {
                Author author = new Author()
                {
                    Name = item.Name,
                };

                authorList.Add(author);
            }

            _AuthorReopsitories.AddRange(authorList);
            _AuthorReopsitories.save();

            return Created("", authorList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorDTO dto,int id)
        {
            var query= await _AuthorReopsitories.GetById(id);

            if (query == null)
                return NotFound("this author is not exist");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (dto is null)
                return BadRequest("author is null");

            
            query.Name=dto.Name;

            _AuthorReopsitories.Update(query);
            _AuthorReopsitories.save();

            return Ok(query);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteAuthor(int id)
        {
            var query = await _AuthorReopsitories.GetById(id);

            if (query is null)
                return BadRequest("this Author is not exist");

            _AuthorReopsitories.Delete(query);
            _AuthorReopsitories.save();

            return Ok(query);
        }
    }
}
