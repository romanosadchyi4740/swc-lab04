using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories;

namespace BookStore.Application.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IBookRepository _bookRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IPaymentRepository _paymentRepository;

		public PaymentService(IBookRepository bookRepository, ICustomerRepository customerRepository, IPaymentRepository paymentRepository)
		{
			_bookRepository = bookRepository;
			_customerRepository = customerRepository;
			_paymentRepository = paymentRepository;
		}

		public PaymentDto GetPaymentById(Guid id)
		{
			return EntityToDto(_paymentRepository.GetById(id));
		}

		public async Task<List<PaymentDto>> GetAllPayments()
		{
			List<Payment> payments = await _paymentRepository.Get();

			return payments.Select(EntityToDto).ToList();
		}

		public async Task<Guid> CreatePayment(PaymentDto paymentDto)
		{
			return await _paymentRepository.Create(DtoToEntity(paymentDto));
		}

		public async Task<Guid> UpdatePayment(Guid id, Guid customerId, decimal amount, DateTime date, List<Guid> books)
		{
			return await _paymentRepository.Update(id, customerId, _customerRepository.GetById(customerId),
				books.Select(_bookRepository.GetById).ToList(), amount, date);
		}

		public async Task<Guid> DeletePayment(Guid id)
		{
			return await _paymentRepository.Delete(id);
		}

		private Payment DtoToEntity(PaymentDto paymentDto)
		{
			return new Payment
			{
				Id = paymentDto.Id,
				CustomerId = paymentDto.CustomerId,
				Customer = _customerRepository.GetById(paymentDto.CustomerId),
				Amount = paymentDto.Amount,
				Date = paymentDto.Date,
				Books = paymentDto.BookIds.Select(_bookRepository.GetById).ToList(),
			};
		}

		private PaymentDto EntityToDto(Payment payment)
		{
			return new PaymentDto(payment.Id, payment.CustomerId, payment.Amount, payment.Date)
			{
				BookIds = payment.Books.Select(b => b.Id).ToList(),
			};
		}
	}
}
