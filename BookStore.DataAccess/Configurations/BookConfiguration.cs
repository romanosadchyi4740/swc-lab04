using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Configurations
{
	internal class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasKey(b => b.Id);

			builder
				.HasMany(b => b.Authors)
				.WithMany(a => a.Books);

			builder
				.HasMany(b => b.Genres)
				.WithMany(g => g.Books);

			builder
				.HasMany(b => b.Payments)
				.WithMany(p => p.Books);

			builder
				.Property(b => b.Title)
				.IsRequired();

			builder
				.Property(b => b.Price)
				.IsRequired();
		}
	}
}
