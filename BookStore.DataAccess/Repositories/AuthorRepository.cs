using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly BookStoreDbContext _context;

		public AuthorRepository(BookStoreDbContext context)
		{
			_context = context;
		}

		public Author GetById(Guid id)
		{
			var author = _context.Authors
				.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
			if (author == null)
			{
				throw new Exception("Author is null");
			}

			return author;
		}

		public async Task<List<Author>> Get()
		{
			var authors = await _context.Authors
				.AsNoTracking()
				.Include(a => a.Books)
				.ToListAsync();

			return authors;
		}

		public async Task<Guid> Create(Author author)
		{
			await _context.Authors.AddAsync(author);
			await _context.SaveChangesAsync();

			return author.Id;
		}

		public async Task<Guid> Update(Guid id, string firstName, string lastName, List<Book> books)
		{
			var author = await _context.Authors
				.Include(a => a.Books)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (author == null)
			{
				throw new Exception("Author not found");
			}

			author.FirstName = firstName;
			author.LastName = lastName;

			author.Books.Clear();
			foreach (var book in books)
			{
				author.Books.Add(book);
			}

			await _context.SaveChangesAsync();
			return id;
		}

		public async Task<Guid> Delete(Guid id)
		{
			await _context.Authors
				.Where(a => a.Id == id)
				.ExecuteDeleteAsync();

			return id;
		}
	}
}
