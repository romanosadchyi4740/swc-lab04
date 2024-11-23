namespace BookStore.Core.Dto
{
	public class CustomerDto
	{
		public CustomerDto() { }

		public CustomerDto(Guid id, string firstName, string lastName, string email, string password)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Password = password;
		}

		public CustomerDto(Guid id, string firstName, string lastName, string email, string password, List<Guid> paymentIds)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Password = password;
			PaymentIds = paymentIds;
		}

		public Guid Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public List<Guid> PaymentIds { get; set; } = [];

		public static CustomerDto? Create(Guid id, string firstName, string lastName, string email, string password, List<Guid> paymentIds)
		{
			if (id == Guid.Empty ||
				string.IsNullOrWhiteSpace(firstName) ||
				string.IsNullOrWhiteSpace(lastName) ||
				string.IsNullOrWhiteSpace(email) ||
				string.IsNullOrWhiteSpace(password) ||
				paymentIds == null)
			{
				return null;
			}

			return new CustomerDto(id, firstName, lastName, email, password)
			{
				PaymentIds = paymentIds
			};
		}
	}
}
