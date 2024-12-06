namespace BookStore.Core.Dto
{
	public class BookDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int NumberInStock { get; set; }
		public string Language { get; set; } = "Ukrainian";
		public string ImgUrl { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public List<Guid> AuthorIds { get; set; } = [];
		public List<Guid> GenreIds { get; set; } = [];
		public List<Guid> PaymentIds { get; set; } = [];
	}
}
