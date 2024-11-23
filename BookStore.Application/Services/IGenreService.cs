using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;

namespace BookStore.Application.Services
{
	public interface IGenreService
	{
		Task<Guid> CreateGenre(GenreDto genreDto);
		Task<Guid> DeleteGenre(Guid id);
		Task<List<GenreDto>> GetAllGenres();
		GenreDto GetGenreById(Guid id);
		Task<Guid> UpdateGenre(Guid id, string genreName, List<Guid> books);
	}
}