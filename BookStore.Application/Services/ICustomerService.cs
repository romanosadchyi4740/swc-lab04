using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;

namespace BookStore.Application.Services
{
	public interface ICustomerService
	{
		Task<Guid> CreateCustomer(CustomerDto customerDto);
		Task<Guid> DeleteCustomer(Guid id);
		Task<List<CustomerDto>> GetAllCustomers();
		CustomerDto GetCustomerById(Guid id);
		Task<Guid> UpdateCustomer(Guid id, string firstName, string lastName, string email, string password, List<Guid> payments);
	}
}