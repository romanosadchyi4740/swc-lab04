using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories;

namespace BookStore.Application.Services
{
	public class GenreService : IGenreService
	{
		private readonly IBookRepository _bookRepository;
		private readonly IGenreRepository _genreRepository;

		public GenreService(IBookRepository bookRepository, IGenreRepository genreRepository)
		{
			_bookRepository = bookRepository;
			_genreRepository = genreRepository;
		}

		public GenreDto GetGenreById(Guid id)
		{
			return EntityToDto(_genreRepository.GetById(id));
		}

		public async Task<List<GenreDto>> GetAllGenres()
		{
			List<Genre> genres = await _genreRepository.Get();

			return genres.Select(EntityToDto).ToList();
		}

		public async Task<Guid> CreateGenre(GenreDto genreDto)
		{
			return await _genreRepository.Create(DtoToEntity(genreDto));
		}

		public async Task<Guid> UpdateGenre(Guid id, string genreName, List<Guid> books)
		{
			return await _genreRepository.Update(id, genreName, books.Select(_bookRepository.GetById).ToList());
		}

		public async Task<Guid> DeleteGenre(Guid id)
		{
			return await _genreRepository.Delete(id);
		}

		private Genre DtoToEntity(GenreDto genreDto)
		{
			return new Genre
			{
				Id = genreDto.Id,
				GenreName = genreDto.GenreName,
				Books = genreDto.BookIds.Select(_bookRepository.GetById).ToList(),
			};
		}

		private GenreDto EntityToDto(Genre genre)
		{
			return new GenreDto(genre.Id, genre.GenreName)
			{
				BookIds = genre.Books.Select(b => b.Id).ToList(),
			};
		}
	}
}
