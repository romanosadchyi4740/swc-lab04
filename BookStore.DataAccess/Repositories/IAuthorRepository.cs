using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories
{
	public interface IAuthorRepository
	{
		Task<Guid> Create(Author author);
		Task<Guid> Delete(Guid id);
		Task<List<Author>> Get();
		Author GetById(Guid id);
		Task<Guid> Update(Guid id, string firstName, string lastName, List<Book> books);
	}
}