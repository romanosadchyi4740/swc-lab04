using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Configurations
{
	public class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder.HasKey(g => g.Id);

			builder
				.HasMany(g => g.Books)
				.WithMany(b => b.Genres);

			builder
				.Property(g => g.GenreName)
				.IsRequired();
		}
	}
}
