using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;

namespace BookStore.Application.Services
{
	public interface IAuthorService
	{
		Task<Guid> CreateAuthor(AuthorDto authorDto);
		Task<Guid> DeleteAuthor(Guid id);
		Task<List<AuthorDto>> GetAllAuthors();
		AuthorDto GetAuthorById(Guid id);
		Task<Guid> UpdateAuthor(Guid id, string firstName, string lastName, List<Guid> books);
	}
}