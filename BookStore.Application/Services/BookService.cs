using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories;

namespace BookStore.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IAuthorRepository _authorRepository;
		private readonly IBookRepository _bookRepository;
		private readonly IGenreRepository _genreRepository;
		private readonly IPaymentRepository _paymentRepository;

		public BookService(IBookRepository bookRepository,
			IAuthorRepository authorRepository,
			IGenreRepository genreRepository,
			IPaymentRepository paymentRepository)
		{
			_bookRepository = bookRepository;
			_authorRepository = authorRepository;
			_genreRepository = genreRepository;
			_paymentRepository = paymentRepository;
		}

		public BookDto GetBookById(Guid id)
		{
			return EntityToDto(_bookRepository.GetById(id));
		}

		public async Task<List<BookDto>> GetAllBooks()
		{
			List<Book> books = await _bookRepository.Get();

			return books.Select(EntityToDto).ToList();
		}

		public async Task<Guid> CreateBook(BookDto bookDto)
		{
			return await _bookRepository.Create(DtoToEntity(bookDto));
		}

		public async Task<Guid> UpdateBook(Guid id, string title, decimal price, int numberInStock, string language,
			List<Guid> authors, List<Guid> genres, List<Guid> payments)
		{
			return await _bookRepository.Update(id, title, price, numberInStock, language,
				authors.Select(_authorRepository.GetById).ToList(),
				genres.Select(_genreRepository.GetById).ToList(),
				payments.Select(_paymentRepository.GetById).ToList());
		}

		public async Task<Guid> DeleteBook(Guid id)
		{
			return await _bookRepository.Delete(id);
		}

		private Book DtoToEntity(BookDto bookDto)
		{
			return new Book
			{
				Id = bookDto.Id,
				Title = bookDto.Title,
				Price = bookDto.Price,
				NumberInStock = bookDto.NumberInStock,
				Language = bookDto.Language,
				Authors = bookDto.AuthorIds.Select(_authorRepository.GetById).ToList(),
				Genres = bookDto.GenreIds.Select(_genreRepository.GetById).ToList(),
				Payments = bookDto.PaymentIds.Select(_paymentRepository.GetById).ToList(),
			};
		}

		private BookDto EntityToDto(Book book)
		{
			return new BookDto(book.Id, book.Title, book.Price, book.NumberInStock, book.Language)
			{
				AuthorIds = book.Authors.Select(a => a.Id).ToList(),
				GenreIds = book.Genres.Select(g => g.Id).ToList(),
				PaymentIds = book.Payments.Select(p => p.Id).ToList(),
			};
		}
	}
}
