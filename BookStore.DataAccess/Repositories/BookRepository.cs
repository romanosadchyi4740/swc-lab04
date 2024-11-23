using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.DataAccess.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly BookStoreDbContext _context;

		public BookRepository(BookStoreDbContext context)
		{
			_context = context;
		}

		public Book GetById(Guid id)
		{
			var book = _context.Books
				.Include(b => b.Authors)
				.Include(b => b.Genres)
				.Include(b => b.Payments)
				.FirstOrDefault(b => b.Id == id);

			if (book == null)
			{
				throw new Exception("Book is null");
			}

			return book;
		}

		public async Task<List<Book>> Get()
		{
			var books = await _context.Books
				.AsNoTracking()
				.Include(b => b.Authors)
				.Include(b => b.Genres)
				.Include(b => b.Payments)
				.ToListAsync();

			return books;
		}

		public async Task<Guid> Create(Book book)
		{
			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();

			return book.Id;
		}

		public async Task<Guid> Update(Guid id, string title, decimal price, int numberInStock, string language, List<Author> authors, List<Genre> genres, List<Payment> payments)
		{
			var book = await _context.Books
				.Include(b => b.Authors)
				.Include(b => b.Genres)
				.Include(b => b.Payments)
				.FirstOrDefaultAsync(b => b.Id == id);

			if (book == null)
			{
				throw new Exception("Book not found");
			}

			book.Title = title;
			book.Price = price;
			book.NumberInStock = numberInStock;
			book.Language = language;

			book.Authors.Clear();
			foreach (var author in authors)
			{
				book.Authors.Add(author);
			}

			book.Genres.Clear();
			foreach (var genre in genres)
			{
				book.Genres.Add(genre);
			}

			book.Payments.Clear();
			foreach (var payment in payments)
			{
				book.Payments.Add(payment);
			}

			await _context.SaveChangesAsync();
			return id;
		}

		public async Task<Guid> Delete(Guid id)
		{
			await _context.Books
				.Where(b => b.Id == id)
				.ExecuteDeleteAsync();

			return id;
		}
	}
}
