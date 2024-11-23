using BookStore.Core.Dto;
using BookStore.DataAccess.Entities;

namespace BookStore.Application.Services
{
	public interface IPaymentService
	{
		Task<Guid> CreatePayment(PaymentDto paymentDto);
		Task<Guid> DeletePayment(Guid id);
		Task<List<PaymentDto>> GetAllPayments();
		PaymentDto GetPaymentById(Guid id);
		Task<Guid> UpdatePayment(Guid id, Guid customerId, decimal amount, DateTime date, List<Guid> books);
	}
}