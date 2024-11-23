using BookStore.Application.Services;
using BookStore.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
	public class AuthorController : ControllerBase
	{
        private IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthors()
        {
            return Ok(await _authorService.GetAllAuthors());
        }

        [HttpGet("{authorId}")]
		public ActionResult<AuthorDto> GetAuthor(Guid authorId)
        {
            var author = _authorService.GetAuthorById(authorId);
            if (author == null) {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest();
            }

            authorDto.Id = Guid.NewGuid();
            await _authorService.CreateAuthor(authorDto);
            return Ok(authorDto.Id);      
        }

        [HttpPut("{authorId}")]
        public async Task<ActionResult<Guid>> UpdateAuthor(Guid authorId, [FromBody] AuthorDto authorDto)
        {
            var id = await _authorService.UpdateAuthor(authorId, authorDto.FirstName, authorDto.LastName, authorDto.BookIds);
            return Ok(id);
        }

        [HttpDelete("{authorId}")]
        public async Task<ActionResult<Guid>> DeleteAuthor(Guid authorId)
        {
            return Ok(await _authorService.DeleteAuthor(authorId));
        }
    }
}
