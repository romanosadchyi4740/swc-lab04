using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories
{
	public interface ICustomerRepository
	{
		Task<Guid> Create(Customer customer);
		Task<Guid> Delete(Guid id);
		Task<List<Customer>> Get();
		Customer GetById(Guid id);
		Task<Guid> Update(Guid id, string firstName, string lastName, string email, string password, List<Payment> payments);
	}
}