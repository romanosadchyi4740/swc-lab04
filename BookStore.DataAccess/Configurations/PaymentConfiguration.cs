using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Configurations
{
	internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder.HasKey(p => p.Id);

			builder
				.HasMany(p => p.Books)
				.WithMany(b => b.Payments);

			builder
				.HasOne(p => p.Customer)
				.WithMany(c => c.Payments)
				.HasForeignKey(p => p.CustomerId);

			builder
				.Property(p => p.Amount)
				.IsRequired();

			builder
				.Property(p => p.Date)
				.IsRequired();
		}
	}
}
