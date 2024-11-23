using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
	public class PaymentRepository : IPaymentRepository
	{
		private readonly BookStoreDbContext _context;

		public PaymentRepository(BookStoreDbContext context)
		{
			_context = context;
		}

		public Payment GetById(Guid id)
		{
			var payment = _context.Payments
				.Include(p => p.Books)
				.FirstOrDefault(a => a.Id == id);

			if (payment == null)
			{
				throw new Exception("Payment is null");
			}

			return payment;
		}

		public async Task<List<Payment>> Get()
		{
			var payments = await _context.Payments
				.AsNoTracking()
				.Include(p => p.Books)
				.ToListAsync();

			return payments;
		}

		public async Task<Guid> Create(Payment payment)
		{
			await _context.Payments.AddAsync(payment);
			await _context.SaveChangesAsync();

			return payment.Id;
		}

		public async Task<Guid> Update(Guid id, Guid customerId, Customer customer, List<Book> books, decimal amount, DateTime date)
		{
			var payment = await _context.Payments
				.Include(p => p.Books)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (payment == null)
			{
				throw new Exception("Payment not found");
			}

			payment.CustomerId = customerId;
			payment.Customer = customer;
			payment.Amount = amount;
			payment.Date = date;

			payment.Books.Clear();
			foreach (var book in books)
			{
				payment.Books.Add(book);
			}

			await _context.SaveChangesAsync();
			return id;
		}

		public async Task<Guid> Delete(Guid id)
		{
			await _context.Payments
				.Where(p => p.Id == id)
				.ExecuteDeleteAsync();

			return id;
		}
	}
}
