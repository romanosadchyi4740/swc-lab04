using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
	public class GenreRepository : IGenreRepository
	{
		private readonly BookStoreDbContext _context;

		public GenreRepository(BookStoreDbContext context)
		{
			_context = context;
		}

		public Genre GetById(Guid id)
		{
			var genre = _context.Genres
				.Include(g => g.Books)
				.FirstOrDefault(a => a.Id == id);

			if (genre == null)
			{
				throw new Exception("Genre is null");
			}

			return genre;
		}

		public async Task<List<Genre>> Get()
		{
			var genres = await _context.Genres
				.AsNoTracking()
				.Include(g => g.Books)
				.ToListAsync();

			return genres;
		}

		public async Task<Guid> Create(Genre genre)
		{
			await _context.Genres.AddAsync(genre);
			await _context.SaveChangesAsync();

			return genre.Id;
		}

		public async Task<Guid> Update(Guid id, string genreName, List<Book> books)
		{
			var genre = await _context.Genres
				.Include(g => g.Books)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (genre == null)
			{
				throw new Exception("Genre not found");
			}

			genre.GenreName = genreName;

			genre.Books.Clear();
			foreach (var book in books)
			{
				genre.Books.Add(book);
			}

			await _context.SaveChangesAsync();
			return id;
		}

		public async Task<Guid> Delete(Guid id)
		{
			await _context.Genres
				.Where(g => g.Id == id)
				.ExecuteDeleteAsync();

			return id;
		}
	}
}
