using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;

namespace BookStore.Application.Services
{
	public interface IBookService
	{
		Task<Guid> CreateBook(BookDto bookDto);
		Task<Guid> DeleteBook(Guid id);
		Task<List<BookDto>> GetAllBooks();
		BookDto GetBookById(Guid id);
		Task<Guid> UpdateBook(Guid id, string title, decimal price, int numberInStock, string language, List<Guid> authors, List<Guid> genres, List<Guid> payments);
	}
}