using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.DataAccess.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly BookStoreDbContext _context;

		public CustomerRepository(BookStoreDbContext context)
		{
			_context = context;
		}

		public Customer GetById(Guid id)
		{
			var customer = _context.Customers
				.Include(c => c.Payments)
				.FirstOrDefault(a => a.Id == id);

			if (customer == null)
			{
				throw new Exception("Customer is null");
			}

			return customer;
		}

		public async Task<List<Customer>> Get()
		{
			var customers = await _context.Customers
				.AsNoTracking()
				.Include(c => c.Payments)
				.ToListAsync();

			return customers;
		}

		public async Task<Guid> Create(Customer customer)
		{
			await _context.Customers.AddAsync(customer);
			await _context.SaveChangesAsync();

			return customer.Id;
		}

		public async Task<Guid> Update(Guid id, string firstName, string lastName, string email, string password, List<Payment> payments)
		{
			var customer = await _context.Customers
				.Include(c => c.Payments)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (customer == null)
			{
				throw new Exception("Customer not found");
			}

			customer.FirstName = firstName;
			customer.LastName = lastName;
			customer.Email = email;
			customer.Password = password;

			customer.Payments.Clear();
			foreach (var payment in payments)
			{
				customer.Payments.Add(payment);
			}

			await _context.SaveChangesAsync();
			return id;
		}

		public async Task<Guid> Delete(Guid id)
		{
			await _context.Customers
				.Where(c => c.Id == id)
				.ExecuteDeleteAsync();

			return id;
		}
	}
}
