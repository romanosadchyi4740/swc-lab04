using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories
{
	public interface IPaymentRepository
	{
		Task<Guid> Create(Payment payment);
		Task<Guid> Delete(Guid id);
		Task<List<Payment>> Get();
		Payment GetById(Guid id);
		Task<Guid> Update(Guid id, Guid customerId, Customer customer, List<Book> books, decimal amount, DateTime date);
	}
}