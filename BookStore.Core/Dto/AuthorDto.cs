namespace BookStore.Core.Dto
{
	public class AuthorDto
	{
        public AuthorDto() { }

        public AuthorDto(Guid id, string firstName, string lastName)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
		}

		public AuthorDto(Guid id, string firstName, string lastName, List<Guid> bookIds)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			BookIds = bookIds;
		}

		public Guid Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public List<Guid> BookIds { get; set; } = [];

		public static AuthorDto? Create(Guid id, string firstName, string lastName, List<Guid> bookIds)
		{
			if (id == Guid.Empty || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || bookIds == null)
			{
				return null;
			}

			return new AuthorDto(id, firstName, lastName)
			{
				BookIds = bookIds
			};
		}
	}
}
