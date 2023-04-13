using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Btb.Api.Source.Domain.Features.GetCustomer
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCustomerController : ControllerBase
    {
        private readonly IMediator mediator;
        public GetCustomerController(IMediator mediator) => this.mediator = mediator;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomers(int id)
        {
            var result = await mediator.Send(new GetCustomerCommand(id));

            return Ok(result);
        }
    }
}
