using BookStore.Application.Services;
using BookStore.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;

		public PaymentController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		[HttpGet]
		public async Task<ActionResult<List<PaymentDto>>> GetPayments()
		{
			return Ok(await _paymentService.GetAllPayments());
		}

		[HttpGet("{paymentId}")]
		public ActionResult<PaymentDto> GetPayment(Guid paymentId)
		{
			var payment = _paymentService.GetPaymentById(paymentId);
			return payment != null ? Ok(payment) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> CreatePayment([FromBody] PaymentDto paymentDto)
		{
			if (paymentDto == null)
			{
				return BadRequest();
			}

			paymentDto.Id = Guid.NewGuid();
			await _paymentService.CreatePayment(paymentDto);
			return Ok(paymentDto.Id);
		}

		[HttpPut("{paymentId}")]
		public async Task<ActionResult<Guid>> UpdatePayment(Guid paymentId, [FromBody] PaymentDto paymentDto)
		{
			var id = await _paymentService.UpdatePayment(paymentId, paymentDto.CustomerId, paymentDto.Amount, paymentDto.Date, paymentDto.BookIds);
			return Ok(id);
		}

		[HttpDelete("{paymentId}")]
		public async Task<ActionResult<Guid>> DeletePayment(Guid paymentId)
		{
			return Ok(await _paymentService.DeletePayment(paymentId));
		}
	}
}
