using BookStore.Application.Services;
using BookStore.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookController : ControllerBase
	{
		private readonly IBookService _bookService;

		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}

		[HttpGet]
		public async Task<ActionResult<List<BookDto>>> GetBooks()
		{
			return Ok(await _bookService.GetAllBooks());
		}

		[HttpGet("{bookId}")]
		public ActionResult<BookDto> GetBook(Guid bookId)
		{
			var book = _bookService.GetBookById(bookId);
			return book != null ? Ok(book) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> CreateBook([FromBody] BookDto bookDto)
		{
			if (bookDto == null)
			{
				return BadRequest();
			}

			bookDto.Id = Guid.NewGuid();
			await _bookService.CreateBook(bookDto);
			return Ok(bookDto.Id);
		}

		[HttpPut("{bookId}")]
		public async Task<ActionResult<Guid>> UpdateBook(Guid bookId, [FromBody] BookDto bookDto)
		{
			var id = await _bookService.UpdateBook(bookId, bookDto.Title, bookDto.Price, bookDto.NumberInStock, bookDto.Language, bookDto.AuthorIds, bookDto.GenreIds, bookDto.PaymentIds);
			return Ok(id);
		}

		[HttpDelete("{bookId}")]
		public async Task<ActionResult<Guid>> DeleteBook(Guid bookId)
		{
			return Ok(await _bookService.DeleteBook(bookId));
		}
	}
}
