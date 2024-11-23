using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Configurations
{
	public class AuthorConfiguration : IEntityTypeConfiguration<Author>
	{
		public void Configure(EntityTypeBuilder<Author> builder)
		{
			builder.HasKey(a => a.Id);

			builder
				.HasMany(a => a.Books)
				.WithMany(b => b.Authors);

			builder
				.Property(a => a.FirstName)
				.IsRequired();

			builder
				.Property (a => a.LastName)
				.IsRequired();
		}
	}
}
