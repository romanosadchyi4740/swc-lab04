using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories;

namespace BookStore.Application.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IPaymentRepository _paymentRepository;

		public CustomerService(ICustomerRepository customerRepository, IPaymentRepository paymentRepository)
		{
			_paymentRepository = paymentRepository;
			_customerRepository = customerRepository;
		}

		public CustomerDto GetCustomerById(Guid id)
		{
			return EntityToDto(_customerRepository.GetById(id));
		}

		public async Task<List<CustomerDto>> GetAllCustomers()
		{
			List<Customer> customer = await _customerRepository.Get();

			return customer.Select(EntityToDto).ToList();
		}

		public async Task<Guid> CreateCustomer(CustomerDto customerDto)
		{
			return await _customerRepository.Create(DtoToEntity(customerDto));
		}

		public async Task<Guid> UpdateCustomer(Guid id, string firstName, string lastName, string email, string password, List<Guid> payments)
		{
			return await _customerRepository.Update(id, firstName, lastName, email, password, payments.Select(_paymentRepository.GetById).ToList());
		}

		public async Task<Guid> DeleteCustomer(Guid id)
		{
			return await _customerRepository.Delete(id);
		}

		private Customer DtoToEntity(CustomerDto customerDto)
		{
			return new Customer
			{
				Id = customerDto.Id,
				FirstName = customerDto.FirstName,
				LastName = customerDto.LastName,
				Email = customerDto.Email,
				Password = customerDto.Password,
				Payments = customerDto.PaymentIds.Select(_paymentRepository.GetById).ToList(),
			};
		}

		private CustomerDto EntityToDto(Customer customer)
		{
			return new CustomerDto(customer.Id, customer.FirstName, customer.LastName, customer.Email, customer.Password)
			{
				PaymentIds = customer.Payments.Select(p => p.Id).ToList(),
			};
		}
	}
}