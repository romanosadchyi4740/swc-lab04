namespace BookStore.DataAccess.Entities
{
	public class Customer
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public List<Payment> Payments { get; set; } = [];
	}
}
