namespace BookStore.Core.Dto
{
	public class PaymentDto
	{
		public PaymentDto() { }

		public PaymentDto(Guid id, Guid customerId, decimal amount, DateTime date)
		{
			Id = id;
			CustomerId = customerId;
			Amount = amount;
			Date = date;
		}

		public PaymentDto(Guid id, Guid customerId, decimal amount, DateTime date, List<Guid> bookIds)
		{
			Id = id;
			CustomerId = customerId;
			Amount = amount;
			Date = date;
			BookIds = bookIds;
		}

		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public List<Guid> BookIds { get; set; } = [];

		public static PaymentDto? Create(Guid id, Guid customerId, decimal amount, DateTime date, List<Guid> bookIds)
		{
			if (id == Guid.Empty || customerId == Guid.Empty || bookIds == null)
			{
				return null;
			}

			return new PaymentDto(id, customerId, amount, date)
			{
				BookIds = bookIds
			};
		}
	}
}
