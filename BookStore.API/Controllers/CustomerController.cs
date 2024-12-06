using BookStore.Application.Services;
using BookStore.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
	private readonly ICustomerService _customerService;

	public CustomerController(ICustomerService customerService)
	{
		_customerService = customerService;
	}

	[HttpGet]
	public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
	{
		return Ok(await _customerService.GetAllCustomers());
	}

	[HttpGet("{customerId}")]
	public ActionResult<CustomerDto> GetCustomer(Guid customerId)
	{
		var customer = _customerService.GetCustomerById(customerId);
		return customer != null ? Ok(customer) : NotFound();
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> CreateCustomer([FromBody] CustomerDto customerDto)
	{
		if (customerDto == null)
		{
			return BadRequest();
		}

		customerDto.Id = Guid.NewGuid();
		await _customerService.CreateCustomer(customerDto);
		return Ok(customerDto.Id);
	}

	[HttpPut("{customerId}")]
	public async Task<ActionResult<Guid>> UpdateCustomer(Guid customerId, [FromBody] CustomerDto customerDto)
	{
		var id = await _customerService.UpdateCustomer(customerId, customerDto.FirstName, customerDto.LastName, customerDto.Email, customerDto.Password, customerDto.PaymentIds);
		return Ok(id);
	}

	[HttpDelete("{customerId}")]
	public async Task<ActionResult<Guid>> DeleteCustomer(Guid customerId)
	{
		return Ok(await _customerService.DeleteCustomer(customerId));
	}
}