namespace BookStore.Core.Dto
{
    public class GenreDto
    {
        public GenreDto() { }

        public GenreDto(Guid id, string genreName)
        {
            Id = id;
            GenreName = genreName;
        }

        public GenreDto(Guid id, string genreName, List<Guid> bookIds)
        {
            Id = id;
            GenreName = genreName;
            BookIds = bookIds;
        }


		public Guid Id { get; set; }
		public string GenreName { get; set; } = string.Empty;
        public List<Guid> BookIds { get; set; } = [];

        public static GenreDto? Create(Guid id, string genreName, List<Guid> bookIds)
        {
            if (id == Guid.Empty || string.IsNullOrWhiteSpace(genreName) || bookIds == null)
            {
                return null;
            }

            return new GenreDto(id, genreName)
            {
                BookIds = bookIds,
            };
        }
	}
}
