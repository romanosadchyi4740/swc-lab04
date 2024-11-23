namespace BookStore.DataAccess.Entities
{
	public class Book
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int NumberInStock { get; set; }
		public string Language { get; set; } = "Ukrainian";
		// public string ImgUrl { get; set; } = @"C:\Pictures\plain-black-03.jpg";
		public List<Author> Authors { get; set; } = [];
		public List<Genre> Genres { get; set; } = [];
		public List<Payment> Payments { get; set; } = [];
	}
}
