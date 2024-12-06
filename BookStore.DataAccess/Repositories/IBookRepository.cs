using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories
{
	public interface IBookRepository
	{
		Task<Guid> Create(Book book);
		Task<Guid> Delete(Guid id);
		Task<List<Book>> Get();
		Book GetById(Guid id);
		Task<Guid> Update(Guid id, string title, decimal price, int numberInStock, string language,
			string imgUrl, string description, List<Author> authors, List<Genre> genres, List<Payment> payments);
	}
}