namespace BookStore.Core.Dto
{
	public class BookDto
	{
		public BookDto() { }

        public BookDto(Guid id, string title, decimal price, int numberInStock, string language)
        {
			Id = id;
			Title = title;
			Price = price;
			NumberInStock = numberInStock;
			Language = language;
        }

		public BookDto(Guid id, string title, decimal price, int numberInStock, string language, List<Guid> authorIds, List<Guid> genreIds, List<Guid> paymentIds)
		{
			Id = id;
			Title = title;
			Price = price;
			NumberInStock = numberInStock;
			Language = language;
			AuthorIds = authorIds;
			GenreIds = genreIds;
			PaymentIds = paymentIds;
		}

		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int NumberInStock { get; set; }
		public string Language { get; set; } = "Ukrainian";
		// public string ImgUrl { get; set; }
		public List<Guid> AuthorIds { get; set; } = [];
		public List<Guid> GenreIds { get; set; } = [];
		public List<Guid> PaymentIds { get; set; } = [];

		public static BookDto? Create(Guid id, string title, decimal price, int numberInStock, string language, List<Guid> authorIds, List<Guid> genreIds, List<Guid> paymentIds)
		{
			if (id == Guid.Empty || string.IsNullOrWhiteSpace(title) || authorIds == null || genreIds == null || paymentIds == null)
			{
				return null;
			}

			return new BookDto(id, title, price, numberInStock, language)
			{
				AuthorIds = authorIds,
				GenreIds = genreIds,
				PaymentIds = paymentIds
			};
		}
	}
}
