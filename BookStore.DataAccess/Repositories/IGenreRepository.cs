using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories
{
	public interface IGenreRepository
	{
		Task<Guid> Create(Genre genre);
		Task<Guid> Delete(Guid id);
		Task<List<Genre>> Get();
		Genre GetById(Guid id);
		Task<Guid> Update(Guid id, string genreName, List<Book> books);
	}
}