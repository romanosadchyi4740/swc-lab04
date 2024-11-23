using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories;

namespace BookStore.Application.Services
{
	public class AuthorService : IAuthorService
	{
		private readonly IAuthorRepository _authorRepository;
		private readonly IBookRepository _bookRepository;

		public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
		{
			_authorRepository = authorRepository;
			_bookRepository = bookRepository;
		}

		public AuthorDto GetAuthorById(Guid id)
		{
			return EntityToDto(_authorRepository.GetById(id));
		}

		public async Task<List<AuthorDto>> GetAllAuthors()
		{
			List<Author> authors = await _authorRepository.Get();

			return authors.Select(EntityToDto).ToList();
		}

		public async Task<Guid> CreateAuthor(AuthorDto authorDto)
		{
			return await _authorRepository.Create(DtoToEntity(authorDto));
		}

		public async Task<Guid> UpdateAuthor(Guid id, string firstName, string lastName, List<Guid> books)
		{
			return await _authorRepository.Update(id, firstName, lastName, books.Select(_bookRepository.GetById).ToList());
		}

		public async Task<Guid> DeleteAuthor(Guid id)
		{
			return await _authorRepository.Delete(id);
		}

		private Author DtoToEntity(AuthorDto authorDto)
		{
			return new Author
			{
				Id = authorDto.Id,
				FirstName = authorDto.FirstName,
				LastName = authorDto.LastName,
				Books = authorDto.BookIds.Select(_bookRepository.GetById).ToList(),
			};
		}

		private AuthorDto EntityToDto(Author author)
		{
			return new AuthorDto(author.Id, author.FirstName, author.LastName)
			{
				BookIds = author.Books.Select(b => b.Id).ToList()
			};
		}
	}
}
