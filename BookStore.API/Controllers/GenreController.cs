using BookStore.Application.Services;
using BookStore.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GenreController : ControllerBase
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}

		[HttpGet]
		public async Task<ActionResult<List<GenreDto>>> GetGenres()
		{
			return Ok(await _genreService.GetAllGenres());
		}

		[HttpGet("{genreId}")]
		public ActionResult<GenreDto> GetGenre(Guid genreId)
		{
			var genre = _genreService.GetGenreById(genreId);
			return genre != null ? Ok(genre) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> CreateGenre([FromBody] GenreDto genreDto)
		{
			if (genreDto == null)
			{
				return BadRequest();
			}

			genreDto.Id = Guid.NewGuid();
			await _genreService.CreateGenre(genreDto);
			return Ok(genreDto.Id);
		}

		[HttpPut("{genreId}")]
		public async Task<ActionResult<Guid>> UpdateGenre(Guid genreId, [FromBody] GenreDto genreDto)
		{
			var id = await _genreService.UpdateGenre(genreId, genreDto.GenreName, genreDto.BookIds);
			return Ok(id);
		}

		[HttpDelete("{genreId}")]
		public async Task<ActionResult<Guid>> DeleteGenre(Guid genreId)
		{
			return Ok(await _genreService.DeleteGenre(genreId));
		}
	}
}
